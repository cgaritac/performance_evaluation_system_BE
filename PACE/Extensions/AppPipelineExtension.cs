using Serilog;

namespace PACE.Extensions
{
    public static class AppPipelineExtension
    {
        public static IApplicationBuilder ConfigureAppPipeline(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorsMiddleware();

            if (env.IsEnvironment("Localhost"))
            {
                app.UseDeveloperExceptionPage(); //Delete at the end
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PACE API v1");
                });
            }
            else //Delete at the end
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseCustomExceptionMiddleware();

            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
