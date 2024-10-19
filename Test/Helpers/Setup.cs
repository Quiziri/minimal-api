using Microsoft.EntityFrameworkCore;
using MinimalAPI.Infraestrutura.Db;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MinimalAPI.Dominio.Services;
using MinimalAPI.Dominio.Interfaces;
using Test.Mocks;

namespace Test.Helpers;

public class Setup
{
    public const string PORT = "5001";
    public static TestContext testContext = default!;
    public static WebApplicationFactory<IStartup> http = default!;
    public static HttpClient client = default!;

    public static void ClassInit(TestContext testContext)
    {
        Setup.testContext = testContext;
        Setup.http = new WebApplicationFactory<IStartup>();

        Setup.http = Setup.http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", Setup.PORT).UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.AddScoped<IAdministradorServico, AdministradorServicoMock>();

                /*
                //== Caso queira deixar o teste com conexão diferente == 
                var conexao = "Server=localhost;Database=minimal_apitest;Uid=root;Pwt=***Jvqr0209";
                services.AddDbContext<DbContexto>(options =>
                {
                    options.UseMySql(conexao,ServerVersion.AutoDetect(conexao));
                });
                */
            });
        });

        Setup.client = Setup.http.CreateClient();
    }

    public static void ClassCleanup()
    {
        Setup.http.Dispose();
    }
}