using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{

    public List<LevelGenerator> levels;

    private int currentLevelIndex = 0;

    void Awake()
    {
        levels[currentLevelIndex].gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        print("changing levels");
        GameObject.FindGameObjectWithTag("Player").gameObject.transform.localPosition = new float3(0f, 0f, 0f);
        levels[currentLevelIndex].gameObject.SetActive(false);
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Count)
        {
            m_OnClick.Invoke();
            return; // We Win
        }
        levels[currentLevelIndex].gameObject.SetActive(true);
    }

    [Serializable]
    /// <summary>
    /// Function definition for a button click event.
    /// </summary>
    public class ButtonClickedEvent : UnityEvent { }

    // Event delegates triggered on click.
    [FormerlySerializedAs("onWin")]
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
