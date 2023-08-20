using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProjectFlowerShop.Models;
using System.Diagnostics;

namespace PassionProjectFlowerShop.Controllers
{
    public class BouquetDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(BouquetDto))]
        public IHttpActionResult ListSpecies()
        {
            List<Bouquet> Bouquet = db.Bouquets.ToList();
            List<BouquetDto> BouquetDtos = new List<BouquetDto>();

            Bouquet.ForEach(s => BouquetDtos.Add(new BouquetDto()
            {
                BouquetId = s.BouquetId,
                BouquetName = s.BouquetName,
                BouquetPrice = s.BouquetPrice,
                BouquetDescription = s.BouquetDescription
            }));

            return Ok(BouquetDtos);
        }

        [ResponseType(typeof(BouquetDto))]
        [HttpGet]
        public IHttpActionResult FindBouquet(int id)
        {
            Bouquet Bouquet = db.Bouquets.Find(id);
            BouquetDto BouquetDto = new BouquetDto()
            {
                BouquetId = s.BouquetId,
                BouquetName = s.BouquetName,
                BouquetPrice = s.BouquetPrice,
                BouquetDescription = s.BouquetDescription
            };
            if (Bouquet == null)
            {
                return NotFound();
            }

            return Ok(BouquetDto);
        }
                
        [ResponseType(typeof(Bouquet))]
        [HttpPost]
        public IHttpActionResult AddBouquet(Bouquet Bouquet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bouquets.Add(Bouquet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Bouquet.BouquetId }, Bouquet);
        }

        /// <summary>
        /// Deletes an Species from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Species</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/SpeciesData/DeleteSpecies/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Bouquet))]
        [HttpPost]
        public IHttpActionResult DeleteBouquet(int id)
        {
            Bouquet Bouquet = db.Bouquets.Find(id);
            if (Bouquet == null)
            {
                return NotFound();
            }

            db.Bouquets.Remove(Bouquet);
            db.SaveChanges();

            return Ok();
        }

    }
}