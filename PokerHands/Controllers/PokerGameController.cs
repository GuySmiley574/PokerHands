using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokerHands.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokerGameController : ControllerBase
    {

        private readonly ILogger<PokerGameController> _logger;

        public PokerGameController(ILogger<PokerGameController> logger)
        {
            _logger = logger;
        }

        // GET: api/<PokerGameController>
        [HttpGet]
        //[HttpGet(Name = "GetPokerResults")]
        public string Get()
        {
            var game = new Game();
            game.AddPlayer("players[1]");
            game.AddPlayer("players[2]");
            game.AddPlayer("players[3]");
            game.AddPlayer("players[4]");
            game.DealAll();
            //game.StackHand("players[1]", new List<string>() { "2H", "3H", "4H", "5H", "6H" });
            //game.StackHand("players[2]", new List<string>() { "2S", "3S", "4S", "5S", "6S" });
            //game.StackHand("players[3]", new List<string>() { "3D", "4D", "5D", "6D", "7S" });

            return game.SynopsisAll;
        }

        //// GET api/<PokerGameController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<PokerGameController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<PokerGameController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PokerGameController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
