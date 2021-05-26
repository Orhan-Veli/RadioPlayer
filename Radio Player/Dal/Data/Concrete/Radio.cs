using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Radio_Player.Dal.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Dal.Data.Concrete
{
    public class Radio : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Name { get; set; }
        [BsonRequired]
        public string RadioConnection { get; set; }
        [BsonRequired]
        public string Category { get; set; }

    }
}
