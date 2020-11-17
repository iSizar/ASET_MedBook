using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Installer
{
    interface IInstaller
    {
        void InstallService(IServiceCollection service, IConfiguration configuration);
    }
}
