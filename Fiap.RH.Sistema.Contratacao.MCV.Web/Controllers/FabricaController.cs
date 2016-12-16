using Fiap.RH.Sistema.Contratacao.Dominio;
using Fiap.RH.Sistema.Contratacao.MCV.Web.ViewModels;
using Fiap.RH.Sistema.Contratacao.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fiap.RH.Sistema.Contratacao.MCV.Web.Controllers
{
    public class FabricaController : Controller
    {
        #region FIELDS
        private UnitOfWork _unit = new UnitOfWork();
        #endregion


        #region GET
        [HttpGet]
        public ActionResult Cadastrar(string mensagem)
        {
            var fabricaViewModel = new FabricaViewModel()
            {
                Mensagem = mensagem
            };
            return View(fabricaViewModel);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var fabricaViewModel = new FabricaViewModel();
            try
            {
                fabricaViewModel.Fabricas = _unit.FabricaRepository.Listar();
            }
            catch (Exception e)
            {
                fabricaViewModel.Mensagem = "Erro ao buscar as fabricas no banco de dados: " + e.Message;
            }
            return View(fabricaViewModel);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var fabricaViewModel = new FabricaViewModel();
            Fabrica fabrica = null;
            try
            {
                fabrica = _unit.FabricaRepository.BuscarPorId(id);
                fabricaViewModel.Nome = fabrica.Nome;
                fabricaViewModel.NotaCorte = fabrica.NotaCorte;
                fabricaViewModel.Id = id;
            }
            catch (Exception e)
            {
                fabricaViewModel.Mensagem = "Erro ao buscar a fabrica no banco de dados para editar: " + e.Message;
            }

            return View(fabricaViewModel);
        }

        [HttpGet]
        public ActionResult VerEstatisticas(int id)
        {
            ICollection<Candidato> candidatos = null;
            FabricaViewModel fabricaViewModel = new FabricaViewModel();

            try
            {
                candidatos = _unit.CandidatoRepository.BuscarPor(c => c.IdFabrica == id);
                var fabrica = _unit.FabricaRepository.BuscarPorId(id);
                fabricaViewModel.Nome = fabrica.Nome;
                fabricaViewModel.NotaCorte = fabrica.NotaCorte;
                GerarEstatisticas(candidatos, fabricaViewModel);
            }
            catch (Exception e)
            {
                fabricaViewModel.Mensagem = "Erro ao gerar as estatisticas da Fabrica selecionada: " + e.Message;
            }
            
            return View(fabricaViewModel);
        }
        #endregion


        #region POST
        [HttpPost]
        public ActionResult Cadastrar(FabricaViewModel fabricaViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(IsFabricaDuplicada(fabricaViewModel))
                    {
                        fabricaViewModel.Mensagem = "Já existe uma fabrica com este nome!";
                        return View(fabricaViewModel);
                    }

                    var fabrica = new Fabrica()
                    {
                        Nome = fabricaViewModel.Nome,
                        NotaCorte = fabricaViewModel.NotaCorte
                    };

                    _unit.FabricaRepository.Cadastrar(fabrica);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    fabricaViewModel.Mensagem = "Erro ao cadastrar fabrica: " + e.Message;
                    return View(fabricaViewModel);
                }

            }
            else return View(fabricaViewModel);

            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Editar(FabricaViewModel fabricaViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IsFabricaDuplicada(fabricaViewModel))
                    {
                        fabricaViewModel.Mensagem = "Já existe uma fabrica com este nome!";
                        return View(fabricaViewModel);
                    }

                    var fabrica = _unit.FabricaRepository.BuscarPorId(fabricaViewModel.Id);
                    fabrica.Id = fabricaViewModel.Id;
                    fabrica.Nome = fabricaViewModel.Nome;

                    if(fabrica.NotaCorte != fabricaViewModel.NotaCorte)
                        CandidatoController.RevalidaAprovacoes(fabricaViewModel.NotaCorte, _unit);

                    _unit.FabricaRepository.Editar(fabrica);
                    _unit.Save();
                }
                catch (Exception e)
                {
                    fabricaViewModel.Mensagem = "Erro ao editar fabrica: " + e.Message;
                    return View(fabricaViewModel);
                }
            }
            else return View(fabricaViewModel);

            return RedirectToAction("Listar");
        }
        #endregion


        #region STATIC
        public static bool IsCandidatoAprovado(decimal notaCandidato, int idFabrica, UnitOfWork unit)
        {
            Fabrica fabrica = null;
            try
            {
                fabrica = unit.FabricaRepository.BuscarPorId(idFabrica);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar a fábrica no banco de dados para recuperar a nota de corte: " + e.Message);
            }

            if (notaCandidato >= fabrica.NotaCorte) return true;
            else return false;
        }
        #endregion


        #region PRIVATE
        private bool IsFabricaDuplicada(FabricaViewModel fabricaViewModel)
        {
            var fabricas = _unit.FabricaRepository.BuscarPor(f => f.Nome == fabricaViewModel.Nome);
            if (fabricas.Any() && !(fabricas.First().Id == fabricaViewModel.Id)) return true;
            return false;
        }

        private FabricaViewModel GerarEstatisticas(ICollection<Candidato> candidatos, FabricaViewModel f)
        {
            // Inilicializadores
            f.Participantes = new Dictionary<string, int>();
            f.Publico = new Dictionary<string, int>();
            f.Presentes = new Dictionary<string, int>();
            f.Aprovados = new Dictionary<string, int>();
            f.AprovadosXCursos = new Dictionary<string, int>();
            f.Aprovados.Add("Aprovados", 0);
            f.Aprovados.Add("Reprovados", 0);
            f.Inscritos = candidatos.Count;
            f.Presentes.Add("Presentes", 0);
            f.Presentes.Add("Ausentes", 0);
            f.Publico.Add("Masculino", 0);
            f.Publico.Add("Feminino", 0);
            List<decimal> notas = new List<decimal>();

            // Calcula cada candidato
            foreach (Candidato c in candidatos)
            {
                if (c.ProvaTecnica == true && c.Nota > 0)
                {
                    //adiciona presentes e suas notas
                    f.Presentes["Presentes"]++;
                    notas.Add((decimal)c.Nota);

                    // Aprovado ou reprovado? Se true adiciona em Aprovados X Curso, sem duplicar o curso no Map (dictionary)
                    if (c.Aprovado == true)
                    {
                        f.Aprovados["Aprovados"]++;
                        if(f.AprovadosXCursos.ContainsKey(c.Curso.Nome)) f.AprovadosXCursos[c.Curso.Nome]++;
                        else f.AprovadosXCursos.Add(c.Curso.Nome, 1);
                    }
                    else f.Aprovados["Reprovados"]++;
                }
                // se nao fez a prova tecnica estava ausente
                else f.Presentes["Ausentes"]++;

                // Adiciona participante sem duplicar o curso no Map (dictionary)
                if(f.Participantes.ContainsKey(c.Curso.Nome)) f.Participantes[c.Curso.Nome]++;
                else f.Participantes.Add(c.Curso.Nome, 1);

                // Adiciona como Feminino ou Masculino true = Masculino
                if (c.Publico == true) f.Publico["Masculino"]++;
                else f.Publico["Feminino"]++;
            }

            // Calcula Media Geral
            var soma = 0m;
            notas.ForEach(n => soma += n);
            f.MediaGeral = soma / notas.Count;

            // Retorna View Model populado
            return f;
        }
        #endregion


        #region OVERRIDE
        protected override void Dispose(bool disposing)
        {
            _unit.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}