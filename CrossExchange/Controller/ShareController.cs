using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrossExchange.Controller
{
    [Route("api/Share")]
    public class ShareController : ControllerBase
    {
        public IShareRepository _shareRepository { get; set; }

        public ShareController(IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> Get([FromRoute]string symbol)
        {
            var shares = _shareRepository.Query().Where(x => x.Symbol.Equals(symbol)).ToList();
            if (!shares.Any())
            {
                return NotFound();
            }
            return Ok(shares);
        }


        [HttpGet("{symbol}/Latest")]
        public async Task<IActionResult> GetLatestPrice([FromRoute]string symbol)
        {
            var share = await _shareRepository.Query().Where(x => x.Symbol.Equals(symbol)).OrderByDescending(x => x.TimeStamp).FirstOrDefaultAsync();
            if (null == share)
            {
                return NotFound();
            }
            return Ok(share?.Rate);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]HourlyShareRateModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }

            HourlyShareRate share = new HourlyShareRate
            {
                Rate = model.Rate,
                Symbol = model.Symbol,
                TimeStamp = model.TimeStamp
            };


            await _shareRepository.InsertAsync(share);

            return Created($"Share/{share.Id}", share);
        }

    }
}
