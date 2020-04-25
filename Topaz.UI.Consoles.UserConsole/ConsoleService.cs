
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Topaz.UI.Consoles.UserConsole.Interfaces;

namespace Topaz.UI.Consoles.UserConsole
{
    public class ConsoleService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private IUserCreationService _userCreationService;

        public ConsoleService(ILogger<ConsoleService> logger, IUserCreationService userCreationService)
        {
            _logger = logger;
            _userCreationService = userCreationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _userCreationService.CreateUser();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}