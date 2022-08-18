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
}