using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ManaRefiller : MonoBehaviour
{
    [Range(0, 10)]
    public int Mana = 10;

    void Awake()
    {
        UpdateMana();
    }

    void Update()
    {
        if (Player.Instance != null && Mana != Player.Instance.Mana)
        {
            Mana = Player.Instance.Mana;
            UpdateMana();
        }
    }

    [Button("UpdateMana")]
    public void UpdateMana()
    {
        int i = 0;
        foreach (var mana in GetComponentsInChildren<ManaUnitScript>())
        {
            mana.SetManaStatus(i < Mana);
            i++;
        }
    }

}
