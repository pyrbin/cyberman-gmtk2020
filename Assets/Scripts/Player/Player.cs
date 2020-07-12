using System.Collections;
using FMODUnity;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnHealthChange : UnityEvent<int> { }

[System.Serializable]
public class OnDeath : UnityEvent { }

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private CharacterController Controller;

    [EventRef]
    public string JUMP_SFX_PATH = "Ambience/FrozenHimalayas/HimalayaAmbience";
    [EventRef]
    public string DAMAGE_SFX_PATH = "Ambience/FrozenHimalayas/HimalayaAmbience";
    [EventRef]
    public string SHOOT_SFX_PATH = "Ambience/FrozenHimalayas/HimalayaAmbience";

    public float ManaReg = 1.5f;

    public int MaxHealth = 3;

    public int Health
    {
        get { return innerHealth; }
        set { innerHealth = math.min(value, MaxHealth); }
    }

    public int MaxMana = 10;

    public int Mana
    {
        get { return innerMana; }
        set { innerMana = math.min(value, MaxMana); }
    }

    public GunFire GunHolder;
    public Transform Wheel;

    public bool ShowEvents;

    [ShowIf("ShowEvents")]
    public OnHealthChange OnHealthChangeEvent;

    [ShowIf("ShowEvents")]
    public OnDeath OnDeathEvent;

    private int innerHealth;
    private int innerMana;

    #region API

    public void Jump(float mod = 1f)
    {
        Controller.Jump(mod);
        FMODUnity.RuntimeManager.PlayOneShot(JUMP_SFX_PATH);
    }
    public void Slide(float dur) { if (!Controller.IsSliding) StartCoroutine(SlideFor(dur)); }
    public void Hover(float dur) { if (!Controller.IsSliding) StartCoroutine(SlideFor(dur, true)); }
    public void Boost(float dur, float mod = 1.5f) { StartCoroutine(BoostFor(dur, mod)); }
    public void PauseMovement(float dur) { StartCoroutine(DontMoveFor(dur)); }
    public void Shoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(SHOOT_SFX_PATH);
        GunHolder.GetComponent<Animation>().Play("gun_fire");
    }


    public bool PlayCard(Card card)
    {
        if (card.Cost > Mana) return false;
        FMODUtil.PlayOneShot(card.SFX_Path, "Card", card.SFX_Parameter);
        card.OnUse(this);
        Mana -= card.Cost;
        return true;
    }

    public void Damage(ushort val)
    {
        Health -= val;
        OnHealthChangeEvent.Invoke(-val);
        FMODUnity.RuntimeManager.PlayOneShot(DAMAGE_SFX_PATH);
        if (IsDead())
        {
            OnDeathEvent.Invoke();
        }
    }

    public void Heal(ushort val)
    {
        Health += val;
        OnHealthChangeEvent.Invoke(val);
    }

    public bool IsDead() { return Health <= 0; }

    #endregion

    [ShowNativeProperty]
    public int CurrentHealth => Health;

    [ShowNativeProperty]
    public Vector2 Velocity => GetComponent<Rigidbody2D>().velocity;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (OnHealthChangeEvent == null)
            OnHealthChangeEvent = new OnHealthChange();

        if (OnDeathEvent == null)
            OnDeathEvent = new OnDeath();

        Controller = GetComponent<CharacterController>();
        Health = MaxHealth;
        Mana = MaxMana;

        StartCoroutine(ManaRegen());
    }

    IEnumerator SlideFor(float dur, bool hover = false)
    {
        Controller.ToggleSlide(hover);
        GunHolder.transform.localPosition = new float3(0f, -0.8f, 0f);
        yield return new WaitForSeconds(dur);
        Controller.ToggleSlide(hover);
        GunHolder.transform.localPosition = new float3(0f, 0.4f, 0f);

    }

    IEnumerator BoostFor(float dur, float mod)
    {
        Controller.SpeedMod = mod;
        yield return new WaitForSeconds(dur);
        Controller.SpeedMod = null;
    }

    IEnumerator ManaRegen()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(ManaReg);
            Mana = math.min(MaxMana, Mana + 1);
        }
    }

    IEnumerator DontMoveFor(float dur)
    {
        Controller.DontMove = true;
        yield return new WaitForSeconds(dur);
        Controller.DontMove = false;
    }
}
