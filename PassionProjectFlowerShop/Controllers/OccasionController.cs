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
    public class OccasionController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static OccasionController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }

        // GET: Occasion/List
        public ActionResult List()
        {
            //objective: communicate with our Keeper data api to retrieve a list of Keepers
            //curl https://localhost:44324/api/Occasiondata/listoccasions


            string url = "OccasionData/listoccasions";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<OccasionDto> Occasions = response.Content.ReadAsAsync<IEnumerable<OccasionDto>>().Result;
            //Debug.WriteLine("Number of Occasion : ");
            //Debug.WriteLine(Occasions.Count());


            return View(Occasions);
        }

        // GET: Occasion/Details/5
        public ActionResult Details(int id)
        {
            DetailsOccasion ViewModel = new DetailsOccasion();

            //objective: communicate with our Keeper data api to retrieve one Keeper
            //curl https://localhost:44324/api/occasiondata/findoccasion/{id}

            string url = "Occasiondata/findoccasion/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            OccasionDto SelectedOccasion = response.Content.ReadAsAsync<OccasionDto>().Result;
            Debug.WriteLine("Occasion : ");
            Debug.WriteLine(SelectedOccasion);

            ViewModel.SelectedOccasion = SelectedOccasion;


            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Occasion/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Occasion/Create
        [HttpPost]
        public ActionResult Create(Occasion Occasion)
        {

            string url = "occasiondata/addoccasion";

            string jsonpayload = jss.Serialize(Occasion);
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

        // GET: Occasion/Edit/2
        public ActionResult Edit(int id)
        {
            string url = "occasiondata/findoccasion/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            OccasionDto SelectedOccasion = response.Content.ReadAsAsync<OccasionDto>().Result;
            return View(SelectedOccasion);
        }

        // POST: Occasion/Update/4
        [HttpPost]
        public ActionResult Update(int id, Occasion Occasion)
        {

            string url = "occasiondata/updateoccasion/" + id;

            string jsonpayload = jss.Serialize(Occasion);

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

        // GET: Occasion/Delete/3
        public ActionResult DeleteConfirm(int id)
        {
            string url = "occasiondata/findoccasion/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            OccasionDto SelectedOccasion = response.Content.ReadAsAsync<OccasionDto>().Result;

            return View(SelectedOccasion);
        }

        // POST: Occasion/Delete/3
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "occasiondata/deleteoccasion/" + id;

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
