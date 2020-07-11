using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private bool randomPitch = false;

    [SerializeField]
    [ShowIf("randomPitch")]
    private float minPitchRange;

    [SerializeField]
    [ShowIf("randomPitch")]
    private float maxPitchRange;

    private AudioSource audioSource;

    private void OnValidate()
    {
        minPitchRange = Mathf.Clamp(minPitchRange, 0.75f, maxPitchRange);
        maxPitchRange = Mathf.Clamp(maxPitchRange, minPitchRange, 3f);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioSource()
    {
        audioSource.Stop();
        if (randomPitch)
            audioSource.pitch = GetRandomPitch();
        audioSource.Play();
    }

    private float GetRandomPitch()
    {
        return Random.Range(minPitchRange, maxPitchRange);
    }
}
