using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn in the gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign the clip and volume
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        // Play clip
        audioSource.Play();

        // Get the length of the clip
        float clipLength = audioSource.clip.length;

        // Destroy the audio source after the clip finishes playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
