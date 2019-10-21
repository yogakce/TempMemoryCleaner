using IntelligentMemoryScavenger.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentMemoryScavenger
{
    public class MemoryScavenger
    {
        private ILogger _logger;
        public MemoryScavenger(ILogger logger)
        {
            _logger = logger;
        }
        public void CleanMemory()
        {
            _logger.Log("Service is recall at " + DateTime.Now);
            try
            {
                var tempFolderPath = Path.GetTempPath();
                var tempFileFolders = Directory.EnumerateDirectories(tempFolderPath);
                var tempFiles = Directory.EnumerateFiles(tempFolderPath);

                foreach (var tempFileFolder in tempFileFolders)
                {
                    try
                    {
                        _logger.Log($"Deleting temp file folder: {tempFileFolder}");
                        Directory.Delete(tempFileFolder, true);
                        _logger.Log($"Service is able to scavenge temp folder at: {tempFileFolder}");
                    }
                    catch (Exception e1)
                    {
                        _logger.Log($"Service is unable to scavenge temp folder at: {tempFileFolder} and exception thrown is: {e1.Message}");
                    }
                }

                foreach (var tempFile in tempFiles)
                {
                    try
                    {
                        _logger.Log($"Deleting temp file: {tempFile}");
                        File.Delete(tempFile);
                        _logger.Log($"Service is able to scavenge temp file: {tempFile}");
                    }
                    catch (Exception e2)
                    {
                        _logger.Log($"Service is unable to scavenge temp folder at: {tempFile} and exception thrown is: {e2.Message}");
                    }
                }
                _logger.Log($"Service is able to scavenge temp folder at " + DateTime.Now);
            }
            catch (Exception exception)
            {
                _logger.Log($"Service is unable to delete files because of exception : { exception.Message }");
            }
        }
    }
}
