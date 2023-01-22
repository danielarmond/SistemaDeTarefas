using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio



    {
        private readonly SistemaDeTarefasDbContext _dbContext;
        public UsuarioRepositorio(SistemaDeTarefasDbContext sistemaDeTarefasDbContext) 
        {
            _dbContext= sistemaDeTarefasDbContext;
        }

        public  async Task<Usuario> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Usuario>> BuscarTodosUsuario()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            _dbContext.SaveChanges();
            return usuario;
        }

        public async Task<Usuario> Atualizar(Usuario usuario, int id)
        {
            Usuario usuarioPorId = await BuscarPorId(id);
            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID:{id} não foi encontrado no banco de dados.");
            }
            usuarioPorId.Nome= usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioPorId);
            _dbContext.SaveChanges();
            return usuarioPorId;
        }
                    
        public async Task<bool> Deletar(int id)
        {
            Usuario usuarioPorId = await BuscarPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID:{id} não foi encontrado no banco de dados.");
            }
            _dbContext.Usuarios.Remove(usuarioPorId);
            _dbContext.SaveChanges();
            return true;
        }

        Task<Usuario> IUsuarioRepositorio.Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
