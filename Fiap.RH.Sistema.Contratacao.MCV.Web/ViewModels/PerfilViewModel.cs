using Fiap.RH.Sistema.Contratacao.Dominio;
using System.Collections.Generic;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels
{
    public class PerfilViewModel
    {
        #region VIEW PROPS
        public string Mensagem { get; set; }
        #endregion

        #region MODEL PROPS
        public int Id { get; set; }
        public string Descricao { get; set; }
        #endregion

        #region MODEL COLLECTIONS
        public ICollection<PerfilProfissional> Perfis { get; set; }
        #endregion
    }
}