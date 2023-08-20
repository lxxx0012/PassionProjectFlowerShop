using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProjectFlowerShop.Models;
using PassionProjectFlowerShop.Models.ViewModels;
using System.Web.Script.Serialization;


namespace PassionProjectFlowerShop.Controllers
{
    public class FlowerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static FlowerController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };

            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }

        // GET: Animal/List
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of animals
            //curl https://localhost:44324/api/flowerdata/listflowers


            string url = "flowerdata/listflowers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<FlowerDto> flowers = response.Content.ReadAsAsync<IEnumerable<FlowerDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(animals.Count());


            return View(flowers);
        }

        // GET: Flower/Details/5
        public ActionResult Details(int id)
        {
            DetailsFlower ViewModel = new DetailsFlower();

            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44324/api/animaldata/findanimal/{id}

            string url = "flowerdata/findflower/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            FlowerDto SelectedFlower = response.Content.ReadAsAsync<FlowerDto>().Result;
            Debug.WriteLine("flower received : ");
            Debug.WriteLine(SelectedFlower.FlowerName);

            ViewModel.SelectedFlower = SelectedFlower;

            //show occasion for the flower
            url = "occasiondata/listoccasionforflowerl/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<OccasionDto> SpecialOccasions = response.Content.ReadAsAsync<IEnumerable<OccasionDto>>().Result;

            ViewModel.SpecialOccasions = SpecialOccasions;

            return View(ViewModel);
        }

        //POST: Flower/Occasion/{FlowerId}
        [HttpPost]
        public ActionResult Flower(int id, int OccasionId)
        {
            //flower to occasion
            string url = "flowerdata/flowertooccasion/" + id + "/" + OccasionId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }


        public ActionResult Error()
        {

            return View();
        }

        // GET: Animal/New
        public ActionResult New()
        {
            //information about all occasion in the system.
            //GET api/bouquetdata/listbouquet

            string url = "bouquetdata/listbouquet";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<BouquetDto> BouquetOptions = response.Content.ReadAsAsync<IEnumerable<BouquetDto>>().Result;

            return View(BouquetOptions);
        }

        // POST: Animal/Create
        [HttpPost]
        public ActionResult Create(Flower flower)
        {
            string url = "animaldata/addanimal";


            string jsonpayload = jss.Serialize(flower);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Animal/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateFlower ViewModel = new UpdateFlower();

            //flower information
            string url = "flowerdata/findflower/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FlowerDto SelectedFlower = response.Content.ReadAsAsync<FlowerDto>().Result;
            ViewModel.SelectedFlower = SelectedFlower;

            url = "bouquetdata/listbouquet/";
            response = client.GetAsync(url).Result;
            IEnumerable<BouquetDto> BouquetOptions = response.Content.ReadAsAsync<IEnumerable<BouquetDto>>().Result;

            ViewModel.BouquetOptions = BouquetOptions;

            return View(ViewModel);
        }

        // POST: Flower/Update/1
        [HttpPost]
        public ActionResult Update(int id, Flower flower)
        {
            string url = "animaldata/updateanimal/" + id;
            string jsonpayload = jss.Serialize(flower);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Flower/Delete/2
        public ActionResult DeleteConfirm(int id)
        {
            string url = "flowerdata/findflower/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            FlowerDto SelectedFlower = response.Content.ReadAsAsync<FlowerDto>().Result;

            return View(SelectedFlower);
        }

        // POST: Flower/Delete/2
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "flowerdata/deleteflower/" + id;

            HttpContent content = new StringContent("");

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}