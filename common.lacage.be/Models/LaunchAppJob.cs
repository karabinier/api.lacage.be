using Common.Logging.Configuration;
using Hangfire;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace common.lacage.be.Models
{
    public class LaunchAppJob
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
 
        [DisplayName("Launching App {0}")]
        [AutomaticRetry(Attempts = 0)]
        public static void Launch(string app)
        {

            var properties = new NameValueCollection();
            properties["configType"] = "INLINE";
            Common.Logging.LogManager.Adapter = new Common.Logging.Log4Net.Log4NetLoggerFactoryAdapter(properties);

            _log.InfoFormat("Launching {0}.", app);
            throw new Exception("kjk");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = @"C:\TFSDATA\KissTheFuture\Trunk\shopping.lacage.be\shoppingconsole.lacage.be\bin\Debug\shoppingconsole.lacage.be.exe";
            startInfo.FileName = app;
            Process.Start(startInfo);
            
            Console.WriteLine("processing order..");
        }
    }
}
