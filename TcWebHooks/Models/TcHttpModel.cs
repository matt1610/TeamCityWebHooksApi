﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TcWebHooks.Models
{
    public class TcHttpModel
    {
        public Build build { get; set; }
    }

    public class Build
    {
        public string buildStatus { get; set; }
        public string buildResult { get; set; }
        public string buildResultPrevious { get; set; }
        public string buildResultDelta { get; set; }
        public string notifyType { get; set; }
        public string buildFullName { get; set; }
        public string buildName { get; set; }
        public string buildId { get; set; }
        public string buildTypeId { get; set; }
        public string buildInternalTypeId { get; set; }
        public string buildExternalTypeId { get; set; }
        public string buildStatusUrl { get; set; }
        public string buildStatusHtml { get; set; }
        public string rootUrl { get; set; }
        public string projectName { get; set; }
        public string projectId { get; set; }
        public string projectInternalId { get; set; }
        public string projectExternalId { get; set; }
        public string buildNumber { get; set; }
        public string agentName { get; set; }
        public string agentOs { get; set; }
        public string agentHostname { get; set; }
        public string triggeredBy { get; set; }
        public string message { get; set; }
        public string text { get; set; }
        public string branchName { get; set; }
        public string branchDisplayName { get; set; }
        public string buildStateDescription { get; set; }
        public bool branchIsDefault { get; set; }
        public Branch branch { get; set; }
        public string[] buildRunners { get; set; }
        public object[] extraParameters { get; set; }
        public Teamcityproperty[] teamcityProperties { get; set; }
    }

    public class Branch
    {
        public string _class { get; set; }
        public string myBranchName { get; set; }
        public string myDisplayName { get; set; }
    }

    public class Teamcityproperty
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}