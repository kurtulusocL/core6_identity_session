using Identity_Session.Business.Abstract;
using Identity_Session.Business.Concrete;
using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Identity_Session.Business.DependencyResolver.DependencyInjection
{
    public static class DependencyContainer
    {
        public static void DependencyService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(opt => {
                opt.Password.RequireDigit = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequiredLength = 2;
                //opt.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddScoped<ICategoryDal, CategoryDal>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentDal, CommentDal>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICountryDal, CountryDal>();
            services.AddScoped<ICountryService, CountryManager>();
            services.AddScoped<IOrderDal, OrderDal>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IPictureDal, PictureDal>();
            services.AddScoped<IPictureService, PictureManager>();
            services.AddScoped<IProductDal, ProductDal>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProfilePhotoDal, ProfilePhotoDal>();
            services.AddScoped<IProfilePhotoService, ProfilePhotoManager>();
            services.AddScoped<IRoleDal, RoleDal>();
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<ISubcategoryDal, SubcategoryDal>();
            services.AddScoped<ISubcategoryService, SubcategoryManager>();
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IUserService, UserManager>();

            services.AddSingleton<IRecaptchaValidatorService, RecaptchaValidatorManager>();
        }
    }
}
