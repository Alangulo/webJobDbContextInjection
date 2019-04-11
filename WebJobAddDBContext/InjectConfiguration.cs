using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJobAddDBContext
{
    class InjectConfiguration
    {
        public InjectConfiguration(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        private void RegisterServices(IServiceCollection services)
        {
            //string connectionString = "****************";
       
            services.AddDbContext<dbExample>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));
            services.AddScoped<dbExample>();
      
        }


        public void Initialize(ExtensionConfigContext context)
        {
            var services = new ServiceCollection();
            RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider(true);

            context
                .AddBindingRule<InjectAttribute>()
                .Bind(new InjectBindingProvider(serviceProvider));


        }


        [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
        [Binding]
        public sealed class InjectAttribute : Attribute
        {
        }


    }
}
