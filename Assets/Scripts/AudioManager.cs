using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager audioManager;

    private void Awake()
    {
        if (audioManager != null && audioManager != this)
            Destroy(gameObject);
        else
        {
            audioManager = this;
            DontDestroyOnLoad(this);
        }
    }
}
