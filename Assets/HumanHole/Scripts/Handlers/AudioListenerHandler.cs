using Agava.YandexGames.Utility;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class AudioListenerHandler
    {
        public void OnUpdated() => 
            AudioListener.pause = WebApplication.InBackground;
    }
}
