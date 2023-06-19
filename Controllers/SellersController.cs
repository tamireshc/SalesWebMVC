using Microsoft.AspNetCore.Mvc;
using salesWebMvc.Models;
using salesWebMvc.Models.ViewModels;
using salesWebMvc.Services;
using salesWebMvc.Services.Exception;
using SQLitePCL;
using System.Diagnostics;

namespace salesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAll();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAll();
            var viewModel = new SellerFormViewModel() { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departmentService.FindAll();
                SellerFormViewModel viewModel = new SellerFormViewModel()
                {
                    Seller = seller,
                    Departments = departments
                };
                return View(viewModel);
            };
            await _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new {message = "Id not provided"});
                var seller = await _sellerService.FindById(id.Value);
                if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;
                return View(seller);         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.Remove(id);
                return RedirectToAction(nameof(Index));

            }catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            var seller = await _sellerService.FindById(id.Value);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" }); ;
            var seller = await _sellerService.FindById(id.Value);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;

            List<Department> departments = await _departmentService.FindAll();  
            SellerFormViewModel viewModel = new SellerFormViewModel() { 
                Seller = seller,
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departmentService.FindAll();
                SellerFormViewModel viewModel = new SellerFormViewModel()
                {
                    Seller = seller,
                    Departments = departments
                };
                return View(viewModel);
            };
            try
            {
                if (Id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Id mismatch" }); ;
                await _sellerService.Updade(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e) 
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch(DbConcurrencyException e) 
            {
                return RedirectToAction(nameof(Error), new { message =  e.Message}); ;
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { 
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
