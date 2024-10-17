using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Entidades;

namespace MinimalAPI.Dominio.Services
{
    public interface IAdministradorServico
    {
        Administrador? Login(LoginDTO loginDTO);
        Administrador Incluir(Administrador administrador);
        Administrador? BuscaPorId(int id);
        List<Administrador> Todos(int? pagina);
    }
}