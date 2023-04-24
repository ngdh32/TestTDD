using System;
using Amazon.DynamoDBv2.DataModel;
using TestTddInfra.Entities.EF;

namespace TestTddInfra.Entities.DynamoDb
{
    [DynamoDBTable("Book")]
    public class DynamoDbBook : DynamoDbBaseEntity
    {
        [DynamoDBProperty("Title")]
        public string Title { get; set; }
        [DynamoDBProperty("CreateDate")]
        public DateTime CreateDate { get; set; }
        [DynamoDBProperty("AuthorId")]
        public int AuthorId { get; set; }
    }
}

