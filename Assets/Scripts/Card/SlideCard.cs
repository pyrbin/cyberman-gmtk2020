using System.Collections;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "SlideCard", menuName = "Card/Slide Card")]
public class SlideCard : Card
{

    public float Duration = 1f;
    public bool Hover = false;

    public override void OnUse(Player player)
    {
        if (Hover) player.Hover(Duration); else player.Slide(Duration);
    }
}
