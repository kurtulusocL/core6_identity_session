using Identity_Session.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Session.ViewComponents
{
    public class ProductHit:ViewComponent
    {
        readonly IProductService _productService;
        public ProductHit(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_productService.HitRead(id));
        }
    }
}
