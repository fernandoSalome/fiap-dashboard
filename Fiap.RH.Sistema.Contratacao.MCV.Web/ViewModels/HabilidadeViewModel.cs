using Fiap.RH.Sistema.Contratacao.Dominio;
using System.Collections.Generic;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels
{
    public class HabilidadeViewModel
    {
        #region VIEW PROPS
        public string Mensagem { get; set; }
        #endregion

        #region MODEL PROPS
        public int Id { get; set; }

        public string Nome { get; set; }
        #endregion

        #region MODEL COLLECTION
        public ICollection<Habilidade> Habilidades { get; set; }
        #endregion
    }
}