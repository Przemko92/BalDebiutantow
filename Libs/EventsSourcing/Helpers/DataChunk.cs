using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSourcing.Helpers
{
    public class DataChunk
    {
        public string ContentType { get; }
        public byte[] Body { get; }

        public DataChunk(byte[] body, string contentType)
        {
            this.Body = body;
            this.ContentType = contentType;
        }
    }
}
