using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    public int HighScore
    {
        get => PlayerPrefs.GetInt("HighScore", 0);
        set => PlayerPrefs.SetInt("HighScore", value);
    }
    public bool SoundOn
    {
        get => PlayerPrefs.GetInt("SoundOn", 1) == 1;
        set => PlayerPrefs.SetInt("SoundOn", value ? 1 : 0);
    }
}