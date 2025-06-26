using System;
using System.IO;
using System.Threading.Tasks;

namespace MovieBooking.Applications
{
    public static class IoProcessorExtensions
    {
        public static async Task<bool> ReadLinesUntil(
            this IoProcessor ioProcessor,
            string? query,
            Func<string, Task<bool>> predicate,
            Func<string, Task> onFailedAsync,
            Func<string, Exception, Task> onErrorAsync)
        {
            try
            {
                bool isValid = false;
                while (!isValid)
                {
                    string value = await ioProcessor.ReadLineAsync(query);
                    try
                    {
                        isValid = await predicate(value);
                        if (!isValid)
                        {
                            await onFailedAsync(value);
                        }
                    }
                    catch (Exception e)
                    {
                        await onErrorAsync(value, e);
                    }
                }
                return true;
            }
            catch (IOException)
            {
                await ioProcessor.WriteLineAsync();
                return false;
            }
        }
    }
}
