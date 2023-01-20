using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

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
        public async Task<IEnumerable<Vehicle>> GetAll(
            [FromQuery] int? brandId,
            [FromQuery] string? vehicleName)
        {
            return await _vehicleRepository.GetAllAsync(brandId, vehicleName);
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
        public async Task<ActionResult<Vehicle>> Create([FromBody] Vehicle vehicle)
        {
            var vehicleId = await _vehicleRepository.CreateAsync(vehicle);
            return await _vehicleRepository.GetByIdAsync(vehicleId);
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<Vehicle>> Update(
            [FromRoute] int id,
            [FromBody] Vehicle vehicle)
        {
            var thisVehicle = await _vehicleRepository.GetByIdAsync(id);

            if (thisVehicle is null)
                return NotFound();

            vehicle.Id = id;
            var success = await _vehicleRepository.UpdateAsync(vehicle);
            
            if (!success)
                return BadRequest();
            
            return await _vehicleRepository.GetByIdAsync(id);
        }
    }
}
