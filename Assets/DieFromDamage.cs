using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DieFromDamage : MonoBehaviour
{

    public int distanceToDeath;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y < -distanceToDeath)
        {
            m_OnClick.Invoke();
        }
    }

    [Serializable]
    /// <summary>
    /// Function definition for a button click event.
    /// </summary>
    public class ButtonClickedEvent : UnityEvent { }

    // Event delegates triggered on click.
    [FormerlySerializedAs("onDeath")]
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();



    public ButtonClickedEvent onClick
    {
        get { return m_OnClick; }
        set { m_OnClick = value; }
    }

}
