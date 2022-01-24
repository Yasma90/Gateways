using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Gateways.Domaine;
using Gateways.Persistence.UnitOfWork.Interface;

namespace Gateways.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GatewaysController> _logger;
        
        public GatewaysController(IUnitOfWork unitOfWork, ILogger<GatewaysController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/Gateways
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetGateways()
        {
            return await _unitOfWork.GatewayRepository.GetAsync(includeProperties: "PeripheralDevices");
        }

        // GET: api/Gateways/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gateway>> GetGateway(int id)
        {
            var gateway = await _unitOfWork.GatewayRepository.GetAsync(g => g.Id == id, includeProperties: "PeripheralDevices");

            if (gateway == null)
            {
                _logger.LogDebug("Gateway don't found.");
                return NotFound();
            }

            return gateway.FirstOrDefault();
        }

        // PUT: api/Gateways/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGateway(int id, Gateway gateway)
        {
            if (!ModelState.IsValid || id != gateway.Id)
            {
                return BadRequest();
            }

            _unitOfWork.GatewayRepository.Update(gateway);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!GatewayExists(id))
                {
                    _logger.LogError($"Update function error: Gateway don't exist.");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Update function error: {ex.Message}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gateways
        [HttpPost]
        public async Task<ActionResult<Gateway>> PostGateway(Gateway gateway)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _unitOfWork.GatewayRepository.AddAsync(gateway);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetGateway", new { id = gateway.Id }, gateway);
        }

        // DELETE: api/Gateways/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGateway(int id)
        {
            var gateway = await _unitOfWork.GatewayRepository.DeleteAsync(id);
            if (gateway == null)
            {
                _logger.LogDebug(" Gateway don't found.");
                return NotFound();
            }

            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        private bool GatewayExists(int id)
        {
            return _unitOfWork.GatewayRepository.GetbyIdAsync(id).Result != null;
        }


        // POST api/<Gateway>
        //[HttpPost]
        //public IActionResult Post()
        //{
        //    var gateway = new Gateway
        //    {
        //        Id = 1,
        //        IPAddress = "127.0.0.1",
        //        Name = "Brige Health Care",
        //        SerialNumber = "104528E"
        //    };

        //    var Catalog = new PeripheralDevice
        //    {
        //        Vendor = "Huawei",
        //        GatewayId = 1,
        //        Gateway = gateway
        //    };

        //    _unitOfWork.GatewayRepository.AddAsync(gateway);
        //    _unitOfWork.PeripheralDeviceRepository.AddAsync(Catalog);
        //    _unitOfWork.SaveChangesAsync();
        //    _logger.LogDebug("Gateway added successful!");
        //    return Ok();
        //}
    }
}
