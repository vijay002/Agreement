using Agreement.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agreement.Web.Controllers
{
    public class ProductGroupController : Controller
    {
        #region variable declaration
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public ProductGroupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetProductGroupList(string query)
        {
            var data = _unitOfWork.ProductGroupRepository.FindBy(x => x.IsActive == true && x.GroupDescription.Contains(query))
                .Select(d => new Domain.Product.ProductGroup { Id = d.Id, GroupDescription = d.GroupDescription })
                .ToList();
            return Json(new { data });

        }
    }
}
