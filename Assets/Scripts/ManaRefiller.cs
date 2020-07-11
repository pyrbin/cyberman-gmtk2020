using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ManaRefiller : MonoBehaviour
{
    [ReorderableList]
    [SerializeField]
    public List<ManaUnitScript> ManaUnitList;

    [SerializeField]
    private int currentMana;

    private List<ManaUnitScript> EmptyManaUnitList;
    private List<ManaUnitScript> FilledManaUnitList;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (ManaUnitScript manaUnit in ManaUnitList){
            if (manaUnit.isFilled)
            {
                FilledManaUnitList.Add(manaUnit);
            }
            else
            {
                EmptyManaUnitList.Add(manaUnit);
            }
        }
        currentMana = FilledManaUnitList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void removeMana(int amount){
        if (amount > currentMana)
            return;
        for (int i = FilledManaUnitList.Count-1; i >= 0; i--){
            ManaUnitScript manaPip = FilledManaUnitList[i];
           // FilledManaUnitList
        }
    }
}
