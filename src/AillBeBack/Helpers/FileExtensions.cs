namespace AillBeBack.Helpers;

public static class FileSystemHelper
{
    public static async Task<string> CopyResourceFileTo(string fromResourcePath, string destinationName)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, destinationName);

        using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(fromResourcePath);

        // Create an output filename
        string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, filePath);

        // Copy the file to the AppDataDirectory
        using FileStream outputStream = File.Create(targetFile);
        inputStream.CopyTo(outputStream);

        return targetFile;
    }
}