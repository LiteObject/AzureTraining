namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.Azure.Cosmos;

    /// <summary>
    /// The cosmos service.
    /// Source: https://docs.microsoft.com/en-us/azure/cosmos-db/sql-api-get-started
    /// </summary>
    internal class CosmosService
    {
        // ADD THIS PART TO YOUR CODE

        /// <summary>
        /// The endpoint uri.
        /// The Azure Cosmos DB endpoint for running this sample.
        /// </summary>
        private static readonly string EndpointUri = "<your endpoint here>";

        /// <summary>
        /// The primary key.
        /// The primary key for the Azure Cosmos account.
        /// </summary>
        private static readonly string PrimaryKey = "<your primary key>";

        /// <summary>
        /// The Cosmos client instance
        /// </summary>
        private readonly CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "FamilyDatabase";

        private string containerId = "FamilyContainer";

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosService"/> class.
        /// </summary>
        public CosmosService()
        {

        }
    }
}

