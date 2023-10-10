﻿using farmacia.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace farmacia.Service.implements
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _Context;

        public ProdutoService(AppDbContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<Produto>> GettAll()
        {
            return await _Context.Produtos

                .ToListAsync();
        }

        public async Task<Produto?> Create(Produto produto)
        {
          
            await _Context.Produtos.AddAsync(produto);
            await _Context.SaveChangesAsync();

            return produto;
        }


        public async Task<Produto?> GetById(long id)
        {
            try
            {
                var Produto = await _Context.Produtos
                    
                    .FirstAsync(i => i.Id == id);

                return Produto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Produto>> GetByNome(string nome)
        {
            var Produto = await _Context.Produtos
                   
                   .Where(p => p.Nome.Contains(nome))
                   .ToListAsync();

            return Produto;
        }


        public async Task<Produto?> Update(Produto produto)
        {
            var ProdutoUpdate = await _Context.Produtos.FindAsync(produto.Id);

            if (ProdutoUpdate is null)
                return null;



            _Context.Entry(ProdutoUpdate).State = EntityState.Detached;
            _Context.Entry(produto).State = EntityState.Modified;
            await _Context.SaveChangesAsync();

            return produto;
        }
        public async Task Delete(Produto produto)
        {
            _Context.Produtos.Remove(produto);
            await _Context.SaveChangesAsync();
        }
    }
}