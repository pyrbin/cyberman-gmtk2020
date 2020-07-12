using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Transform HealthGrid;

    [Range(0, 3)]
    public int Hp = 3;

    void Update()
    {
        if (Player.Instance != null && Hp != Player.Instance.Health)
        {
            Hp = Player.Instance.Health;
            UpdateHP();
        }
    }

    [Button("UpdateHP")]
    public void UpdateHP()
    {
        for (int i = 0; i < HealthGrid.childCount; i++)
        {
            HealthGrid.GetChild(i).gameObject.SetActive(i < Hp);
        }
    }
}
