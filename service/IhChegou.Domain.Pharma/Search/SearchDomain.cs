using IhChegou.Domain.Contract.Search;
using IhChegou.DTO.Search;
using IhChegou.Repository.Contract;
using IhChegou.Repository.Pharma.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IhChegou.Domain.Pharma.Search
{
    public class SearchDomain : ISearchDomain
    {
        private IProductRepository Query { get; set; }
        private ISearchRepository SearchRepository { get; set; }

        public SearchDomain(IProductRepository productQueries, ISearchRepository searchRepository)
        {
            Query = productQueries;
            SearchRepository = searchRepository;
        }
        public List<string> SetProductsOnElastic()
        {
            return SearchRepository.IndexAllProducts();
        }

        public DtoSearchResult Fulltext(DtoSearchRequest request)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var result = SearchRepository.Fulltext(request);
            watch.Stop();
            result.QueryTime = $"{watch.ElapsedMilliseconds} ms";
            return result;
        }

        public DtoCategoryTree GetCategoryTree()
        {
            var catstring = Query.GetAllSearchCategories();

            var tree = new DtoCategoryTree();

            foreach (var cat in catstring)
            {
                tree.Categories = tree.Categories ?? new List<DtoCategoryTreeItem>();

                var categories = cat.Split('|');

                var nv1 = categories.ElementAtOrDefault(0);
                var nv2 = categories.ElementAtOrDefault(1);
                var nv3 = categories.ElementAtOrDefault(2);

                if (nv1 != null)
                {
                    var realnv1 = tree.Categories.Where(i => i.Name == nv1).SingleOrDefault();
                    if (realnv1 == null)
                    {
                        realnv1 = new DtoCategoryTreeItem();
                        realnv1.Name = nv2;
                    }

                    realnv1.Name = nv1;

                    if (nv2 != null)
                    {
                        realnv1.SubCategories = realnv1.SubCategories ?? new List<DtoCategoryTreeItem>();
                        var realnv2 = realnv1.SubCategories.Where(i => i.Name == nv2).SingleOrDefault();
                        if (realnv2 == null)
                        {
                            realnv2 = new DtoCategoryTreeItem();
                            realnv2.Name = nv2;
                        }
                        if (nv3 != null)
                        {
                            realnv2.SubCategories = realnv2.SubCategories ?? new List<DtoCategoryTreeItem>();
                            var realnv3 = realnv2.SubCategories.Where(i => i.Name == nv3).SingleOrDefault();
                            if (realnv3 == null)
                            {
                                realnv3 = new DtoCategoryTreeItem();
                                realnv3.Name = nv3;
                            }
                            if (!realnv2.SubCategories.Contains(realnv3))
                                realnv2.SubCategories.Add(realnv3);
                        }
                        if (!realnv1.SubCategories.Contains(realnv2))
                            realnv1.SubCategories.Add(realnv2);
                    }
                    if (!tree.Categories.Contains(realnv1))
                        tree.Categories.Add(realnv1);
                }
            }
            return tree;
        }
    }
}