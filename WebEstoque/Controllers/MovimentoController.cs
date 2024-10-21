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
        private readonly IFornecedorWeb _fornecedor;
        private Fornecedor _fornecedorEntity;
        public MovimentoController(IEstoqueWeb i, IFornecedorWeb fornWeb)
        {
            iestoque = i;
            _fornecedor = fornWeb;
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



        // GET: Movimento/Create
        public ActionResult Create()
        {
            var movimentoEstoque = new MovimentoEstoque();
            return PartialView("Create", movimentoEstoque);
        }

        // POST: Movimento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovimentoEstoque estoque)
        {
            try
            {
                _fornecedorEntity = await _fornecedor.Select(estoque.Fornecedor.NomeFornecedor.ToLower());
                estoque.FkFornecedor = _fornecedorEntity == null ? await _fornecedor.AddFornecedor(estoque.Fornecedor.NomeFornecedor) : _fornecedorEntity.CodFornecedor;

                await iestoque.Add(estoque);

                if (estoque.notificacoes.Any())
                {
                    foreach (var item in estoque.notificacoes)
                    {
                        ModelState.AddModelError(item.nomePropriedade, item.mensagem);
                    }
                    return RedirectToAction("Index", "Home");
                }
                TempData["SuccessMessage"] = $"O Produto {estoque.Produto.NomeProduto} Foi Salvo com Sucesso !";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["ErrorMessage"] = $"Erro ao salvar :{e.Message}";


            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Movimento/CreateSaida
        public ActionResult CreateSaida()
        {
            MovimentoEstoque model = new MovimentoEstoque();

            return PartialView("CreateSaida", model);
        }

        // POST: Movimento/CreateSaida
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSaida(MovimentoEstoque estoque)
        {
            try
            {
                await iestoque.AddSaidaEstoque(estoque);
                if (estoque.notificacoes.Any())
                {
                    foreach (var item in estoque.notificacoes)
                    {
                        ModelState.AddModelError(item.nomePropriedade, item.mensagem);
                    }
                    TempData["SuccessMessage"] = "O Estoque foi atualizado com Sucesso!";
                    return RedirectToAction("Index", "Home");
                }
                TempData["SuccessMessage"] = $"O Produto {estoque.Produto.NomeProduto} Foi dado baixa com Sucesso!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["ErrorMessage"] = $"Erro ao salvar :{e.Message}";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
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

        public async Task<ActionResult> TabelaMovimento()
        {
            List<MovimentoEstoque> Movimento = await iestoque.Listar();
            return View(Movimento);
        }

        [HttpGet]
        public async Task<ActionResult> ListarEstoque_DatasEntradas_saidaTabelaMov(string pesquisa, string tipo, DateTime init, DateTime fim)
        {
            List<MovimentoEstoque> Movimento = null;
            int Entrada_Saida = tipo == "Entrada" ? 1 : 2;
            Movimento = await _iestoqueWeb.ListarEstoque_DatasEntradas_saida(Entrada_Saida, init.AddDays(1), fim.AddDays(1));
            return Json(new { products = Movimento }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ProcurarProdutoPorNome(string pesquisa)
        {
            List<MovimentoEstoque> Movimento = null;
            Movimento = await _iestoqueWeb.ProcurarPeloNome(pesquisa);

            return Json(new { products = Movimento }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ListarEstoque_DatasEntradas_saida(string tipo, DateTime init, DateTime fim)
        {
            int Entrada_Sainda = tipo == "Entrada" ? 1 : 2;

            List<MovimentoEstoque> Movimento = await _iestoqueWeb.ListarEstoque_DatasEntradas_saida(Entrada_Sainda, init.AddDays(01), fim.AddDays(DateTime.Now.Day));

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
                        .Sum(m => (int?)m.Quantidade ?? 0)).ToList(),
                    valorUnit = alldates.Select(date => g
                   .Where(m => m.DataMovimentacao.ToString("MM/yy") == date)
                    .Sum(m => (int?)m.ValorUnitario ?? 0)).ToList(),
                    valor = alldates.Select(date => g
                  .Where(m => m.DataMovimentacao.ToString("MM/yy") == date)
                 .Sum(m => (int?)m.ValorUnitario * m.Quantidade ?? 0)).ToList(),
                }).ToList();
            return Json(new { alldates, products }, JsonRequestBehavior.AllowGet);
        }
    }
}


