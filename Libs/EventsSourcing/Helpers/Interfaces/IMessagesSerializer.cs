namespace EventsSourcing.Helpers.Interfaces
{
   public interface IMessagesSerializer
    {
        object Deserialize(DataChunk dataChunk);
        DataChunk Serialize(object item);
    }
}
