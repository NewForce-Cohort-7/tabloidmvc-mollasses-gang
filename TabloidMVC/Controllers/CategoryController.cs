using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController( ICategoryRepository categoryRepository)
        {
            
            _categoryRepository = categoryRepository;
        }

        // GET: CategoryController
        //public ActionResult Index()
        //{
        //    var categoryList = _categoryRepository.GetAll();
        //    //order list in alphabetical order
        //    categoryList.Sort((x, y) => string.Compare(x.Name, y.Name));
        //    return View(categoryList);
        //}


        public ActionResult Index()
        {
            // Retrieve the error message from TempData, if available
            string errorMessage = TempData["ErrorMessage"] as string;

            if (!string.IsNullOrEmpty(errorMessage))// if error message is not null or empty => assigns it to the ViewBag.ErrorMessage property
            {
                ViewBag.ErrorMessage = errorMessage; // The ViewBag property uses the dynamic type => assign values to it using any property name and access those values in the view i.e Pass the error message from edit action to the index view where the error can be displayed
            }

            // Retrieve the list of categories and pass it to the view

            var categoryList = _categoryRepository.GetAll();
            //order list in alphabetical order
            categoryList.Sort((x, y) => string.Compare(x.Name, y.Name));
            return View(categoryList);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                TempData["ErrorMessage"] = "Ooops. Category not found!";//TempData (a temporary storage mechanism in ASP.NET MVC).
                return RedirectToAction("Index"); // Redirect user to the Index action which displays the list of categories
            }
            return View(category);
        }

        //Need to also give Post Edit method the id as a parameter to identify which category should be updated in the repo.When the user submits the edit form, it includes the updated values of the category fields, except for the ID, since it's not part of the form fields provided to users. We need a way to associate the updated category object with its corresponding ID.

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRepository.Update(category);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {

                _categoryRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }


        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = _categoryRepository.GetCategoryById(id); 
            if (category == null)
            {
                return NotFound("Ooops, Category not found!");
            }

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }
    }
}
