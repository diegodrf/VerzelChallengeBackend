using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using WebApi.Models;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Vehicle>> GetAll([FromQuery] string? q, [FromQuery] bool priceLowerToHigher = true)
        {
            return await _vehicleRepository.GetAllAsync(q, priceLowerToHigher);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<ActionResult<Vehicle>> GetById([FromRoute] int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if(vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            
            if (vehicle is null) 
                return NotFound();

            await _vehicleRepository.DeleteAsync(vehicle);
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vehicle>> Create([FromBody] VehicleRequest vehicle)
        {
            string? filename = null;
            if(vehicle.ImageBase64 is not null)
            {
                var fileExtension = ImageUtils.GetImageExtentionFromBase64(vehicle.ImageBase64);
                filename = Guid.NewGuid().ToString("N") + fileExtension;
                await ImageUtils.SaveImage(filename, ImageUtils.ParseBase64(vehicle.ImageBase64));
            }
            
            var vehicleId = await _vehicleRepository.CreateAsync(vehicle, filename);

            var newVehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (newVehicle is null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return newVehicle;
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<Vehicle>> Update(
            [FromRoute] int id,
            [FromBody] VehicleRequest vehicle)
        {
            var thisVehicle = await _vehicleRepository.GetByIdAsync(id);

            if (thisVehicle is null)
                return NotFound();

            string? filename = null;
            if (vehicle.ImageBase64 is not null)
            {
                var fileExtension = ImageUtils.GetImageExtentionFromBase64(vehicle.ImageBase64);
                filename = Guid.NewGuid().ToString("N") + fileExtension;
                await ImageUtils.SaveImage(filename, ImageUtils.ParseBase64(vehicle.ImageBase64));
            }
            
            var success = await _vehicleRepository.UpdateAsync(id, vehicle, filename);

            if (!success)
                return BadRequest();
            
            return await _vehicleRepository.GetByIdAsync(id);
        }

        [HttpGet]
        [Route("brands")]
        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await _vehicleRepository.GetAllBrandsAsync();
        }
    }
}
