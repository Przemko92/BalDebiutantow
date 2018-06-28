using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSourcing.Models
{
    public class BasicEvent
    {
        public string Message { get; set; }
        public override string ToString()
        {
            return $"{this.GetType()} {Message}";
        }
    }
}
