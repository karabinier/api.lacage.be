using System;
using Hangfire;
using Hangfire.SqlServer;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(api.lacage.be.Startup))]

namespace api.lacage.be
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseHangfire(config =>
            {
                //config.UseSqlServerStorage(@"Data Source=t55kxp502o.database.windows.net;Initial Catalog=DBLacage;Integrated Security=False;User ID=lacagek;Password=JgE7Wofm;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
                config.UseSqlServerStorage(@"Data Source=jaykay;Initial Catalog=SpriteHangFire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False");
                
            });
        }
    }
}
