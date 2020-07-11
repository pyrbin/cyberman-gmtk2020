using System.Collections;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private CharacterController Controller;

    #region API

    public void Jump(float mod = 1f) { Controller.Jump(mod); }
    public void Slide(float dur) { if (!Controller.IsSliding) StartCoroutine(SlideFor(dur)); }
    public void Hover(float dur) { if (!Controller.IsSliding) StartCoroutine(SlideFor(dur, true)); }
    public void Boost(float dur, float mod = 1.5f) { StartCoroutine(BoostFor(dur, mod)); }

    #endregion

    [ShowNativeProperty]
    public Vector2 Velocity => GetComponent<Rigidbody2D>().velocity;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        Controller = GetComponent<CharacterController>();
    }

    IEnumerator SlideFor(float dur, bool hover = false)
    {
        Controller.ToggleSlide(hover);
        yield return new WaitForSeconds(dur);
        Controller.ToggleSlide(hover);
    }

    IEnumerator BoostFor(float dur, float mod)
    {
        Controller.SpeedMod = mod;
        yield return new WaitForSeconds(dur);
        Controller.SpeedMod = null;
    }
}
