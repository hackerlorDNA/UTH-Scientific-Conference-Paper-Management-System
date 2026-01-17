namespace Submission.Service.Services;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string directory, string? customFileName = null);
    Task<byte[]> ReadFileAsync(string filePath);
    Task DeleteFileAsync(string filePath);
    Task<bool> FileExistsAsync(string filePath);
    long GetFileSize(string filePath);
}

public class FileStorageService : IFileStorageService
{
    private readonly string _storagePath;
    private readonly ILogger<FileStorageService> _logger;

    public FileStorageService(IConfiguration configuration, ILogger<FileStorageService> logger)
    {
        _storagePath = configuration["FileStorage:Path"] ?? Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        _logger = logger;

        // Create storage directory if it doesn't exist
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
            _logger.LogInformation("Created storage directory: {Path}", _storagePath);
        }
    }

    public async Task<string> SaveFileAsync(IFormFile file, string directory, string? customFileName = null)
    {
        try
        {
            // Create directory path
            var directoryPath = Path.Combine(_storagePath, directory);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Generate unique filename
            var fileName = customFileName ?? $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(directoryPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path
            var relativePath = Path.Combine(directory, fileName).Replace("\\", "/");
            _logger.LogInformation("File saved: {FilePath}", relativePath);

            return relativePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving file");
            throw;
        }
    }

    public async Task<byte[]> ReadFileAsync(string filePath)
    {
        try
        {
            var fullPath = Path.Combine(_storagePath, filePath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            return await File.ReadAllBytesAsync(fullPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading file: {FilePath}", filePath);
            throw;
        }
    }

    public Task DeleteFileAsync(string filePath)
    {
        try
        {
            var fullPath = Path.Combine(_storagePath, filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                _logger.LogInformation("File deleted: {FilePath}", filePath);
            }

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting file: {FilePath}", filePath);
            throw;
        }
    }

    public Task<bool> FileExistsAsync(string filePath)
    {
        var fullPath = Path.Combine(_storagePath, filePath);
        return Task.FromResult(File.Exists(fullPath));
    }

    public long GetFileSize(string filePath)
    {
        var fullPath = Path.Combine(_storagePath, filePath);
        if (!File.Exists(fullPath))
        {
            return 0;
        }

        var fileInfo = new FileInfo(fullPath);
        return fileInfo.Length;
    }
}
