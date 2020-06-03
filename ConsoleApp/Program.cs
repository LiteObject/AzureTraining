namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
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
            
            var containerName = "general";
            var service = new BlobService(connectionString);
            List<string> blobs = await service.GetContainerBlobListAsync(containerName);

            foreach (var blobName in blobs)
            {
                await service.DownloadBlobAsync(containerName, blobName, "c:\\temp\\");
            }
            
            stopWatch.Stop();
            Console.WriteLine($"Time Elapsed: {stopWatch.ElapsedMilliseconds}ms.");
        }
    }
}
