Imports DevExpress.AspNetCore
Imports DevExpress.DashboardAspNetCore
Imports DevExpress.DashboardWeb
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.FileProviders
Imports Microsoft.Extensions.Hosting

Namespace WebDashboardAspNetCore3

    Public Class Startup

        Public Sub New(ByVal configuration As IConfiguration, ByVal hostingEnvironment As IWebHostEnvironment)
            Me.Configuration = configuration
            FileProvider = hostingEnvironment.ContentRootFileProvider
        End Sub

        Public ReadOnly Property Configuration As IConfiguration

        Public ReadOnly Property FileProvider As IFileProvider

        ' This method gets called by the runtime. Use this method to add services to the container.
        Public Sub ConfigureServices(ByVal services As IServiceCollection)
            services.AddDevExpressControls().AddControllersWithViews().AddDefaultDashboardController(Sub(configurator)
                configurator.SetDashboardStorage(New DashboardFileStorage(FileProvider.GetFileInfo("Data/Dashboards").PhysicalPath))
                configurator.SetConnectionStringsProvider(New DashboardConnectionStringsProvider(Configuration))
            End Sub)
        End Sub

        ' This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        Public Sub Configure(ByVal app As IApplicationBuilder, ByVal env As IWebHostEnvironment)
            If env.IsDevelopment() Then
                app.UseDeveloperExceptionPage()
            Else
                app.UseExceptionHandler("/Home/Error")
                ' The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts()
            End If

            app.UseHttpsRedirection()
            app.UseStaticFiles()
            app.UseDevExpressControls()
            app.UseRouting()
            app.UseAuthorization()
            app.UseEndpoints(Sub(endpoints)
                EndpointRouteBuilderExtension.MapDashboardRoute(endpoints, "api/dashboards")
                endpoints.MapControllerRoute(name:="default", pattern:="{controller=Home}/{action=Index}/{id?}")
            End Sub)
        End Sub
    End Class
End Namespace
