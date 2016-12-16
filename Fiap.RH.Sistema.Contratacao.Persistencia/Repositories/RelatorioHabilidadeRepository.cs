using Fiap.RH.Sistema.Contratacao.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Fiap.RH.Sistema.Contratacao.Persistencia.Repositories
{
    class RelatorioHabilidadeRepository : GenericRepository<RelatorioHabilidade>, IRelatorioHabilidadeRepository
    {
        public RelatorioHabilidadeRepository(Entities persistencia) : base(persistencia)
        {
        }

        public void RemoverPelaOrdem(int idOrdem)
        {
            IEnumerable<RelatorioHabilidade> elementos = BuscarPor(e => e.IdOrdem == idOrdem);
            _persistencia.Set<RelatorioHabilidade>().Remove(elementos.First());
        }

        public RelatorioHabilidade BuscaPelaOrdem(int idOrdem)
        {
            return BuscarPor(e => e.IdOrdem == idOrdem).First();
        }
    }
}
