using Agreement.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agreement.Web.Controllers
{
    public class ProductController : Controller
    {
        #region variable declaration
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetProductList(string query)
        {
            var data = _unitOfWork.ProductRepository.FindBy(x => x.IsActive == true && x.ProductDescription.Contains(query))
                .Select(d => new Domain.Product.Product { Id = d.Id, ProductDescription = d.ProductDescription })
                .ToList();
            return Json(new { data });

        }

    }
}
