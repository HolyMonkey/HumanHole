using System.Linq;
using System.Threading.Tasks;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HumanHole.Scripts.Infrastructure.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private const string _localizationTableName = "UIText";
        private const string EnglishLocaleIdentifier = "en";

        private StringTable _stringTable;

        public async Task Initialize()
        {
            await LocalizationSettings.InitializationOperation.Task;

            AsyncOperationHandle<StringTable> loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(_localizationTableName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                _stringTable = loadingOperation.Result;
                Debug.Log("String Table is loaded");
            }
            else
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException);
            }
            
            #if !UNITY_EDITOR
            SetLocale(YandexGamesSdk.Environment.browser.lang);
            #endif
        }

        private static void SetLocale(string localeIdentifier)
        {
            Locale locale = LocalizationSettings.AvailableLocales.Locales.FirstOrDefault(x => x.Identifier == localeIdentifier);
            if (locale == null)
                locale = LocalizationSettings.AvailableLocales.Locales.FirstOrDefault(x =>
                    x.Identifier == EnglishLocaleIdentifier);
            
            LocalizationSettings.SelectedLocale = locale;
            Debug.Log($"Language changed, current language identifier {LocalizationSettings.SelectedLocale.LocaleName}");
        }

        public string GetStringFor(string entryName)
        {
            if (_stringTable != null)
            {
                return _stringTable
                    .GetEntry(entryName)
                    .GetLocalizedString();
            }

            return "<Empty>";
        }

        public void AddLocalVariable<T>(LocalizedString localizedString, string key, T value) where T : struct =>
            localizedString.Add(key, new Variable<T>()
            {
                Value = value
            });

        public void RemoveLocalVariable(LocalizedString localizedString, string key)=>
            localizedString.Remove(key);
    }
}