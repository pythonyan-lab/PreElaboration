using MongoDB.Driver;
using System;
using MongoDB.Bson;
namespace MongoPreElaboration
{
    public class MongoDbOperation
    {
        public MongoClient dbClient { get; set; }
        public IMongoDatabase database { get; set; }

        public MongoDbOperation(string cnsString, string databaseName)
        {
            dbClient = new MongoClient(cnsString);
            database = dbClient.GetDatabase(databaseName);
        }

        public void DbList()
        {
            var dbList = dbClient.ListDatabases().ToList();
            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }

        }
        public void ReviewList(string reviewId)
        {
            var database = dbClient.GetDatabase("CBAS");
            var filter = Builders<BsonDocument>.Filter.Eq("review_id", reviewId);
            var reviewCollection = database.GetCollection<BsonDocument>("pre_elaboration");
            var reviewsDocument = reviewCollection.Find(filter).ToList();
            foreach (var review in reviewsDocument)
            {
                Console.WriteLine(review);
            }

        }
        public List<BsonDocument> GetSingleDuplicateReview(string reviewId)
        {

            var filter = new BsonDocument
            {
                { "review_id", reviewId },
                { "processed", new BsonDocument("$eq", new BsonArray()) }
            };
            var projection = Builders<BsonDocument>.Projection.Include("_id");
            var reviewCollection = database.GetCollection<BsonDocument>("pre_elaboration");
            var reviewsDocument = reviewCollection.Find(filter).Project(projection).ToList();
            // foreach (var review in reviewsDocument)
            // {
            //     Console.WriteLine(review);
            // }
            return reviewsDocument;

        }
        public List<BsonDocument> GetDuplicateReviewIds()
        {
            var duplicateReviewCollection = database.GetCollection<BsonDocument>("duplicate_reviews");
            var filter = new BsonDocument();
            var projection = Builders<BsonDocument>.Projection.Include("name").Exclude("_id");
            var reviewsDocument = duplicateReviewCollection.Find(filter).Project(projection).ToList();
            return reviewsDocument;

        }

        public void DeleteDuplicateReviewId(string object_id)
        {
             var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(object_id));
            //var filter = new BsonDocument("_id", new ObjectId(object_id));
            var reviewCollection = database.GetCollection<BsonDocument>("pre_elaboration");
            // var reviewsDocument = reviewCollection.Find(filter).First();
            // foreach (var review in reviewsDocument)
            // {
            //     Console.WriteLine(review);
            // }
            reviewCollection.DeleteOne(deleteFilter);
            Console.WriteLine("deleted document --- {0}", object_id);

        }

    }
}