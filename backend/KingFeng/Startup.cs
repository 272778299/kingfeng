using KingFeng.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFeng
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();


            //����Controllersȫ����AutoFac����
            //services.AddControllersWithViews().AddControllersAsServices()

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //AddSingleton���������ڣ���Ŀ���� - ��Ŀ�ر�   �൱�ھ�̬�� ֻ����һ��
            services.AddScoped<IConfigServices, ConfigServices>();

            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KingFeng", Version = "v1" });
                /*
                c.AddSecurityDefinition("Jwt�����֤", new OpenApiSecurityScheme()
                {
                    Scheme = "bearer",
                    //Description = "Authorization:Bearer {your JWT token}<br/><b>��Ȩ��ַ:/Base_Manage/Home/SubmitLogin</b>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });
                */
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP ClientRequest pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
             
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KingFeng v1");
                //c.RoutePrefix = string.Empty;
            });

            // ��������������Դ����
            app.UseCors(options =>
            {
                options.SetIsOriginAllowed(origin => true);
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowCredentials();
            });

            //����Session
            //app.UseSession();

            //app.UseHttpsRedirection(); //ǿ��ʹ��HttpsЭ��

            //��̬�ļ�
            app.UseStaticFiles();
            /*
            app.UseStaticFiles(new StaticFileOptions
              {
                  ServeUnknownFileTypes = true,
                  DefaultContentType = "application/octet-stream"
              })
            */
 
            app.UseRouting();

            app.UseAuthorization();

            app.UseFileServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
