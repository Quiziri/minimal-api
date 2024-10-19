using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Services;
using MinimalAPI.Infraestrutura.Db;

namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        var configuration = builder.Build();
        
        return new DbContexto(configuration);
    }

    [TestMethod]
    public void TestandoSalvarAdministrador()
    {
        //Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "adm";

        var AdministradorServico = new AdministradorServico(context);

        //Act
        AdministradorServico.Incluir(adm);
        var admDoBanco = AdministradorServico.BuscaPorId(adm.Id);

        //Assert
        Assert.AreEqual(1, admDoBanco?.Id);
    }
}