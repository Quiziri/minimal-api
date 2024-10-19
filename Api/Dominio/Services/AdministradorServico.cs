using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Infraestrutura.Db;
using MinimalAPI.Dominio.Interfaces;

namespace MinimalAPI.Dominio.Services
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _contexto;
        public AdministradorServico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public Administrador? BuscaPorId(int id)
        {
            return _contexto.Administradores.Where(a => a.Id == id).FirstOrDefault();
        }

        public Administrador Incluir(Administrador administrador)
        {
            _contexto.Administradores.Add(administrador);
            _contexto.SaveChanges();

            return administrador;
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            var adm = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
        }

        public List<Administrador> Todos(int? pagina)
        {
            var query = _contexto.Administradores.AsQueryable();

            int itensPorPagina = 10;

            if(pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }

            return query.ToList();
        }
    }
}