using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ManaUnitScript : MonoBehaviour
{
    public bool Filled;

    [SerializeField]
    public Sprite FilledBg;

    [SerializeField]
    public Sprite EmptyBg;

    // Start is called before the first frame update
    void Awake()
    {
        SetManaStatus(Filled);
    }

    [Button("Toggle Filled")]
    public void SetFilled() => SetManaStatus(!Filled);

    // Update is called once per frame
    public void SetManaStatus(bool status)
    {
        Filled = status;
        GetComponent<Image>().sprite = status ? FilledBg : EmptyBg;
    }
}
