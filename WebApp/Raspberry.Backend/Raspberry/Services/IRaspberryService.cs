using Raspberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Raspberry.Services
{
    public interface IRaspberryService
    {
        Task<HttpResponseMessage> TurnHeatOn(DesiredTemperature desired);
        Task<RealTimeData> GetTemperature();
        Task CancelHeating();
    }
}
