using Agava.YandexGames;

namespace HumanHole.Scripts.Infrastructure.Services.DeviceDetection
{
    public interface IDeviceDetectionService: IService
    {
        DeviceType GetDeviceType();
    }
}
