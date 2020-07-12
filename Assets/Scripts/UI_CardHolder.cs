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

    public Color RarityColor(Card card)
    {
        switch (Card?.Rarity)
        {
            case Rarity.Common:
                return new Color(0.9f, 0.8f, 0.7f);
            case Rarity.Epic:
                return new Color(0f, 0.44f, 0.84f);
            case Rarity.Rare:
                return new Color(0.64f, 0.21f, 0.93f);
            case Rarity.Legendary:
                return new Color(1.00f, 0.50f, 0f);
        }
        return Color.white;
    }

    public void SyncCardView()
    {
        if (Card != null)
        {
            Background.sprite = Card.Background;
            Cost.transform.parent.gameObject.SetActive(true);
            Title.text = Card.Title;
            Cost.text = Convert.ToString(Card.Cost);
            Title.transform.parent.GetComponent<Image>().color = RarityColor(Card);
        }
        else
        {
            Background.sprite = null;
            Title.text = "";
            Cost.text = "";
            Cost.transform.parent.gameObject.SetActive(false);
            Title.transform.parent.GetComponent<Image>().color = Color.white;
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
