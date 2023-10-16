using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.BLL.Services;
using Sales.BLL.Services.ProductServices;
using Sales.BLL.Services.UserServices;
using Sales.Data.DataContext;
using Sales.Data.UnitOfWork;

namespace Sales.BLL
{
    public static class DependencyInjectionConfig
    {
        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<UserRoleService>();
            services.AddScoped<RoleService>();

            services.AddScoped<BankService>();
            services.AddScoped<BrandService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<PresentationService>();
            services.AddScoped<ProductService>();
            services.AddScoped<SubCategoryService>();
            services.AddScoped<TelephonyService>();
            services.AddScoped<UnitOfMeasureService>();
            services.AddScoped<VendorAddressService>();
            services.AddScoped<VendorBankAccountService>();
            services.AddScoped<VendorPhoneService>();
            services.AddScoped<VendorProductService>();
            services.AddScoped<VendorService>();
        }

        public static void AddAddDbContextPgSql(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<SalesDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DbConexion")));
        }
    }
}
