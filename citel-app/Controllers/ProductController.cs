using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using citelapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace citelapp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private HttpClient httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpClient = httpClientFactory.CreateClient("citel-api");
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();
            using (var response = await httpClient.GetAsync("product/category"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
            }
            return View(products);
        }

        public async Task<IActionResult> Add()
        {
            List<Category> categories = new List<Category>();
            using (var response = await httpClient.GetAsync("category"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
            }
            ViewBag.Categories = new SelectList(
                categories,
                "Id",
                "Name"
            );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            string stringData = JsonConvert.SerializeObject(product);
            var contentData = new StringContent(stringData, Encoding.UTF8,"application/json");

            using (var response = await httpClient.PostAsync("product", contentData))
            {
                await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Product product = new Product();
            using (var response = await httpClient.GetAsync($"product/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<Product>(apiResponse);
            }
            
            List<Category> categories = new List<Category>();
            using (var response = await httpClient.GetAsync("category"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
            }
            ViewBag.Categories = new SelectList(
                categories,
                "Id",
                "Name"
            );
            
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product)
        {
            string stringData = JsonConvert.SerializeObject(product);
            var contentData = new StringContent(stringData, Encoding.UTF8,"application/json");
            using (var response = await httpClient.PutAsync($"product/{id}", contentData))
            {
                await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var response = await httpClient.DeleteAsync($"product/{id}"))
            {
                await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }
    }
}