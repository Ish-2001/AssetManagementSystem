using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagementSystemUI.Controllers
{
    public class BookController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly BookService _bookService;

        public BookController(IHttpContextAccessor httpContextAccessor, BookService bookService)
        {
            _contextAccessor = httpContextAccessor;
            _bookService = bookService;
        }
      
        public async Task<IActionResult> GetAll()
        {
            return View(await _bookService.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookDTO book)
        {
            var result = _bookService.Add(book);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "Book has been added successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "Book with this serial number is already present in the system";

            return View();
        }

        public IActionResult Delete(string serialNumber)    
        {
            try
            {
                TempData["message"] = "The book has been deleted successfully";
                _bookService.Delete(serialNumber);
            }
            catch(Exception ex) {
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        public IActionResult Edit(BookDTO book)
        {
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(BookDTO book , string identityNumber)
        { 
            var result = _bookService.Edit(book,identityNumber);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The book has been updated successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "The book cannot be updated";
            return View();
        }

        public IActionResult Availability()
        {
            List<BookDTO> books =_bookService.Availability();
            return View(books);
        }
    }
}
