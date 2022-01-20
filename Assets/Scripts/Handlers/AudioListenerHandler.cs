using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexGames.Utility;

public class AudioListenerHandler : MonoBehaviour
{
    private void Update()
    {
        AudioListener.pause = WebApplication.InBackground;
    }
}
