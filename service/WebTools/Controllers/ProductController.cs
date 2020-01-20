using IhChegou.Domain.Contract.Product;
using IhChegou.Domain.Contract.Search;
using IhChegou.DTO.Product;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebTools.Controllers
{
    public class ProductController : Controller
    {
        public IProductDomain ProductDomain;
        public ISearchDomain SearchDomain;

        public ProductController(IProductDomain productDomain, ISearchDomain searchDomain)
        {
            ProductDomain = productDomain;
            SearchDomain = searchDomain;
        }

        // GET: Product/Ranking
        public ActionResult Ranking(int page = 0, int size = 50, string query = null)
        {
            List<DtoProduct> model = null;
            if (query != null)
            {
                model = ProductDomain.GetProductLike(query, page, size);
            }
            else
                model = ProductDomain.GetProductOrderByRank(page, size);
            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.Query = query;
            return View(model);
        }

        public ActionResult UpdateView(DtoProduct product)
        {
            ProductDomain.UpdateView(product);
            List<DtoProduct> model = null;
            if (ViewBag.Query != null)
            {
                model = ProductDomain.GetProductLike(ViewBag.Query, ViewBag.Page, ViewBag.Size);
            }
            else
                model = ProductDomain.GetProductOrderByRank(ViewBag.Page ?? 0, ViewBag.Size ?? 50);
            return View("Ranking",model);
        }


    }
}

