using System;
using System.IO;
using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace ReviewApp.Storage.Client
{
    public class SpacesClient
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly string _spacesBasePath;
        private readonly string _spacesBucket;
        private readonly string _spacesUrl;

        public SpacesClient(string awsAccessKey, string awsSecret, string spacesBasePath, string spacesBucket, string spacesUrl)
        {
            var awsConfig = new AmazonS3Config()
            {
                ServiceURL = spacesUrl
            };
            
            _amazonS3 = new AmazonS3Client(awsAccessKey, awsSecret, awsConfig);

            _spacesUrl = spacesUrl;
            _spacesBucket = spacesBucket;
            _spacesBasePath = spacesBasePath;
        }

        public string PutObject(MemoryStream memoryStream, string type, string fileName)
        {
            var dotIndex = fileName.LastIndexOf(".", StringComparison.Ordinal);
            var file = _spacesBasePath + "/" + Guid.NewGuid() + fileName.Substring(dotIndex);
            
            var request = new PutObjectRequest()
            {
                BucketName = _spacesBucket,
                CannedACL = S3CannedACL.PublicRead,
                InputStream = memoryStream,
                ContentType = type,
                Key = file
            };

            var response = _amazonS3.PutObjectAsync(request)
                .Result;

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return "https://cdn.cavitos.net/" + file;
            }

            return "";
        }
    }
}