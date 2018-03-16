using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Infrastructure
{
    public static class Server
    {
        static IHostingEnvironment env;

        public static void Initialize(IHostingEnvironment environment)
        {
            env = environment;
        }

        public static string MapPath(string virtualPath)
        {
            return Path.Combine(
                env.WebRootPath,
                virtualPath.TrimStart("~/".ToCharArray()).Replace('/', '\\')
            );
        }
    }
}
