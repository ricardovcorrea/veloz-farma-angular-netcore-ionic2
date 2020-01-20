using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Tools.AmazonApi.S3
{
    public class S3Base
    {
        protected string AccessKey = "";
        protected string SecretKey = "";
        protected string BucketName = "";
        protected string Folder = "farma/";
        protected string AmazonUrl => $"http://s3.amazonaws.com/{BucketName}/";

    }
}
