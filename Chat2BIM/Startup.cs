using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Chat2BIM.Startup))]

namespace Chat2BIM
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Weitere Informationen zum Konfigurieren Ihrer Anwendung finden Sie unter https://go.microsoft.com/fwlink/?LinkID=316888.
            app.MapSignalR();
        }

    }

    
}
