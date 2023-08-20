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
    public class BouquetController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static BouquetController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }

        // GET: Bouquet/List
        public ActionResult List()
        {
            //show the list of bouquetst
            //curl https://localhost:44324/api/Bouquetdata/listBouquet


            string url = "bouquetdata/listbouquet";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<BouquetDto> Bouquet = response.Content.ReadAsAsync<IEnumerable<BouquetDto>>().Result;
            //Debug.WriteLine("Number of Bouquet : ");
            //Debug.WriteLine(Bouquet.Count());


            return View(Bouquet);
        }

        // GET: Bouquet/Details/5
        public ActionResult Details(int id)
        {
            //To look up a bouquet in the bouquet database
            //curl https://localhost:44324/api/Speciesdata/findspecies/{id}

            DetailsBouquet ViewModel = new DetailsBouquet();

            string url = "bouquetdata/findbouquet/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            BouquetDto SelectedBouquet = response.Content.ReadAsAsync<BouquetDto>().Result;
            Debug.WriteLine("Bouquet : ");
            Debug.WriteLine(SelectedBouquet.BouquetName);

            ViewModel.SelectedBouquet = SelectedBouquet;

            // flowers in the bouquet
            url = "flowerdata/listflowersinbouquet/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<FlowerDto> RelatedFlowers = response.Content.ReadAsAsync<IEnumerable<FlowerDto>>().Result;

            ViewModel.RelatedFlowers = RelatedFlowers;


            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Bouquet/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Bouquet/Create
        [HttpPost]
        public ActionResult Create(Bouquet Bouquet)
        {

            string url = "bouquetdata/addbouquet";

            string jsonpayload = jss.Serialize(Bouquet);
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

        // GET: Bouquet/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "bouquetdata/findbouquet/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            BouquetDto selectedBouquet = response.Content.ReadAsAsync<BouquetDto>().Result;

            return View(selectedBouquet);
        }

        // POST: Bouquet/Update/5
        [HttpPost]
        public ActionResult Update(int id, Bouquet Bouquet)
        {

            string url = "bouquetdata/updatebouquet/" + id;
            string jsonpayload = jss.Serialize(Bouquet);

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

        // GET: Bouquet/Delete/2
        public ActionResult DeleteConfirm(int id)
        {
            string url = "speciesdata/findspecies/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            BouquetDto selectedBouquet = response.Content.ReadAsAsync<BouquetDto>().Result;

            return View(selectedBouquet);
        }

        // POST: Bouquet/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "bouquetdata/deletebouquet/" + id;

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
