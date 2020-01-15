using Core.DTOs;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }

        public ViewResult Index()
        {
            var products = _productService.GetAll();

            return View(products);
        }

        public ViewResult Edit(int productId)
        {
            var product = _productService.GetById(productId);

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                _productService.SaveOrder(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new ProductDto());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var product = _productService.GetById(productId);
            ProductDto deletedProduct = _productService.Remove(product);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }

            return RedirectToAction("Index");
        }
    }
}
