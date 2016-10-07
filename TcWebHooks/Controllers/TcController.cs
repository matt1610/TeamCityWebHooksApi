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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TcWebHooks.Controllers
{
    public class TcController : ApiController
    {
        public MqttClient client { get; set; }
        public ExtensionMethods ExtensionMethods { get; set; }
        public TcController()
        {
            ExtensionMethods = new ExtensionMethods();
            client = new MqttClient(IPAddress.Parse("127.0.0.1"));
            //client = new MqttClient(IPAddress.Parse("10.1.21.178"));

            // register to message received 
            client.MqttMsgPublishReceived += TestMethod;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            Console.WriteLine(clientId);

            // subscribe to the topic "/home/temperature" with QoS 2 
            client.Subscribe(new string[] { "/home/temperature" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        [Route("api/Tc/TestMqtt")]
        public bool TestMqtt([FromBody] MqttMessage mqttMessage)
        {
            client.Publish("/home/Mobile :: Mobile Services :: Forms Service", Encoding.ASCII.GetBytes(mqttMessage.Message));
            return true;
        }

        public void TestMethod(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("Yo!");
        }

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
            ExtensionMethods.WriteToDebugLogFile(Request);
            return true;
        }

        [Route("api/Tc/TcUpdate")]
        [HttpPost]
        public bool TcUpdate([FromBody]TcHttpModel req)
        {
            List<Device> devicesList = db.DeviceModels.ToList();
            ExtensionMethods.WriteStringToFile(req.build.buildFullName);
            client.Publish("/home/temperature", Encoding.ASCII.GetBytes(req.build.buildFullName));

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
