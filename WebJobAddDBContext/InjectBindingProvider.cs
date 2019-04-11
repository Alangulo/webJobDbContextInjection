using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;

namespace WebJobAddDBContext
{
    internal class InjectBindingProvider : IBindingProvider
    {
        private ServiceProvider serviceProvider;

        public InjectBindingProvider(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}

