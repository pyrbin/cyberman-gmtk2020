using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWin : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Game.Find().LevelManager.NextLevel();
    }
}
