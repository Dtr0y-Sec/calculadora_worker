using Calculadora_Worker.Domain.RabbitMQ;
using System.Threading.Tasks;
using System;
using Calculadora_Worker.MongoDB;
using MongoDB.Driver;
using Calculadora_Worker.Models;
using System.Linq.Expressions;
using MongoDB.Bson;
using System.Threading;

namespace Calculadora_Worker.Services
{
    public class SumOperationService
    {
        public async Task<RabbitMQResponseTask> SumOperation(string _id)
        {
            try
            {
                IMongoClient mongoClient = new MongoDBConnection().MongoClient;
                IMongoDatabase database = mongoClient.GetDatabase("calculator");
                IMongoCollection<SumModel> colSum = database.GetCollection<SumModel>("Sum");

                FilterDefinition <SumModel> query = Builders<SumModel>.Filter.Eq(i => i.Id, ObjectId.Parse(_id));
                UpdateDefinition <SumModel> updateInProcess = Builders<SumModel>.Update.Set(i => i.status, "Processing");

                await Task.Delay(5000);

                SumModel findDoc = await colSum.FindOneAndUpdateAsync(query, updateInProcess);

                await Task.Delay(5000);

                int result = findDoc.number1 + findDoc.number2;

                UpdateDefinition<SumModel> updateFinish = Builders<SumModel>.Update.Set(i => i.status, "Success")
                                                                                   .Set(i => i.result, result);

                await colSum.UpdateOneAsync(query, updateFinish);

                return new RabbitMQResponseTask(true, "");
            }
            catch (Exception ex)
            {
                return new RabbitMQResponseTask(false, ex.ToString());
            }
        }
    }
}
