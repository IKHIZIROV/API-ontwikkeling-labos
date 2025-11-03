using Microsoft.AspNetCore.Mvc;

namespace Labo03_Configuration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController(IConfiguration configuration) : ControllerBase
    {
        [HttpGet]
        [Route("my-key")]
        public ActionResult ReadKeys()
        {
            var key1 = configuration["ApplicationSettings:AppName"];
            var key2 = configuration["ApplicationSettings:Version"];
            var key3 = configuration["ApplicationSettings:MaxUsers"];

            return Ok(
                new
                {
                    AppName = key1,
                    Version = key2,
                    MaxUsers = key3
                }
            );
        }

        [HttpGet]
        [Route("bind")]
        public ActionResult BindConfiguration()
        {
            var appSettings = new ConfigurationSettings();
            configuration.GetSection("ApplicationSettings").Bind(appSettings);

            return Ok(new
            {
                appSettings.AppName,
                appSettings.Version,
                appSettings.MaxUsers
            });
        }
    }
}
