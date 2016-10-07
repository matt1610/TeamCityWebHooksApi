using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TcWebHooks.Models;
using TcWebHooks.Shared;

namespace TcWebHooks.Controllers
{
    public class DevicesController : ApiController
    {
        private TcWebHooksContext db = new TcWebHooksContext();

        // GET: api/Devices
        public IQueryable<Device> GetDeviceModels()
        {
            return db.DeviceModels;
        }

        // GET: api/Devices/5
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> GetDevice(int id)
        {
            Device device = await db.DeviceModels.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // PUT: api/Devices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDevice(int id, Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.Id)
            {
                return BadRequest();
            }

            db.Entry(device).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        // POST: api/Tc/RegisterDevice
        [Route("api/Tc/RegisterDevice")]
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> PostDevice(Device device)
        {
            string str = String.Empty;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceModels.Add(device);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = device.Id }, device);
        }

        // DELETE: api/Devices/5
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> DeleteDevice(int id)
        {
            Device device = await db.DeviceModels.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            db.DeviceModels.Remove(device);
            await db.SaveChangesAsync();

            return Ok(device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceExists(int id)
        {
            return db.DeviceModels.Count(e => e.Id == id) > 0;
        }
    }
}