using System.IO;

namespace IhChegou.Tools.Contract
{
    public interface IFileUploader
    {
        string UploadFile(Stream file, string filename);
    }
}