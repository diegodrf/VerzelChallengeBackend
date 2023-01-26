using Dapper;
using Dapper.Contrib.Extensions;
using System.Data.Common;
using System.Text.RegularExpressions;
using WebApi.Models;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDatabaseService _db;

        public VehicleRepository(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(VehicleRequest vehicle, string? filename)
        {
            var queryToInsertVehicle = @"INSERT INTO Vehicles (Name, Model, Price, BrandId) VALUES (@name, @model, @price, @brandId);
                                        SELECT CAST(scope_identity() AS INT);";
            
            using var connection = _db.CreateConnection();
            var vehicleId = await connection.ExecuteScalarAsync<int>(queryToInsertVehicle, new
            {
                vehicle.Name,
                vehicle.Model,
                vehicle.Price,
                vehicle.BrandId,
                filename
            });

            if(filename is not null)
            {
                var queryToInsertImage = @"INSERT INTO Images VALUES (@filename, @vehicleId);
                                SELECT CAST(scope_identity() AS INT);";

                var imageId = await connection.ExecuteScalarAsync<int>(queryToInsertImage, new { filename, vehicleId });
            }

            return vehicleId;
        }

        public async Task DeleteAsync(Vehicle vehicle)
        {
            var query = @"DELETE FROM Images WHERE VehicleId = @id;
                          DELETE FROM Vehicles WHERE Id = @id;";

            using var connection = _db.CreateConnection();
            await connection.ExecuteScalarAsync(query, new { vehicle.Id });
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(string? value, bool orderAscendant)
        {
            var filters = new List<string>();

            var whereCondition = string.Empty;

            if (!string.IsNullOrWhiteSpace(value))
            {
                whereCondition = " WHERE b.Name like @value OR v.Name like @value";
            };

            var orderBy = $" ORDER BY v.Price {(orderAscendant ? "" : "DESC")};";

            var query = @"SELECT
                v.Id,
                v.Name,
                v.Model,
                v.Price,
                b.Id,
                b.Name,
                i.Id,
                i.Filename
                FROM Vehicles as v
                INNER JOIN Brands as b ON b.Id = v.BrandId
                LEFT JOIN Images as i ON i.VehicleId = v.Id"
                + whereCondition
                + orderBy;

            using var connection = _db.CreateConnection();

            var vehicles = new List<Vehicle>();
            await connection.QueryAsync<Vehicle, Brand, Image, Vehicle>(
                query,
                param: new { value = $"%{value}%" },
                splitOn: "Id",
                map: (vehicle, brand, image) =>
                {
                    var thisVehicle = vehicles.FirstOrDefault(_ => _.Id == vehicle.Id);
                    if (thisVehicle is not null)
                    {
                        thisVehicle.Brand = brand;
                        if (image is not null)
                        {
                            thisVehicle.AddImage(image);
                        }
                    }
                    else
                    {
                        thisVehicle = vehicle;
                        thisVehicle.Brand = brand;
                        if (image is not null)
                        {
                            thisVehicle.AddImage(image);
                        }
                        vehicles.Add(thisVehicle);
                    }

                    return thisVehicle;
                });

            return vehicles!;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            var query = "SELECT Id, Name FROM Brands ORDER BY Name";
            using var connection = _db.CreateConnection();
            return await connection.QueryAsync<Brand>(query);
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            var query = @"SELECT
                v.Id,
                v.Name,
                v.Model,
                v.Price,
                b.Id,
                b.Name,
                i.Id,
                i.Filename
                FROM Vehicles as v
                INNER JOIN Brands as b ON b.Id = v.BrandId
                LEFT JOIN Images as i ON i.VehicleId = v.Id
                WHERE v.Id = @id;";

            using var connection = _db.CreateConnection();

            var vehicles = new List<Vehicle>();
            await connection.QueryAsync<Vehicle, Brand, Image, Vehicle>(
                query,
                param: new { id },
                splitOn: "Id",
                map: (vehicle, brand, image) =>
                {
                    var thisVehicle = vehicles.FirstOrDefault(_ => _.Id == vehicle.Id);
                    if (thisVehicle is not null)
                    {
                        thisVehicle.Brand = brand;
                        if (image is not null)
                        {
                            thisVehicle.AddImage(image);
                        }
                    }
                    else
                    {
                        thisVehicle = vehicle;
                        thisVehicle.Brand = brand;
                        if (image is not null)
                        {
                            thisVehicle.AddImage(image);
                        }
                        vehicles.Add(thisVehicle);
                    }

                    return thisVehicle;
                });

            return vehicles.FirstOrDefault() ;
        }

        public async Task<bool> UpdateAsync(int id, VehicleRequest vehicle, string? filename)
        {
            var query = @"UPDATE Vehicles SET
                            Name = @name,
                            Model = @model, 
                            Price = @price, 
                            BrandId = @brandId
                        WHERE Id = @id;";
            using var connection = _db.CreateConnection();

            var affectedRows = await connection.ExecuteAsync(query, new
            {
                vehicle.Name,
                vehicle.Model,
                vehicle.Price,
                brandId = vehicle.BrandId,
                id
            });

            if(filename is not null)
            {
                var queryToInsertImage = @"UPDATE Images SET Filename = @filename WHERE VehicleId = @id;";

                var imageId = await connection.ExecuteAsync(queryToInsertImage, new { filename, id });
            }
            
            return affectedRows == 1;
        }
    }
}
