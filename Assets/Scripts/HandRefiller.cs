using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class HandRefiller : MonoBehaviour
{
    [ReorderableList]
    [SerializeField]
    public List<Card> Deck;
    public List<GameObject> CardHolderList;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < CardHolderList.Count; i++)
        {
            DrawCard(i);
        }
    }

    [Button("Update Cards")]
    void UpdateCardViews()
    {
        foreach(Transform cardHolder in this.gameObject.transform)
        {
            cardHolder.GetComponent<UI_CardHolder>().SyncCardView();
        }
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
            UpdateCardViews();
        #endif

        if(Input.GetKeyDown(KeyCode.Alpha1))
            PlayCard(0);

        else if(Input.GetKeyDown(KeyCode.Alpha2))
            PlayCard(1);

        else if(Input.GetKeyDown(KeyCode.Alpha3))
            PlayCard(2);

        else if(Input.GetKeyDown(KeyCode.Alpha4))
            PlayCard(3);

    }
    public void DrawCard(int cardPos)
    {
        var holder = CardHolderList[cardPos];
        var card = Deck[UnityEngine.Random.Range(0, Deck.Count-1)];
        holder.GetComponent<UI_CardHolder>().SetCard(card);
        holder.SetActive(true);
    }

    public void PlayCard(int cardPos)
    {
        var holder = CardHolderList[cardPos];
        holder.SetActive(false);
        holder.GetComponent<UI_CardHolder>().SetEmpty();
        DrawCard(cardPos);
    }
}
