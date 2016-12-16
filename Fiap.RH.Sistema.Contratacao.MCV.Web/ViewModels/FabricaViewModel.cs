using Fiap.RH.Sistema.Contratacao.Dominio;
using System.Collections.Generic;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels
{
    public class FabricaViewModel
    {
        #region VIEW PROPS
        public string Mensagem { get; set; }
        #endregion

        #region VIEW ESTATISTICAS
        public int Inscritos { get; set; }
        public decimal MediaGeral { get; set; }
        public IDictionary<string, int> Participantes { get; set; }
        public IDictionary<string, int> Publico { get; set; }
        public IDictionary<string, int> Presentes { get; set; }
        public IDictionary<string, int> Aprovados { get; set; }
        public IDictionary<string, int> AprovadosXCursos { get; set; }
        #endregion

        #region MODEL PROPS 
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal NotaCorte { get; set; }
        #endregion

        #region MODEL COLLECTIONS
        public ICollection<Aula> Aulas { get; set; }
        public ICollection<Fabrica> Fabricas { get; set; }
        #endregion
    }
}