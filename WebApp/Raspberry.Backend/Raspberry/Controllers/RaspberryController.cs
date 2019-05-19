using Microsoft.AspNetCore.Mvc;
using Raspberry.Models;
using Raspberry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raspberry.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RaspberryController : Controller
    {
        private IRaspberryService _raspberry;
        public RaspberryController(IRaspberryService raspberryService)
        {
            _raspberry = raspberryService;
        }

        [HttpPost]
        public async Task<IActionResult> AbortHeating()
        {
            await _raspberry.CancelHeating();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTemperature()
        {
            var temperature = await _raspberry.GetTemperature();
            return Ok(temperature);
        }

        [HttpPost]
        public async Task<IActionResult> SetTemperature([FromBody] DesiredTemperature temperature)
        {
            if(temperature == null)
            {
                return BadRequest();
            }


            var response = await _raspberry.TurnHeatOn(temperature);

            if(response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest(); 
        }
    }
}
