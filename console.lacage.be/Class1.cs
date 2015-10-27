using Common.Logging.Configuration;
using Hangfire;
using Hangfire.SqlServer;
using log4net;
using log4net.Config;
using common.lacage.be.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace console.lacage.be
{
    class Program
    {  
        private static SqlServerStorage _storage;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
 

        static void Main(string[] args)
        {
            XmlConfigurator.Configure(); //only once

            _log.Debug("Application is starting");

            if (args.Length > 0)
            {

                _storage = new SqlServerStorage(@"Data Source=.;Initial Catalog=SpriteHangFire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False");
                var options = new BackgroundJobServerOptions();
                var server = new BackgroundJobServer(options, _storage);
                server.Start();
                
                Console.WriteLine("Hangfire Server started. Press any key to exit...");

                var input = Console.ReadKey();
                HandleInput(input);
                //using ()
                //{
                //    server.Start();


            }
            else
            {
                _log.Info("application launched without arguments");
                throw new NullReferenceException("kkkkkkjkljfkldsjfkl");
                //FileInfo f = new FileInfo();
                File.Create(string.Format(@"C:\TFSDATA\KissTheFuture\Trunk\shopping.lacage.be\shoppingconsole.lacage.be\bin\Debug\{0}.txt", Guid.NewGuid()));
                Thread.Sleep(5000);
            }
            
                //c.Schedule(() => MyJob.DoSomething(), TimeSpan.FromMinutes(10));
             
                //Console.ReadKey();
            //}
        }


        private static void HandleInput(ConsoleKeyInfo input )
        {


            /*if (input.KeyChar.Equals('s'))
            {


                var newInput = Console.ReadKey();
                HandleInput(newInput);
            }
            else*/ if (input.KeyChar.Equals('q'))
            {
                BackgroundJobClient c = new BackgroundJobClient(_storage);

                //RecurringJob.AddOrUpdate(()=>MyJob.DoSomething(),Cron.
                var b = new BackgroundJobClient(_storage);
                //b.Enqueue(() => Console.WriteLine("my job"));

                c.Enqueue(() => LaunchAppJob.Launch(@"C:\TFSDATA\KissTheFuture\Trunk\shopping.lacage.be\shoppingconsole.lacage.be\bin\Debug\shoppingconsole.lacage.be.exe"));//, new Hangfire.States.EnqueuedState("backgroundsynchro"));
                //c.Enqueue(() => Console.Write('q'));
                Console.WriteLine("Hangfire job enqueued. Press any key to exit...");
                       //Thread.Sleep(5000);                               

                var newInput = Console.ReadKey();
                HandleInput(newInput);
            }


        }
    }
}
