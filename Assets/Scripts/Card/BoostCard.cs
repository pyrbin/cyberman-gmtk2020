using System.Collections;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "SlideCard", menuName = "Card/BoostCard")]
public class BoostCard : Card
{
    public float SpeedMod = 2f;
    public float Duration = 1f;

    public override void OnUse(Player player)
    {
        player.Boost(Duration, SpeedMod);
    }
}
