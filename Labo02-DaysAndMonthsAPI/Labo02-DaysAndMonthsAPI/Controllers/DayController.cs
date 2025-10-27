using Labo02_DaysAndMonthsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo02_DaysAndMonthsAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class BlaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<blabla>> GetDays()
        {
            return new List<blabla>
            {
                new blabla{ Name = "Monday"},
                new blabla { Name = "Tuesday" },
                new blabla { Name = "Wednesday" },
                new blabla { Name = "Thursday" },
                new blabla { Name = "Friday" },
                new blabla { Name = "Saturday" },
                new blabla { Name = "Sunday" }
            };
        }
    }
}
