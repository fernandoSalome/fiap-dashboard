using Fiap.RH.Sistema.Contratacao.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.RH.Sistema.Contratacao.Persistencia.Repositories
{
    public interface IRelatorioHabilidadeRepository : IGenericRepository<RelatorioHabilidade>
    {
        void RemoverPelaOrdem(int idOrdem);

        RelatorioHabilidade BuscaPelaOrdem(int idOrdem);
    }
}
