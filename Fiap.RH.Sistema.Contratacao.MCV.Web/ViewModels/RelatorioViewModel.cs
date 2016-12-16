using Fiap.RH.Sistema.Contratacao.Dominio;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels
{
    public class RelatorioViewModel
    {
        #region VIEW PROPS
        public string Mensagem { get; set; }
        public SelectList Habilidades { get; set; }
        #endregion


        #region MODEL PROPS
        public int Id { get; set; }
        public int Faltas { get; set; }
        public int AderenciaComportamental { get; set; }
        public decimal DesempenhoNota { get; set; }
        public string Status { get; set; }
        public Candidato Candidato { get; set; }
        public Login Login { get; set; }
        #endregion


        #region PARTIAL
        [Required(ErrorMessage = "Selecione uma habilidade")]
        public int NovaHabilidadeId { get; set; }
        #endregion


        #region MODEL COLLECTION
        public ICollection<RelatorioHabilidade> HabilidadesDoCandidato { get; set; }
        #endregion
    }
}