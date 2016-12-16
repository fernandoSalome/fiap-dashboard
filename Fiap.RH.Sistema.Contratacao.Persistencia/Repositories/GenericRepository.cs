using Fiap.RH.Sistema.Contratacao.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fiap.RH.Sistema.Contratacao.Persistencia.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected Entities _persistencia;

        public GenericRepository(Entities persistencia)
        {
            _persistencia = persistencia;
        }

        public virtual ICollection<T> BuscarPor(Expression<Func<T, bool>> filtro)
        {
            return _persistencia.Set<T>().Where(filtro).ToList();
        }

        public virtual T BuscarPorId(int id)
        {
            return _persistencia.Set<T>().Find(id);
        }

        public virtual void Cadastrar(T entidade)
        {
            _persistencia.Set<T>().Add(entidade);
        }

        public virtual void Editar(T entidade)
        {
            _persistencia.Entry(entidade).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual ICollection<T> Listar()
        {
            return _persistencia.Set<T>().ToList();
        }

        public virtual void Remover(int id)
        {
            var entidade = BuscarPorId(id);
            _persistencia.Set<T>().Remove(entidade);
        }

    }
}
