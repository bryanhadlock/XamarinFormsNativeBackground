using System;
using System.Threading.Tasks;

namespace NativeBackground
{
    public interface IRandomUploadService
    {
        Task StartUploadForIdAsync(Guid id);
    }
}
