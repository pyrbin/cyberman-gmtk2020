using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collection of all manager classes
public class Game : MonoBehaviour
{
    public static Game Find()
    {
        if (Instance == null)
        {
            Instance = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        }
        return Instance;
    }

    private static Game Instance;

    public LevelManager LevelManager;
}