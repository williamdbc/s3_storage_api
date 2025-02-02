namespace s3_storage_api.Consants;

public static class AppConstants
{
    public const string StorageDirectory = "wwwroot";
    public const long MaxFileSizeInMegabytes = 2048L;
    public const long MaxFileSize = MaxFileSizeInMegabytes * 1024L * 1024L;
}