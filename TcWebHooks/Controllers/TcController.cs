using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using TcWebHooks.Models;
using TcWebHooks.Shared;

namespace TcWebHooks.Controllers
{
    public class TcController : ApiController
    {
        private TcWebHooksContext db = new TcWebHooksContext();
        public Utilities Utilities = new Utilities();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public bool Post([FromBody]TcHttpModel req)
        {
            req.build.buildName.WriteToDebugLogFile(Request);
            return true;
        }

        [Route("api/Tc/TcUpdate")]
        [HttpPost]
        public bool TcUpdate([FromBody]TcHttpModel req)
        {
            List<Device> devicesList = db.DeviceModels.ToList();

            foreach (Device device in devicesList)
            {
                if (device.SubscribedBranches.Contains(req.build.branchName))
                {
                    bool result = req.build.buildResult == "success";
                    DeviceUpdate deviceUpdateMessage = new DeviceUpdate(req.build.buildName, req.build.notifyType, result, req.build.branchName);
                    Utilities.MakeWebRequestToDevice(device.IpAddress, deviceUpdateMessage);
                }
            }

            return true;
        }

    }
}
