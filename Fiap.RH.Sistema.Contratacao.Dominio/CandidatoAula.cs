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
    
    public partial class CandidatoAula
    {
        public int IdFabrica { get; set; }
        public int IdAula { get; set; }
        public int IdCandidato { get; set; }
        public bool Presenca { get; set; }
    
        public virtual Aula Aula { get; set; }
        public virtual Candidato Candidato { get; set; }
    }
}
