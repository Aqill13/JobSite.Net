using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobSite.Services
{
    public class UserStatusBackgroundService : BackgroundService
    {
        private readonly ILogger<UserStatusBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserStatusBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<UserStatusBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
                    var users = userManager.Users.ToList();

                    foreach (var user in users)
                    {
                        if (user.LastActiveTime.HasValue && (DateTime.Now - user.LastActiveTime.Value).TotalMinutes >= 1)
                        {
                            user.Status = "Offline";
                            await userManager.UpdateAsync(user);
                        }
                    }
                }
            }
        }
    }


}

