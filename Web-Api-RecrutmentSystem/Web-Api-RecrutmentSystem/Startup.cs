using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecrutmentSystem.Data;
using RecrutmentSystem.Infrastructures;
using RecrutmentSystem.Services.Candidates;
using RecrutmentSystem.Services.Interviews;
using RecrutmentSystem.Services.Jobs;
using RecrutmentSystem.Services.Recruiters;
using RecrutmentSystem.Services.Skills;

namespace RecrutmentSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddDbContext<RecrutmentSystemDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IRecruitersService, RecruitersService>();
            services.AddTransient<ICandidatesService, CandidatesService>();
            services.AddTransient<ISkillsService, SkillsService>();
            services.AddTransient<IJobsService, JobsService>();
            services.AddTransient<IInterviewsService, InterviewsService>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
