using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebEstoque.Interfaces;

namespace WebEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoWeb produto;

        public ProdutoController (IProdutoWeb ip)
        {
            produto = ip;
        }
        public  async Task<ActionResult> Index()
        {
            List<Produto> _produto =await produto.SelectProduto();
            
            return View(_produto);
        }

        // GET: ProdutosController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var produtos = await produto.SelectProdutoGetId(id); // Busque o produto pelo ID
            if (produtos == null)
            {
                TempData["ErrorMessage"] = "Produto não encontrado.";
                return RedirectToAction(nameof(Index));
            }

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
                if (estoqueEdit != null && estoqueEdit.CodProduto != id)
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
                return View(produtoEdit);
            }
        }
    }
}
