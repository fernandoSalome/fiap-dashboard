//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fiap.RH.Sistema.Contratacao.Dominio
{
    using System;
    using System.Collections.Generic;
    
    public partial class AvaliacaoComportamental
    {
        public int Id { get; set; }
        public decimal JobCoach { get; set; }
        public decimal Video { get; set; }
        public decimal Redacao { get; set; }
        public decimal Linkedin { get; set; }
        public decimal Media { get; set; }
        public decimal AderenciaJobCoach { get; set; }
    
        public virtual Candidato Candidato { get; set; }
    }
}
