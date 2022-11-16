using Agreement.Services;
using Agreement.Web.Models;
using Agreement.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Agreement.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IHttpContextAccessor _httpContextAccessor { get; set; }
        private readonly HttpContext _httpContext;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,
            IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpContext = _httpContextAccessor.HttpContext;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public JsonResult LoadData(JQueryDataTableParamModel param)
        {
            try
            {
                var sortColumnIndex = _httpContext.Request.Form["iSortCol_0"];
                int StartIndex = param.iDisplayStart + 1;
                int EndIndex = param.iDisplayStart + param.iDisplayLength;

                string strWhere = string.Empty;
                string strSortOrder = string.Empty;
                string sortColumnName = "";// Convert.ToString(_httpContext.Request.Form["sColumns"][0]).ToString().Split(',')[sortColumnIndex]);
                string sortDirection = Convert.ToString(_httpContext.Request.Form["sSortDir_0"]);

                int totalRecords = 0;
                var agreementList = _unitOfWork.AgreementRepository.GetAllAgreement(StartIndex, EndIndex, strSortOrder, strWhere, out totalRecords);
                
                var arrydata = (from c in agreementList
                                select new object[]
                                {
                                    c.Id,
                                    c.ProductId,
                                    c.ProductGroupId,
                                    c.EffectiveDate,
                                    c.ExpirationDate,
                                    c.ProductPrice,
                                    c.NewPrice,
                                    null
                                });



                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalDisplayRecords = totalRecords != null && totalRecords > 0 ? totalRecords : 0,
                    iTotalRecords = totalRecords != null && totalRecords > 0 ? totalRecords : 0,
                    aaData = arrydata,
                    strSortOrder = strSortOrder,
                    whereCondition = strWhere
                });
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _unitOfWork.AgreementRepository.FindBy(q => q.Id == id).FirstOrDefault();
            AgreementViewModel model = new AgreementViewModel();



            model.ProductList = new List<SelectListItem>();

            model.ProductList = _unitOfWork.ProductRepository.FindBy(x => x.IsActive == true).OrderBy(m => m.ProductDescription).Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.ProductDescription
            }).ToList();

            model.ProductGroupList = _unitOfWork.ProductGroupRepository.GetAll().OrderBy(m => m.GroupDescription).Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.GroupDescription
            }).ToList();

            if (entity != null)
            {
                model.Id = entity.Id;
                model.IsActive = entity.IsActive;
                model.NewPrice = entity.NewPrice;
                model.ProductGroupId = entity.ProductGroupId;
                model.ProductId = entity.ProductId;
                model.ProductPrice = entity.ProductPrice;
                model.EffectiveDate = entity.EffectiveDate;
                model.ExpirationDate = entity.ExpirationDate;
            }


            return PartialView("_AddEditAgreement", model);
        }

        [HttpPost]
        public IActionResult EditAgreementSave(AgreementViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal principal = this.User as ClaimsPrincipal;
                if (null != principal)
                {
                    foreach (Claim claim in principal.Claims)
                    {
                        Console.WriteLine("CLAIM TYPE: " + claim.Type + "; CLAIM VALUE: " + claim.Value + "</br>");
                    }
                }


                var entity = _unitOfWork.AgreementRepository.FindBy(q => q.Id == viewmodel.Id).FirstOrDefault();
                if (entity == null)
                    entity = new Domain.Product.Agreement();

                    entity.Id = viewmodel.Id;
                    entity.IsActive = viewmodel.IsActive;
                    entity.NewPrice = viewmodel.NewPrice;
                    entity.ProductGroupId = viewmodel.ProductGroupId;
                    entity.ProductId = viewmodel.ProductId;
                    entity.ProductPrice = viewmodel.ProductPrice;
                    entity.EffectiveDate = viewmodel.EffectiveDate;
                    entity.ExpirationDate = viewmodel.ExpirationDate;


                    if (entity.Id > 0)
                    {
                        _unitOfWork.AgreementRepository.Update(entity);
                    }
                    else
                    {
                        _unitOfWork.AgreementRepository.Add(entity);
                    }
                    _unitOfWork.AgreementRepository.Commit();

                    return Json(new { success = true, message = String.Format("Success_save", "Agreement") });
                
            }
            return Json(new { success = false, message = String.Format("Message_Error", "Agreement") });
        }

        public JsonResult Delete(int id)
        {
            try
            {
                //var project = 
                _unitOfWork.AgreementRepository.DeleteWhere(q => q.Id == id);
                _unitOfWork.AgreementRepository.Commit();
                return Json(new { success = true, message = String.Format("Success_Delete", "Agreement") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error" });
            }
        }



    }
}
