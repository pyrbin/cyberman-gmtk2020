using System.Collections;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "ShootCard", menuName = "Card/Shoot Card")]
public class ShootCard : Card
{
    public override void OnUse(Player player)
    {
        player.Shoot();
    }
}
