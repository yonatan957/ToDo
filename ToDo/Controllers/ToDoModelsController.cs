using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ToDoModelsController : Controller
    {
        private readonly HttpClient _httpClient= new HttpClient();


        // GET: ToDoModels
        public async Task<IActionResult> Index()
        {
            List<ToDoModel> toDoModels = new List<ToDoModel>();
            for (int i = 1; i <= 30; i++)
            {
                var response = await _httpClient.GetAsync($"https://dummyjson.com/todos/{i}");
                response.EnsureSuccessStatusCode();
                toDoModels.Add(await response.Content.ReadFromJsonAsync<ToDoModel>());
            }             
            return View(toDoModels);
        }

        // GET: ToDoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync($"https://dummyjson.com/todos/{id}");
            var toDoModel = await response.Content.ReadFromJsonAsync<ToDoModel>();

            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }
        // GET: ToDoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Completed,userid")] ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                await _httpClient.PostAsJsonAsync("https://dummyjson.com/todos/add", toDoModel);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoModel);
        }

        //GET: ToDoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            //add to do
            var response = await _httpClient.GetAsync($"https://dummyjson.com/todos/{id}");
            return View(await response.Content.ReadFromJsonAsync<ToDoModel>());
        }

        //POST: ToDoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Completed,userid")] ToDoModel toDoModel)
        {
            if (id != toDoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _httpClient.PutAsJsonAsync($"https://dummyjson.com/todos/{id}", toDoModel);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDoModel);
        }

        // GET: ToDoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                await _httpClient.DeleteAsync($"https://dummyjson.com/todos/{id}");
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
