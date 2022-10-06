using System;

namespace MongoPreElaboration
{
    class Program
    {
        static void Main(string[] args)
        {
            var cnsString = "mongodb://mongoadmin:mongoadmin@172.24.166.1:27017/?authSource=admin&replicaSet=rs0&readPreference=primary&directConnection=true&ssl=false";
            var dabaseName = "CBAS";
            MongoDbOperation operation = new MongoDbOperation(cnsString, dabaseName);

            //get list database
            //operation.DbList();
            //get list duplicate reviews
            var reviewsDocumentIds = operation.GetDuplicateReviewIds();
            foreach (var review in reviewsDocumentIds)
            {
                Console.WriteLine("reviewsDocumentIds ------------------------- ");
                var review_id = review["name"].ToString();
                Console.WriteLine("review_id: " + review_id); 
                if (!String.IsNullOrEmpty(review_id))
                {
                    Console.WriteLine("IsNullOrEmpty ------------------------- ");
                    // get review with duplicate review_id 
                    var toDeleteIds = operation.GetSingleDuplicateReview(review_id);
                     Console.WriteLine(" toDeleteIds ------------------------- {0}",toDeleteIds.Count());
                    foreach (var objectId in toDeleteIds)
                    {
                        Console.WriteLine("toDeleteIds ------------------------- ");
                        var document_id = objectId["_id"].ToString();
                        // Console.WriteLine("review_id: " + review["name"].ToString()); 
                        // Console.WriteLine("object_id: " + objectId["_id"].ToString()); 
                        if (!String.IsNullOrEmpty(document_id))
                        {
                            operation.DeleteDuplicateReviewId(document_id);
                        }

                    }
                }

            }
            Console.WriteLine("Working completed..document count {0}", reviewsDocumentIds.Count());

        }
    }
}
