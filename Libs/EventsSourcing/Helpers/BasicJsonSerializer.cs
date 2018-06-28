using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EventsSourcing.Helpers.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace EventsSourcing.Helpers
{
    public class BasicJsonSerializer : IMessagesSerializer
    {
        private readonly JsonSerializer _serializer;
        public string Type => "application/bson";

        public BasicJsonSerializer()
        {
            _serializer = new JsonSerializer();
            _serializer.TypeNameHandling = TypeNameHandling.All;
        }

        public DataChunk Serialize(object item)
        {
            using (var ms = new MemoryStream())
            {
                JsonWriter writer = new BsonDataWriter(ms);
                _serializer.Serialize(writer, item);
                return new DataChunk(ms.ToArray(), Type);
            }
        }

        public object Deserialize(DataChunk bytes)
        {
            if (!bytes.ContentType.Equals(Type))
            {
                throw new InvalidOperationException("Cannot deserialize type " + bytes.ContentType);
            }

            using (var ms = new MemoryStream(bytes.Body))
            {
                JsonReader reader = new BsonDataReader(ms);
                return _serializer.Deserialize(reader);
            }
        }
    }
}
