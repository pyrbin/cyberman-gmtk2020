using FMODUnity;
using NaughtyAttributes;
using UnityEngine;

public enum Rarity
{
    Common = 1 << 0,
    Rare = 1 << 1,
    Epic = 1 << 2,
    Legendary = 1 << 3,
}

public abstract class Card : ScriptableObject
{
    [Header("Properties")]
    public string Title = "Unknown";
    [ResizableTextArea]
    public string Description = "";
    public ushort Cost = 0;
    [EnumFlags]
    public Rarity Rarity = Rarity.Common;

    [BoxGroup("Visuals")]
    [ShowAssetPreview]
    public Sprite Background;

    [EventRef]
    public string SFX_Path = "Unknown";
    [Range(0, 4)]
    public int SFX_Parameter = 0;


    public abstract void OnUse(Player player);
}
