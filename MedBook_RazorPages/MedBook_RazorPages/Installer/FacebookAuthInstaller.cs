using MedBook_RazorPages.Models;
using MedBook_RazorPages.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Installer
{
    public class FacebookAuthInstaller : IInstaller
    {
        public void InstallService(IServiceCollection service, IConfiguration configuration)
        {
            var facebookAuthSettings = new FacebookAuthSettings();
            configuration.Bind(nameof(FacebookAuthSettings), facebookAuthSettings);
            service.AddSingleton(facebookAuthSettings);

            service.AddHttpClient();
            service.AddSingleton<IFacebookAuthService, FacebookAuthService>();
        }
    }
}
