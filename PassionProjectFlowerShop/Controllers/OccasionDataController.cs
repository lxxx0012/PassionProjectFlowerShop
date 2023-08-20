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
    public class OccasionDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(OccasionDto))]
        public IHttpActionResult ListOccasions()
        {
            List<Occasion> Occasions = db.Occasions.ToList();
            List<OccasionDto> OccasionDtos = new List<OccasionDto>();

            Occasions.ForEach(o => OccasionDtos.Add(new OccasionDto()
            {
                OccasionId = o.OccasionId,
                OccasionName = o.OccasionName,
                OccasionDate = o.OccasionDate
            }));

            return Ok(OccasionDtos);
        }

        [HttpGet]
        [ResponseType(typeof(OccasionDto))]
        public IHttpActionResult ListOccasionforFlower(int id)
        {
            List<Occasion> Occasions = db.Occasions.Where(o => o.Flowers.Any(o => o.FlowerId == id)).ToList();
            List<OccasionDto> OccasionDtos = new List<OccasionDto>();

            Occasions.ForEach(k => OccasionDtos.Add(new OccasionDto()
            {
                OccasionId = o.OccasionId,
                OccasionName = o.OccasionName,
                OccasionDate = o.OccasionDate
            }));

            return Ok(KeeperDtos);
        }


        [ResponseType(typeof(OccasionDto))]
        [HttpGet]
        public IHttpActionResult FindOccasion(int id)
        {
            Occasion Occasion = db.Occasions.Find(id);
            OccasionDto OccaionDto = new OccasionDto()
            {
                OccasionId = o.OccasionId,
                OccasionName = o.OccasionName,
                OccasionDate = o.OccasionDate
            };
            if (Occasion == null)
            {
                return NotFound();
            }

            return Ok(OccasionDto);
        }

        [ResponseType(typeof(Occasion))]
        [HttpPost]
        public IHttpActionResult AddOccasion(Occasion Occasion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Occasions.Add(Occasion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Occasion.OccasionId }, Occasion);
        }

        [ResponseType(typeof(Occasion))]
        [HttpPost]
        public IHttpActionResult DeleteOccasion(int id)
        {
            Occasion Occasion = db.Occasions.Find(id);
            if (Occasion == null)
            {
                return NotFound();
            }

            db.Occasions.Remove(Occasion);
            db.SaveChanges();

            return Ok();
        }

    }
}
