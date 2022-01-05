using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Calculadora_Worker.Models
{
    public class SumModel
    {
        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("number1")]
        [BsonRequired()]
        public int number1 { get; set; }

        [BsonElement("number2")]
        [BsonRequired()]
        public int number2 { get; set; }

        [BsonElement("status")]
        [BsonRequired()]
        public string status { get; set; }

        [BsonElement("result")]
        [BsonRequired()]
        public int? result { get; set; }

        [BsonElement("__v")]
        [BsonRequired()]
        public int? __v { get; set; }
    }
}