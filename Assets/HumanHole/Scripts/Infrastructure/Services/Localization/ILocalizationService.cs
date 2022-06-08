using System.Threading.Tasks;
using UnityEngine.Localization;

namespace HumanHole.Scripts.Infrastructure.Services.Localization
{
    public interface ILocalizationService: IService
    {
        string GetStringFor(string entryName);
        Task Initialize();
        void AddLocalVariable<T>(LocalizedString localizedString, string key, T value) where T : struct;
        void RemoveLocalVariable(LocalizedString localizedString, string key);
    }
}