using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DieFromFalling : MonoBehaviour
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


    /// <summary>
    /// UnityEvent that is triggered when the button is pressed.
    /// Note: Triggered on MouseUp after MouseDown on the same object.
    /// </summary>
    ///<example>
    ///<code>
    /// using UnityEngine;
    /// using UnityEngine.UI;
    /// using System.Collections;
    ///
    /// public class ClickExample : MonoBehaviour
    /// {
    ///     public Button yourButton;
    ///
    ///     void Start()
    ///     {
    ///         Button btn = yourButton.GetComponent<Button>();
    ///         btn.onClick.AddListener(TaskOnClick);
    ///     }
    ///
    ///     void TaskOnClick()
    ///     {
    ///         Debug.Log("You have clicked the button!");
    ///     }
    /// }
    ///</code>
    ///</example>
    public ButtonClickedEvent onClick
    {
        get { return m_OnClick; }
        set { m_OnClick = value; }
    }

}
