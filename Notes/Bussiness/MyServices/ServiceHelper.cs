using Bussiness.Abstract;
using Bussiness.Concrete;
using Data.Access.Abstract;
using Data.Access.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Bussiness.MyServices
{
    public static class ServiceHelper
    {
        public static void LoadServices(this IServiceCollection service)
        {
            service.AddSingleton<INotesRepository, NotesRepository>();
            service.AddSingleton<INotesService, NotesManager>();
        }
    }
}
