using Agava.YandexGames.Utility;
using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class AudioListenerHandler : MonoBehaviour
    {
        private void Update() => 
            AudioListener.pause = WebApplication.InBackground;
    }
}
