using System;
using Amazon.DynamoDBv2.DataModel;

namespace TestTddInfra.Entities.DynamoDb
{
	public abstract class DynamoDbBaseEntity
	{
		[DynamoDBProperty("Id")]
		public int Id { get; set; }
	}
}

