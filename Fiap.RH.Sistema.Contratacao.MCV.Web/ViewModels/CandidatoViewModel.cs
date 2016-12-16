using Fiap.RH.Sistema.Contratacao.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels
{
    public class CandidatoViewModel
    {
        #region Mensagem
        public string Mensagem { get; set; }
        #endregion

        #region definir

        public ICollection<Candidato> Candidatos { get; set; }
        public string NomeBusca { get; set; }
        public SelectList FabricaSelectList { get; set; }
        public SelectList UnidadeSelectList { get; set; }
        public SelectList PerfilSelectList { get; set; }
        public SelectList CursoSelectList { get; set; }
        [Display(Name = "Fábrica")]
        public int FabricaId { get; set; }
        [Display(Name ="Unidade")]
        public int UnidadeId { get; set; }
        [Display(Name = "Perfil Profissional")]
        public int PerfilId { get; set; }
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        #endregion

        #region CandidatoProperties
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Display(Name = "Público")]
        public bool Publico { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public decimal? Residencial { get; set; }

        public decimal? Celular { get; set; }

        [Display(Name = "C.V.")]
        public bool Cv { get; set; }
        [Display(Name = "Confirmação")]
        public bool Confirmacao { get; set; }
        [Display(Name="Prova Técnica")]
        public bool ProvaTecnica { get; set; }

        public decimal Nota { get; set; }

        public bool Aprovado { get; set; }
        [Display(Name = "RM")]
        public decimal? Rm { get; set; }
        #endregion




    }
}