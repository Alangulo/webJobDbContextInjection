using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebJobAddDBContext
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {


        static IConfiguration _iconfiguration;

        public Program(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public static IConfigurationRoot Configuration { get; set; }
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        public static void Main(string[] args)
        {
            var dbConnectionString = "";

            var serviceCollection = new ServiceCollection();
            // wire up your services    
            serviceCollection.AddTransient<IThing, Thing>();

            // important! wire up your actual jobs, too
            serviceCollection.AddTransient<ServiceBusJobListener>();

            // added example to connect EF
            serviceCollection.AddDbContext<dbExample>(options =>
               options.UseSqlServer(dbConnectionString));


            // add it to a JobHostConfiguration
            Configuration.JobActivator = new ServiceJobActivator(serviceCollection);



            var builder2 = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
          
            Configuration = builder2.Build();
            var getConfigTest = Configuration.GetSection("testing");

            var builder = new HostBuilder();

            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorage();
                b.AddExecutionContextBinding();
               
               // b.AddAzureStorage();
                // b.Services.AddSingleton<IConfiguration>();
                // b.AddExtension<InjectConfiguration>();

                // b.AddExecutionContextBinding(GetConfiguration(""));


            });
            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();


            });

            var host = builder.Build();
            using (host)
            {
                host.Run();
            }


        }









    }

}
