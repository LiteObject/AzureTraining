namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Azure;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;

    /// <summary>
    /// The blob service.
    /// </summary>
    internal class BlobService
    {
        private readonly BlobServiceClient blobServiceClient;
        
        private readonly string connectionString;

        private readonly string key;

        public BlobService(string connectionString, string key)
        {
            this.connectionString = connectionString;
            this.key = key;

            // Create a BlobServiceClient object which will be used to create a container client
            blobServiceClient = new BlobServiceClient(connectionString);

            // Create a unique name for the container
            // containerName = "quickstartblobs" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            // BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
        }

        public async Task<BlobContainerClient> CreateContainerAsync(string containerName)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //Create a unique name for the container
            containerName = "AzureTraining_" + Guid.NewGuid();

            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            return containerClient;
        }

        public async Task DeleteContainerAsync(string containerName)
        {
            Console.WriteLine("Deleting blob container...");
            BlobContainerClient containerClient = GetBlobContainerClientAsync(containerName);
            await containerClient.DeleteAsync();
        }

        public async Task UploadBlobToContainerAsync(string fileName, string localPath, string containerName)
        {
            // Create a local file in the ./data/ directory for uploading and downloading

            /*string localPath = "./data/";
            string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt"; 
             */

            string localFilePath = Path.Combine(localPath, fileName);

            // Write text to the file
            // await File.WriteAllTextAsync(localFilePath, "Hello, World!");

            BlobContainerClient blobContainerClient = this.GetBlobContainerClientAsync(containerName);

            // Get a reference to a blob
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            // Open the file and upload its data
            using (FileStream stream = File.OpenRead(localFilePath))
            {
                await blobClient.UploadAsync(stream, true);
                stream.Close();
            }
        }

        public async Task<List<string>> GetContainerBlobListAsync(string containerName)
        {
            Console.WriteLine("Listing blobs...");

            BlobContainerClient blobContainerClient = this.GetBlobContainerClientAsync(containerName);

            var result = new List<string>();

            // List all blobs in the container
            if (blobContainerClient != null)
            {
                AsyncPageable<BlobItem> blobs = blobContainerClient.GetBlobsAsync();

                await foreach (BlobItem blobItem in blobs)
                {
                    Console.WriteLine("\t" + blobItem.Name);
                    result.Add(blobItem.Name);
                }
            }

            return result;
        }
        
        public async Task DownloadBlobAsync(string containerName, string fileName, string localDownloadFilePath)
        {
            if (string.IsNullOrWhiteSpace(localDownloadFilePath) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException($"{nameof(containerName)}, {nameof(fileName)}, or {nameof(localDownloadFilePath)} cannot be null or empty.");
            }

            // Download the blob to a local file
            // Append the string "DOWNLOAD" before the .txt extension so you can compare the files in the data directory
            // string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOAD.txt");

            Console.WriteLine("\nDownloading blob to\n\t{0}\n", localDownloadFilePath);

            BlobContainerClient blobContainerClient = this.GetBlobContainerClientAsync(containerName);
            // Get a reference to a blob
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            // Download the blob's contents and save it to a file
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            await using FileStream downloadFileStream = File.OpenWrite(localDownloadFilePath);
            await download.Content.CopyToAsync(downloadFileStream);
            downloadFileStream.Close();
        }

        private BlobContainerClient GetBlobContainerClientAsync(string containerName)
        {
            // Create the container and return a container client object
            // BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

            // Old CloudBlockBlob
            // CloudBlockBlob blob = container.GetBlockBlobReference("BlobName");

            // New v12 BlobClient
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            // containerClient.CreateIfNotExistsAsync();

            return containerClient;
        }
    }
}
