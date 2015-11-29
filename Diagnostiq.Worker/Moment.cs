using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostiq.Worker
{
    public class Moment<T>
    {
        public Moment(string name, T value, DateTime timeStamp)
        {
            Name = name;
            Value = value;
            TimeStamp = timeStamp;
        } 

        public string Name { get; set; }
        public T Value { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
