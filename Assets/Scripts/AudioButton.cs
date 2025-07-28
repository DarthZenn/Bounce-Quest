using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public void ToggleAudio()
    {
        AudioManager.Instance.ToggleMute();
    }
}
