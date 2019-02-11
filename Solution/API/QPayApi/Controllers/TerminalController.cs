using Microsoft.AspNetCore.Mvc;
using PaymentTerminal.Business;
using QPayApi.Models;

namespace QPayApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Terminal")]
    public class TerminalController : Controller
    {
        private readonly ValidateScan _validateScan;

        public TerminalController(ValidateScan validateScan)
        {
            _validateScan = validateScan;
        }

        // POST: api/Terminal
        [HttpPost]
        public (int status, string message) Post([FromBody]NewScan newScan) => _validateScan.CheckScan(newScan.CardUid, newScan.DeviceUid);
    }
}