using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gateways.Persistence.Context;
using Gateways.Domaine;
using Gateways.Persistence.UnitOfWork.Interface;
using Microsoft.Extensions.Logging;

namespace Gateways.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeripheralDevicesController : ControllerBase
    {
        //private readonly GatewaysDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GatewaysController> _logger;

        public PeripheralDevicesController(IUnitOfWork unitOfWork, ILogger<GatewaysController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/PeripheralDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeripheralDevice>>> GetPeripheralDevices()
        {
            //return await _context.PeripheralDevices.ToListAsync();
            return await _unitOfWork.PeripheralDeviceRepository.GetAllAsync();
        }

        // GET: api/PeripheralDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeripheralDevice>> GetPeripheralDevice(int id)
        {
            //var peripheralDevice = await _context.PeripheralDevices.FindAsync(id);
            var peripheralDevice = await _unitOfWork.PeripheralDeviceRepository.GetAsync(id);

            if (peripheralDevice == null)
            {
                _logger.LogDebug(" Peripheral Device don't found.");
                return NotFound();
            }

            return peripheralDevice;
        }

        // PUT: api/PeripheralDevices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeripheralDevice(int id, PeripheralDevice peripheralDevice)
        {
            if (id != peripheralDevice.Id)
            {
                return BadRequest();
            }

            //_context.Entry(peripheralDevice).State = EntityState.Modified;
            _unitOfWork.PeripheralDeviceRepository.Update(peripheralDevice);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PeripheralDeviceExists(id))
                {
                    _logger.LogError($"Update function error: Peripheral Device don't exist.");
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

        // POST: api/PeripheralDevices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PeripheralDevice>> PostPeripheralDevice(PeripheralDevice device)
        {
            //_context.PeripheralDevices.Add(device);
            await _unitOfWork.PeripheralDeviceRepository.AddAsync(device);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetPeripheralDevice", new { id = device.Id }, device);
        }

        // DELETE: api/PeripheralDevices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeripheralDevice(int id)
        {
            //var device = await _context.PeripheralDevices.FindAsync(id);
            var device = await _unitOfWork.PeripheralDeviceRepository.DeleteAsync(id);
            if (device == null)
            {
                _logger.LogDebug(" Peripheral Device don't found.");
                return NotFound();
            }
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        private bool PeripheralDeviceExists(int id)
        {
            return _unitOfWork.PeripheralDeviceRepository.GetAsync(id).Result != null;
        }
    }
}
