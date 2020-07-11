using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class HandTestController : MonoBehaviour
{
    [ReorderableList]
    public List<Card> Cards;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cards[0].OnUse(Player.Instance);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cards[1].OnUse(Player.Instance);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Cards[2].OnUse(Player.Instance);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Cards[3].OnUse(Player.Instance);
        }
    }
}
