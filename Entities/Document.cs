using MongoDB.Bson;

namespace Library.Mongo.Entities
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}
