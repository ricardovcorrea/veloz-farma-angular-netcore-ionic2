using IhChegou.Domain.Contract.Product;
using IhChegou.Domain.Contract.Search;
using IhChegou.DTO.Product;
using IhChegou.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTools.Models.Products;

namespace WebTools.Controllers
{
    public class SkuController : Controller
    {
        public IProductDomain ProductDomain;
        public ISearchDomain SearchDomain;

        public SkuController(IProductDomain productDomain, ISearchDomain searchDomain)
        {
            ProductDomain = productDomain;
            SearchDomain = searchDomain;
        }
        // GET: Sku
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Edit(int id = 1)
        {
            var model = ProductDomain.GetSku(id);
            return View(model);
        }

        public ActionResult List(int page = 0, int size = 50)
        {
            var model = ProductDomain.GetAllSkusWithoutImage(page, size);
            @ViewBag.Page = page;
            return View(model);
        }

        public ActionResult RemoveImages(List<RemoveImageModel> model)
        {
            ProductDomain.RemoveSkuImages(model.Where(i => i.ToDelete == true).Select(i => i.Id)?.ToList());
            @ViewBag.Page = model.FirstOrDefault()?.Page;

            return View("List", ProductDomain.GetAllSkusWithoutImage(model.FirstOrDefault()?.Page ?? 0, 50));
        }

        public ActionResult Update(DtoSku model, HttpPostedFileBase UploadImage)
        {
            var S3 = new IhChegou.Tools.AmazonApi.S3.FileUploader();
            var image = S3.UploadFile(UploadImage.InputStream, Guid.NewGuid().ToString() + UploadImage.FileName);
            model.Image = image;
            ProductDomain.SaveOrUpdate(model);
            return View("Edit", model);
        }

        public ActionResult Search(string query)
        {
            var model = SearchDomain.Fulltext(new DtoSearchRequest()
            {
                Query = query
            });
            return View("index", model);
        }

    }
}
