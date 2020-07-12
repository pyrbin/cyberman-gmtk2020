using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManaUnitScript : MonoBehaviour, IPointerClickHandler
{
    public bool isFilled;
    // Start is called before the first frame update
    void Start()
    {
        isFilled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
