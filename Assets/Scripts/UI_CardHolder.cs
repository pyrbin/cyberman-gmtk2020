using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UI_CardHolder : MonoBehaviour, IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public Card Card;

    [HideInInspector]
    public ushort Id;

    [HideInInspector]
    public HandRefiller HandRefiller;

    private Image Background;
    private TextMeshProUGUI Title;
    private TextMeshProUGUI Cost;

    public void SetCard(Card card)
    {
        Card = card;
        SyncCardView();
    }

    public void SyncCardView()
    {
        if(Card != null) {
            Background.sprite = Card.Background;
            Cost.transform.parent.gameObject.SetActive(true);
            Title.text = Card.Title;
            Cost.text = Convert.ToString(Card.Cost);
        } else {
            Background.sprite = null;
            Title.text = "";
            Cost.text = "";
            Cost.transform.parent.gameObject.SetActive(false);
        }
    }

    public void SetEmpty()
    {
        Card = null;
        SyncCardView();
    }


    // Start is called before the first frame update
    void Awake()
    {
        Background = transform.Find("Image").GetComponent<Image>();
        Cost = transform.Find("Cost/Text").GetComponent<TextMeshProUGUI>();
        Title = transform.Find("Title/Text").GetComponent<TextMeshProUGUI>();
        SetCard(Card);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandRefiller.PlayCard(Id);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new float3(1.1f, 1.1f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new float3(1f, 1f, 1f);
    }
}
