using Labo02_DaysAndMonthsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo02_DaysAndMonthsAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MonthController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Month>> GetMonths()
        {
            return new List<Month>
            {
                new Month { Name = "Januari", Days = 31},
                new Month { Name = "February", Days = 28 },
                new Month { Name = "March", Days = 31 },
                new Month { Name = "April", Days = 30 },
                new Month { Name = "May", Days = 31 },
                new Month { Name = "June", Days = 30 },
                new Month { Name = "July", Days = 31 },
                new Month { Name = "August", Days = 31 },
                new Month { Name = "September", Days = 30 },
                new Month { Name = "October", Days = 31 },
                new Month { Name = "November", Days = 30 },
                new Month { Name = "December", Days = 31 }
            };
        }
    }
}
