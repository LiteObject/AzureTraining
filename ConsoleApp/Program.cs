namespace ConsoleApp
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Azure.Storage.Blobs;

    /// <summary>
    /// The program.
    /// Source: https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=mystorage0602;AccountKey=Abp4UveCG9wjIoWBTWeFqI9aU5gBjYAIdpsQbzbGq41ucyJXFBWxCUzoK1hTX14ZI5BrDy013GxIJBgXb5mwxw==;EndpointSuffix=core.windows.net";
            string key = "Abp4UveCG9wjIoWBTWeFqI9aU5gBjYAIdpsQbzbGq41ucyJXFBWxCUzoK1hTX14ZI5BrDy013GxIJBgXb5mwxw==";

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            // Create a unique name for the container
            string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);




            stopWatch.Stop();
            Console.WriteLine($"Time Elapsed: {stopWatch.ElapsedMilliseconds}ms.");
        }
    }
}
