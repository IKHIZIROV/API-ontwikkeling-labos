using Labo02_DaysAndMonthsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo02_DaysAndMonthsAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class DayController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Day>> GetDays()
        {
            return new List<Day>
            {
                new Day{ Name = "Monday"},
                new Day { Name = "Tuesday" },
                new Day { Name = "Wednesday" },
                new Day { Name = "Thursday" },
                new Day { Name = "Friday" },
                new Day { Name = "Saturday" },
                new Day { Name = "Sunday" }
            };
        }
    }
}
