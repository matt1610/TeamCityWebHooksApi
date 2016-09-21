using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TcWebHooks.Models
{
    public class DeviceUpdate
    {
        public DeviceUpdate(string projectName, string eventName, bool buildSucceeded, string branchName)
        {
            ProjectName = projectName;
            EventName = eventName;
            BuildSucceeded = buildSucceeded;
            BranchName = branchName;
        }
        public string ProjectName { get; set; }
        public string EventName { get; set; }
        public bool BuildSucceeded { get; set; }
        public string BranchName { get; set; }
    }
}