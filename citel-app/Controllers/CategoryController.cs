﻿using System;
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
using Newtonsoft.Json;

namespace citelapp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private HttpClient httpClient;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpClient = httpClientFactory.CreateClient("citel-api");
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = new List<Category>();
            using (var response = await httpClient.GetAsync("category"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
            }
            return View(categories);
        }

        public ViewResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            string stringData = JsonConvert.SerializeObject(category);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8,"application/json");

            using (var response = await httpClient.PostAsync("category", contentData))
            {
                await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Category category = new Category();
            using (var response = await httpClient.GetAsync($"category/{id}/products"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<Category>(apiResponse);
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            string stringData = JsonConvert.SerializeObject(category);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8,"application/json");

            using (var response = await httpClient.PutAsync($"category/{id}", contentData))
            {
                await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var response = await httpClient.DeleteAsync($"category/{id}"))
            {
                await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }
    }
}