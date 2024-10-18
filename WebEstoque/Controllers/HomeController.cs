using Entities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebEstoque.Interfaces;

namespace WebEstoque.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoWeb produto;
        private readonly IEstoqueWeb estoque;

        public HomeController(IProdutoWeb ip, IEstoqueWeb est)
        {
            estoque = est;
            produto = ip;
        }


        public async Task<ActionResult> Index()
        {

            List<MovimentoEstoque> movEstoque =await estoque.ListarProdutosEstoque();



            string nome = "";
            int Quantidade = 0;
            decimal valor = 0;

            for (int i = 0; i < movEstoque.Count; i++)
            {
                if(movEstoque[i].Produto.Estoque > Quantidade)
                {
                    Quantidade = movEstoque[i].Produto.Estoque;
                    nome = movEstoque[i].Produto.NomeProduto;
                    valor =Convert.ToDecimal(movEstoque[i].ValorUnitario);
                }
            }

            var array = new
            {
                nome,
                Quantidade,
                valor
            };
            ViewBag.DadosdoProduto = JsonConvert.SerializeObject(array);
            return View(movEstoque);
        }




        // GET: ProdutosController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var produtos = await produto.SelectProdutoGetId(id); 
            if (produtos == null)
            {
                TempData["ErrorMessage"] = "Produto não encontrado.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = $"O {produtos.NomeProduto} foi salvo com sucesso!";

            return View(produtos);
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Produto produtoEdit)
        {
            if (id != produtoEdit.CodProduto)
            {
                TempData["ErrorMessage"] = "O ID do produto é inválido.";
                return View(produtoEdit);
            }

            if (!ModelState.IsValid)
            {
                return View(produtoEdit);
            }

            try
            {

                var estoqueEdit = await produto.SelectProdutoGetId(produtoEdit.CodProduto);
                if (estoqueEdit != null && estoqueEdit.NomeProduto.Equals(produtoEdit.NomeProduto))
                {
                    ModelState.AddModelError("NomeProduto", "O nome do produto já está em uso.");
                    return View(produtoEdit);
                }
                await produto.UpdateNome(produtoEdit);
                TempData["SuccessMessage"] = $"O {produtoEdit.NomeProduto} foi salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Erro ao salvar: {e.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

