using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using uPLibrary.Networking.M2Mqtt;

[assembly: OwinStartup(typeof(TcWebHooks.Startup))]

namespace TcWebHooks
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
