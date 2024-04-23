using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Library.Mongo.Entities
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
