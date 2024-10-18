using Entities.Entities;
using Infra.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebEstoque.Interfaces;

namespace WebEstoque.Controllers
{
    public class MovimentoController : Controller
    {

        private MovimentoEstoqueRepository _iestoqueWeb = new MovimentoEstoqueRepository();
        private readonly IEstoqueWeb iestoque;
        public MovimentoController(IEstoqueWeb i)
        {
            iestoque = i;
        }


        public async Task<ActionResult> Index()
        {
            List<MovimentoEstoque> Movimento = await _iestoqueWeb.ListarEstoque_DatasEntradas_saida(1, DateTime.Now.AddMonths(-10), DateTime.Now);

            var alldates = Movimento.Select(m => m.DataMovimentacao.ToString("MM/yy"))
                                    .Distinct()
                                    .OrderBy(d => d)
                                    .ToList();
            
            var products = Movimento
                .GroupBy(m => m.Produto.NomeProduto)
                .Select(g => new
                {
                    NomeProduto = g.Key,
                    Quantidades = alldates.Select(date => g
                        .Where(m => m.DataMovimentacao.ToString("MM/yy") == date)
                        .Sum(m => (int?)m.Quantidade ?? 0))
                        .ToList(),
                    valorUnit = alldates.Select(date => g
                     .Where(m => m.DataMovimentacao.ToString("MM/yy") == date)
                     .Sum(m => (int?)m.ValorUnitario ?? 0)).ToList(),
                    valor = alldates.Select(date => g
                    .Where(m => m.DataMovimentacao.ToString("MM/yy") == date)
                   .Sum(m => (int?)m.ValorUnitario * m.Quantidade ?? 0)).ToList(),
                }).ToList();


            ViewBag.DadosIniciais = JsonConvert.SerializeObject(new { alldates, products });

            return View(Movimento);
        }


        // GET: Movimento/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: Movimento/Create
        public async Task<ActionResult> Create()
        {

            return View();
        }

        // POST: Movimento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovimentoEstoque estoque)
        {
            try
            {
                await iestoque.Add(estoque);
                if (estoque.notificacoes.Any())
                {
                    foreach (var item in estoque.notificacoes)
                    {
                        ModelState.AddModelError(item.nomePropriedade, item.mensagem);
                    }
                    return View("Create", estoque);
                }
            }
            catch
            {
                return View("Create", estoque);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Movimento/Edit/5

        public async Task<ActionResult> Edit(MovimentoEstoque estoque)
        {
            var estoqueEdit = await iestoque.Select(estoque);
            return View(estoqueEdit);
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MovimentoEstoque estoque)
        {
            try
            {
                await _iestoqueWeb.UpdateMovimentoEstoqueQuantidade(estoque);
                if (estoque.notificacoes.Any())
                {
                    foreach (var item in estoque.notificacoes)
                    {
                        ModelState.AddModelError(item.nomePropriedade, item.mensagem);
                    }

                    return View("Edit", estoque);
                }

            }
            catch
            {
                return View("Edit", estoque);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Movimento/Delete/5
        public async Task<ActionResult> Delete(int id, MovimentoEstoque estoque)
        {
            return View(await _iestoqueWeb.Select(estoque));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(MovimentoEstoque estoque)
        {
            try
            {
                var produtoDeletar = await _iestoqueWeb.Select(estoque);

                await _iestoqueWeb.DeleteMovimentoEstoque(estoque);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<JsonResult> ListarEstoque_DatasEntradas_saida(string tipo, DateTime init, DateTime fim)
        {
            int Entrada_Sainda = tipo == "Entrada" ? 1 : 2;

            List<MovimentoEstoque> Movimento = await _iestoqueWeb.ListarEstoque_DatasEntradas_saida(Entrada_Sainda, init.AddDays(01), fim.AddDays(DateTime.Now.Day));

            // Pega todas as datas únicas no intervalo, formatadas
            var alldates = Movimento.Select(m => m.DataMovimentacao.ToString("MM/yy"))
                                    .Distinct()
                                    .OrderBy(d => d)
                                    .ToList();

            // Agrupa os produtos e mapeia suas quantidades para cada data
            var products = Movimento
                .GroupBy(m => m.Produto.NomeProduto)
                .Select(g => new
                {
                    NomeProduto = g.Key,
                    Quantidades = alldates.Select(date => g
                        .Where(m => m.DataMovimentacao.ToString("MM/yy") == date)
                        .Sum(m => (int?)m.Quantidade ?? 0)).ToList(),
                    valorUnit = alldates.Select(date => g
                   .Where(m => m.DataMovimentacao.ToString("MM/yy") == date)
                    .Sum(m => (int?)m.ValorUnitario ?? 0)).ToList(),
                    valor = alldates.Select(date => g
                  .Where(m => m.DataMovimentacao.ToString("MM/yy") == date)
                 .Sum(m => (int?)m.ValorUnitario * m.Quantidade ?? 0)).ToList(),
                }).ToList();

            // Retorna o JSON com as datas e os produtos
            return Json(new { alldates, products }, JsonRequestBehavior.AllowGet);
        }



    }
}


