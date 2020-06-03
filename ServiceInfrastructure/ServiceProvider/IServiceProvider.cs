using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider
{
    public interface IServiceProvider
    {
        TService GetService<TService>() where TService : class;
    }
}
