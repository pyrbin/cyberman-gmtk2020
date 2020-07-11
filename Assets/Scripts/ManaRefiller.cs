using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ManaRefiller : MonoBehaviour
{
    [ReorderableList]
    [SerializeField]
    public List<ManaUnitScript> ManaUnitList;

    [SerializeField]
    private int currentMana;

    [SerializeField]
    public Sprite emptyPipImage;
    public Sprite filledPipImage;

    private List<ManaUnitScript> EmptyManaUnitList = new List<ManaUnitScript>{};
    private List<ManaUnitScript> FilledManaUnitList = new List<ManaUnitScript>{};

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
        for (int i = FilledManaUnitList.Count-1; i >= 0; i--)
        {
            ManaUnitScript manaPip = FilledManaUnitList[i];
            manaPip.isFilled = false;
            manaPip.gameObject.transform.GetComponent<Image>().sprite = emptyPipImage;
            FilledManaUnitList.RemoveAt(i);
            EmptyManaUnitList.Add(manaPip);
        }
        currentMana -= amount;
    }
    void addMana(int amount){
        foreach (ManaUnitScript manaPip in EmptyManaUnitList)
        {
            manaPip.isFilled = true;
            manaPip.gameObject.transform.GetComponent<Image>().sprite = filledPipImage;
            EmptyManaUnitList.Remove(manaPip);
            FilledManaUnitList.Add(manaPip);
        }
    }
}
