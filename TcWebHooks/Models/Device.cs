using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TcWebHooks.Models
{
    public class Device
    {
        public string DeviceName { get; set; }
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public virtual PersistableStringCollection SubscribedBranches { get; set; }
        public string ProjectName { get; set; }
    }

    /// <summary>
    /// ALlows persisting of a simple string collection.
    /// </summary>
    [ComplexType]
    public class PersistableStringCollection : PersistableScalarCollection<string>
    {
        protected override string ConvertSingleValueToRuntime(string rawValue)
        {
            return rawValue;
        }

        protected override string ConvertSingleValueToPersistable(string value)
        {
            return value;
        }
    }
}