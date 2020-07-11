using UnityEngine;

[CreateAssetMenuAttribute(fileName = "JumpCard", menuName = "Card/JumpCard")]
public class JumpCard : Card
{
    public float JumpMod = 1f;
    public override void OnUse(Player player)
    {
        player.Jump(JumpMod);
    }
}
