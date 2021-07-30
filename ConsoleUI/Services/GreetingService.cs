//using ConsoleUI.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleUI
{
    public class GreetingService : IGreetingService
    {
        private readonly ILogger<GreetingService> _log;
        private readonly IConfiguration _config;
        //private readonly ExampleSettings _exampleSettings;

        //public GreetingService(ILogger<GreetingService> log, IConfiguration config, ExampleSettings exampleSettings)
        public GreetingService(ILogger<GreetingService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
            //_exampleSettings = exampleSettings;
        }
        public void Run()
        {
            var greetingMessage = _config.GetValue<string>("GreetingMessage");


            _log.LogInformation("Hello {greetingMessage}", greetingMessage);


            //_log.LogInformation("Example {exampleValue}", _exampleSettings.Settings1);

        }
    }
}
