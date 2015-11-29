using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostiq.Web.Models
{
    public class Moment
    {
        public Moment(string name, dynamic value, DateTime timeStamp)
        {
            Name = name;
            Value = value;
            TimeStamp = timeStamp;
        }

        public string Name { get; set; }
        public dynamic Value { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
