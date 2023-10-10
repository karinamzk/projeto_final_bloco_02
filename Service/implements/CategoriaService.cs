using farmacia.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace farmacia.Service.implements
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _Context;
        public CategoriaService(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<Categoria> Create(Categoria categoria)
        {
            await _Context.Categorias.AddAsync(categoria);
            await _Context.SaveChangesAsync();

            return categoria;
        }
        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _Context.Categorias
              .Include(c => c.Produtos)
              .ToListAsync();
        }

        public async Task<Categoria?> GetById(long id)
        {
            try
            {
                var Categoria = await _Context.Categorias
                     .Include(c => c.Produtos)
                     .FirstAsync(i => i.Id == id);

                return Categoria;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Categoria>> GetByTipo(string tipo)
        {
            var Categoria = await _Context.Categorias
                .Include(c => c.Produtos)
                .Where(c => c.Tipo.Contains(tipo))
                .ToListAsync();

            return Categoria;
        }

        public async Task<Categoria?> Update(Categoria categoria)
        {
            var CategoriUpdate = await _Context.Categorias.FindAsync(categoria.Id);

            if (CategoriUpdate is null)
                return null;

            _Context.Entry(CategoriUpdate).State = EntityState.Detached;
            _Context.Entry(categoria).State = EntityState.Modified;
            await _Context.SaveChangesAsync();

            return categoria;
        }
        public async Task Delete(Categoria categoria)
        {
            _Context.Categorias.Remove(categoria);
            await _Context.SaveChangesAsync();
        }
    }
}
