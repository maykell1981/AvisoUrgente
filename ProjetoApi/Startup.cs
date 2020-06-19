using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoApi.Models;

namespace ProjetoApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string ContentRoot { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ContentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var conexaoArquivoUrgnete = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AvisoUrgenteContext>(options => options.UseSqlite(conexaoArquivoUrgnete));

            services.AddCors();
            services.AddControllers();


            services.AddSwaggerGen(n =>
            {
                n.SwaggerDoc("v1", new OpenApiInfo { Title = "Aviso Urgente - Teste Api", Version = "v1" });



                // Defina o caminho dos comentários para o Swagger JSON e a interface do usuário.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                n.IncludeXmlComments(xmlPath);

            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //var context = serviceScope.ServiceProvider.GetRequiredService<ContextoBd>();
                //inicializaBd.Inicializar(context);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(n =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(n.RoutePrefix) ? "." : "..";
                n.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Aviso Urgente - Teste Api V1");
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(n => n.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
