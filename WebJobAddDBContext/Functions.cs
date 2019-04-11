using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static WebJobAddDBContext.InjectConfiguration;

namespace WebJobAddDBContext
{
    public class Functions
    {

        public  Functions(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }


        public  void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //var databaseConnString = Configuration.GetSection("StoreDataServiceSettings").GetValue<string>("DatabaseConnectionString");
            services.AddDbContext<dbExample>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));

       




        }


        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log, [Inject()] IServiceCollection greeter, [Inject()] IConfiguration config)

        {
            log.WriteLine(message);
            greeter.AddDbContext<dbExample>(options => options.UseSqlServer(config.GetConnectionString("ConnectionStrings")));


        }


    }
}
