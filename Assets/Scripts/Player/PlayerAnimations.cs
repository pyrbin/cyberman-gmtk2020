using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public float ShakeIntensity = 0.5f;
    private Animation anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    public void PlayOnDamage()
    {
        anim.Play("on_damage");
        CinemachineShake.ShakeCamera(ShakeIntensity);
    }
}
