using Agava.YandexGames;

namespace HumanHole.Scripts.Infrastructure.Services.DeviceDetection
{
    public class DeviceDetectionService : IDeviceDetectionService
    {
        public DeviceType GetDeviceType() => 
            Device.Type;
    }
}
