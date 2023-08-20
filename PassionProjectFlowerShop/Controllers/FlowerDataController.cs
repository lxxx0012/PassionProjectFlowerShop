using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProjectFlowerShop.Models;

namespace PassionProjectFlowerShop.Controllers
{
    public class FlowerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(FlowerDto))]
        public IHttpActionResult ListFlowers()
        {
            List<Flower> Flowers = db.Flowers.ToList();
            List<FlowerDto> FlowerDtos = new List<FlowerDto>();

            Flowers.ForEach(f => FlowerDtos.Add(new FlowerDto()
            {
                FlowerId = f.FlowerId,
                FlowerName = f.FlowerName,
                FlowerColor = f.FlowerColor,
                FlowerPrice = f.FlowerPrice,
                BouquetId = f.BouquetId,
                BouquetName = f.BouquetName,
                OccasionId = f.OccasionId,
                OccasionName = f.OccasionName
            }));

            return Ok(FlowerDtos);
        }


        [HttpGet]
        [ResponseType(typeof(FlowerDto))]
        public IHttpActionResult ListAnimalsForSpecies(int id)
        {
            List<Flower> Flowers = db.Flowers.Where(f => f.BouquetId == id).ToList();
            List<FlowerDto> FlowerDtos = new List<FlowerDto>();

            Flowers.ForEach(f => FlowerDtos.Add(new FlowerDto()
            {
                FlowerId = f.FlowerId,
                FlowerName = f.FlowerName,
                FlowerColor = f.FlowerColor,
                FlowerPrice = f.FlowerPrice,
                BouquetId = f.Bouquet.BouquetId,
                BouquetName = f.Bouquet.BouquetName,
                OccasionId = f.OccasionId,
                OccasionName = f.OccasionName
            }));

            return Ok(FlowerDtos);
        }

        [HttpGet]
        [ResponseType(typeof(FlowerDto))]
        public IHttpActionResult Listflowersinbouqet(int id)
        {
            //all animals that have keepers which match with our ID
            List<Flower> Flowers = db.Flowers.Where(f => f.Occasions.Any(o => o.OccasionId == id)).ToList();
            List<FlowerDto> FlowerDtos = new List<FlowerDto>();

            Flowers.ForEach(a => FlowerDtos.Add(new FlowerDto()
            {
                FlowerId = f.FlowerId,
                FlowerName = f.FlowerName,
                FlowerColor = f.FlowerColor,
                FlowerPrice = f.FlowerPrice,
                BouquetId = f.Bouquet.BouquetId,
                BouquetName = f.Bouquet.BouquetName,
                OccasionId = f.OccasionId,
                OccasionName = f.OccasionName
            }));

            return Ok(FlowerDtos);
        }

        [HttpPost]
        [Route("api/FlowerData/flowerforoccasion/{flowerid}/{occasionid}")]
        public IHttpActionResult FlowerforOccasion(int flowerId, int OccasionId)
        {

            Flower SelectedFlower = db.Flowers.Include(f => f.Occasions).Where(f => f.FlowerId == flowerId).FirstOrDefault();
            Occasion SelectedOccasion = db.Occasions.Find(OccasionId);

            if (SelectedFlower == null || SelectedOccasion == null)
            {
                return NotFound();
            }

            SelectedFlower.Occasions.Add(SelectedOccasion);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Returns all animals in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An animal in the system matching up to the animal ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the animal</param>
        /// <example>
        /// GET: api/AnimalData/FindAnimal/5
        /// </example>
        [ResponseType(typeof(FlowerDto))]
        [HttpGet]
        public IHttpActionResult FindFlower(int id)
        {
            Flower Flower = db.Flowers.Find(id);
            FlowerDto FlowerDto = new FlowerDto()
            {
                FlowerId = f.FlowerId,
                FlowerName = f.FlowerName,
                FlowerColor = f.FlowerColor,
                FlowerPrice = f.FlowerPrice,
                BouquetId = f.Bouquet.BouquetId,
                BouquetName = f.Bouquet.BouquetName,
                OccasionId = f.OccasionId,
                OccasionName = f.OccasionName
            };
            if (Flower == null)
            {
                return NotFound();
            }

            return Ok(FlowerDto);
        }

        /// <summary>
        /// Updates a particular animal in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Animal ID primary key</param>
        /// <param name="animal">JSON FORM DATA of an animal</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/AnimalData/UpdateAnimal/5
        /// FORM DATA: Animal JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateAnimal(int id, Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.AnimalID)
            {

                return BadRequest();
            }

            db.Entry(animal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Flower))]
        [HttpPost]
        public IHttpActionResult AddFlower(Flower flower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flowers.Add(flower);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flower.FlowerId }, flower);
        }

        [ResponseType(typeof(Flower))]
        [HttpPost]
        public IHttpActionResult DeleteFlower(int id)
        {
            Flower flower = db.Flowers.Find(id);
            if (flower == null)
            {
                return NotFound();
            }

            db.Flowers.Remove(flower);
            db.SaveChanges();

            return Ok();
        }
    }
}

        