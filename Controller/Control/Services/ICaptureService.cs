using Control.Models;

namespace Control.Services
{
    public interface ICaptureService
    {
        Task CaptureData(CaptureRequest request);
    }
}
