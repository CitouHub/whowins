using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using AutoMapper;
using Plk.Blazor.DragDrop;
using BlazorPro.BlazorSize;

using VemVinner.Data;
using VemVinner.Domain;
using VemVinner.Service;

namespace VemVinner.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazorDragDrop();

            services.AddDbContext<WhoWinsDbContext>(options => options.UseSqlServer(Configuration["Database:ConnectionString"]));
            services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(Configuration["Database:ConnectionString"]));

            services
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddScoped<IResizeListener, ResizeListener>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IGroupService, GroupService>()
                .AddScoped<IGameService, GameService>()
                .AddScoped<IUserStatisticsService, UserStatisticsService>()
                .AddScoped<IAchievementService, AchievementService>()
                .AddScoped<IAlertService, AlertService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
