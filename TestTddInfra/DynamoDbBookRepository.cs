using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using TestTddCore.Entities;
using TestTddCore.Interfaces;

namespace TestTddInfra
{
	public class DynamoDbBookRepository : IBookRepository
    {
        private readonly IAmazonDynamoDB _client;
        private const string _bookTableName = "Book";
        private const string _authorTableName = "Author";

        public DynamoDbBookRepository(IAmazonDynamoDB client)
		{
            _client = client;
        }

        public Author GetAuthor(int id)
        {
            var request = new GetItemRequest
            {
                TableName = _authorTableName,
                Key = new Dictionary<string, AttributeValue>()
                {
                    {
                        "Id", new AttributeValue
                        {
                            N = $"{id}"
                        }
                    }
                }
            };

            var response = _client.GetItemAsync(request).Result;

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var result = response.Item;
            return new Author
            {
                Id = Convert.ToInt32(result["Id"].N),
                FirstName = result["FirstName"].S,
                LastName = result["LastName"].S
            };
        }

        public Book GetBook(int id)
        {
            var request = new GetItemRequest
            {
                TableName = _bookTableName,
                Key = new Dictionary<string, AttributeValue>()
                {
                    {
                        "Id", new AttributeValue
                        {
                            N = $"{id}"
                        }
                    }
                }
            };

            var response = _client.GetItemAsync(request).Result;

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var result = response.Item;
            return new Book
            {
                Id = Convert.ToInt32(result["Id"].N),
                Title = result["Title"].S,
                AuthorId = Convert.ToInt32(result["AuthorId"].N),
                CreateDate = Convert.ToDateTime(result["CreateDate"].S)
            };
        }

        public List<Book> GetBooks()
        {
            var request = new ScanRequest
            {
                TableName = _bookTableName
            };

            var response = _client.ScanAsync(request).Result;

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var result = new List<Book>();
            foreach(var item in response.Items)
            {
                result.Add(new Book
                {
                    Id = Convert.ToInt32(item["Id"].N),
                    Title = item["Title"].S,
                    AuthorId = Convert.ToInt32(item["AuthorId"].N),
                    CreateDate = Convert.ToDateTime(item["CreateDate"].S)
                });
            }

            return result;
        }
    }
}

