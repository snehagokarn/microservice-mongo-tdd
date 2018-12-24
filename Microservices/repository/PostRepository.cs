using Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.repository
{
    [Authorize]
    public class PostRepository : BaseRepository, IPostRepository
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<Models.Post> collection;
        public PostRepository(IConfiguration configuration) : base(configuration)
        {
            var connectionString = GetConnectionString();
            mongoClient = new MongoClient(connectionString);
            db = mongoClient.GetDatabase("post");
            collection = db.GetCollection<Models.Post>("posts");
        }

        public Post Create(Models.Post model)
        {
            model.CreatedDate = DateTime.UtcNow;
            collection.InsertOne(model);
            return ReadById(model.Id);
        }

        public bool Delete(string id)
        {
            var filter = new MongoDB.Bson.BsonDocument("_id", id);
            var fetched = collection.DeleteOne(filter).DeletedCount == 1;
            return fetched;
        }

        public Post ReadById(string id)
        {
            var filter = new MongoDB.Bson.BsonDocument("_id", id);
            var fetched = collection.Find(filter).FirstOrDefault();
            return fetched;
        }

        public Post Update(Post model)
        {
            var filter = new MongoDB.Bson.BsonDocument("_id", model.Id);
            collection.FindOneAndUpdate(filter, Translate(model));
            return ReadById(model.Id);
        }

        private static UpdateDefinition<Post> Translate(Post model)
        {
            return Builders<Post>.Update
                .Set(x => x.Title, model.Title)
                .Set(x => x.Content, model.Content)
                .Set(x => x.BloggerId, model.BloggerId)
                .Set(x => x.UpdatedDate, DateTime.UtcNow);
        }
    }
}
