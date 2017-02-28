using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Domain.Entities;
using WebUI.Controllers;
using WebUI.Models;
using System.Linq;

namespace WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo paginginfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= paginginfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == paginginfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
        //extension method (because of this)
        public static MvcHtmlString PagedProductGrid(this HtmlHelper html, PagingInfo paging,
            IEnumerable<Product> products)
        {
            StringBuilder result = new StringBuilder();
            var gridProducts = products.OrderBy(p => p.ProductID)
                .Skip((paging.CurrentPage - 1) * paging.ItemsPerPage)
                .Take(paging.ItemsPerPage);

            
            foreach(var product in gridProducts)
            {
                TagBuilder h2 = new TagBuilder("h2");
                TagBuilder p = new TagBuilder("p");
                h2.InnerHtml = product.Name + " (" + product.Price.ToString("c") +")";
                p.InnerHtml = product.Description;

                TagBuilder div = new TagBuilder("div");
                div.InnerHtml = h2.ToString()+p.ToString();
                
                div.AddCssClass("well");
                TagBuilder br = new TagBuilder("br");
                result.Append(div.ToString()+br.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}