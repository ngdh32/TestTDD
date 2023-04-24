using System;
using Amazon.DynamoDBv2.DataModel;

namespace TestTddInfra.Entities.DynamoDb
{
    [DynamoDBTable("Author")]
    public class DynamoDbAuthor : DynamoDbBaseEntity
    {
        [DynamoDBProperty("FirstName")]
        public string FirstName { get; set; }
        [DynamoDBProperty("LastName")]
        public string LastName { get; set; }
    }
}

