using GamerWeb.Business.Abstract;
using GamerWeb.Business.Concrete;
using GamerWeb.DataAccess.Abstract;
using GamerWeb.DataAccess.EntityFramework;
using GamerWeb.DataAccess.Repositories;

namespace GamerWeb.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IContactDal, EfContactDal>();

            services.AddScoped<IContactUsService, ContactUsManager>();
            services.AddScoped<IContactUsDal, EfContactUsDal>();

            services.AddScoped<IGameService, GameManager>();
            services.AddScoped<IGameDal, EfGameDal>();

            services.AddScoped<INewsService, NewsManager>();
            services.AddScoped<INewsDal, EfNewsDal>();

            services.AddScoped<IReviewService, ReviewManager>();
            services.AddScoped<IReviewDal, EfReviewDal>();

            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EfCommentDal>();

            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
        }
    }
}