using ChallangeX1.GuessNumberModels;
using ChallangeX1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChallangeX1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessNumberController: ControllerBase
    {
        private IGuessNumberService _guessNumberService;
        public GuessNumberController(IGuessNumberService guessNumberService)
        {
            _guessNumberService = guessNumberService;
        }

        [HttpPost]
        public ActionResult<List<GuessNumberResponse>> Post([FromBody] GuessNumberRequest value)
        {
            var result = _guessNumberService.PlayGame(value);
            return result;
        }
    }
}
