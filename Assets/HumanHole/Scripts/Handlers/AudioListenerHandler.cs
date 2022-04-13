using Agava.YandexGames.Utility;
using UnityEngine;

public class AudioListenerHandler : MonoBehaviour
{
    private void Update() => 
        AudioListener.pause = WebApplication.InBackground;
}
