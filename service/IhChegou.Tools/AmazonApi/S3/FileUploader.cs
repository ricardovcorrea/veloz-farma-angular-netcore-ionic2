using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using IhChegou.Tools.Contract;
using System;
using System.IO;

namespace IhChegou.Tools.AmazonApi.S3
{
    public class FileUploader : S3Base, IFileUploader
    {

        public string UploadFile(Stream file, string filename)
        {
            using (var client = new AmazonS3Client(this.AccessKey, this.SecretKey, RegionEndpoint.USEast1))
            {

                try
                {
                    PutObjectRequest request = new PutObjectRequest();
                    request.InputStream = file;
                    request.BucketName = this.BucketName;
                    request.CannedACL = S3CannedACL.PublicRead;
                    request.Key = Folder + filename;
                    var response = client.PutObject(request);

                    return AmazonUrl + Folder + filename;

                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                        ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        throw new Exception("Check the provided AWS Credentials.");
                    }
                    else
                    {
                        throw new Exception("Error occurred: " + amazonS3Exception.Message);
                    }
                }
            }
        }
    }
}
