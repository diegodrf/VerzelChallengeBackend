using Dapper;
using Dapper.Contrib.Extensions;
using WebApi.Models;
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

        public async Task<int> CreateAsync(Vehicle vehicle)
        {
            var query = @"INSERT INTO Vehicles (Name, Model, Price, BrandId) VALUES (@name, @model, @price, @brandId);
                        SELECT CAST(scope_identity() AS INT);";

            using var connection = _db.CreateConnection();
            var vehicleId = await connection.ExecuteScalarAsync<int>(query, new {
                vehicle.Name, 
                vehicle.Model, 
                vehicle.Price, 
                brandId = vehicle.Brand.Id
            });
            return vehicleId;
        }

        public async Task<bool> DeleteAsync(Vehicle vehicle)
        {
            using var connection = _db.CreateConnection();
            var success = await connection.DeleteAsync(vehicle);
            return success;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(int? brandId, string? vehicleName)
        {
            var filters = new List<string>();

            if (brandId is not null)
            {
                filters.Add("b.Id = @brandId");
            }

            if (vehicleName is not null)
            {
                filters.Add("v.Name = @vehicleName");
            }

            var whereCondition = filters.Count() == 0 ? "" : $" WHERE {string.Join(" AND ", filters)}";

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
                + ";";

            using var connection = _db.CreateConnection();

            var vehicles = new List<Vehicle>();
            await connection.QueryAsync<Vehicle, Brand, Image, Vehicle>(
                query,
                param: new { brandId, vehicleName },
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

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            using var connection = _db.CreateConnection();
            var vehicle = await connection.GetAsync<Vehicle>(id);
            return vehicle;
        }

        public async Task<bool> UpdateAsync(Vehicle vehicle)
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
                brandId = vehicle.Brand.Id,
                vehicle.Id
            });
            return affectedRows == 1;
        }
    }
}
