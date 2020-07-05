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

        public SpacesClient(string awsAccessKey, string awsSecret, string spacesBasePath, string spacesBucket, string spacesUrl)
        {
            var awsConfig = new AmazonS3Config()
            {
                ServiceURL = spacesUrl
            };
            
            _amazonS3 = new AmazonS3Client(awsAccessKey, awsSecret, awsConfig);

            _spacesBucket = spacesBucket;
            _spacesBasePath = spacesBasePath;
        }

        public void PutObject(byte [] bytes)
        {
            var request = new PutObjectRequest()
            {
                BucketName = _spacesBucket,
                CannedACL = S3CannedACL.PublicRead,
            };

            var response = _amazonS3.PutObjectAsync(request)
                .Result;
            
            
        }
    }
}