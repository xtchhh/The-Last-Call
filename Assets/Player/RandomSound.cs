using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [Header("Assign audio clips to be played at random")]
    public AudioClip[] soundClips;

    [Header("Sound that plays every 120 seconds")]
    public AudioClip specialClip;

    [Header("Audio source settings")]
    [Range(0f, 1f)]
    public float minVolume = 0.5f;
    [Range(0f, 1f)]
    public float maxVolume = 1.0f;

    private AudioSource audioSource;
    private float timer = 0f;
    private float nextPlayInterval = 60f; // Will be randomized between 60 and 120
    private float specialTimer = 0f;
    private const float specialInterval = 120f;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound (global)
        audioSource.loop = false;
        SetNextInterval();
    }

    void Update()
    {
        timer += Time.deltaTime;
        specialTimer += Time.deltaTime;

        // Play random sound at random interval (60 or 120 seconds)
        if (timer >= nextPlayInterval && !audioSource.isPlaying)
        {
            PlayRandomSound();
            timer = 0f;
            SetNextInterval();
        }

        // Play special sound every 120 seconds
        if (specialClip != null && specialTimer >= specialInterval && !audioSource.isPlaying)
        {
            PlaySpecialSound();
            specialTimer = 0f;
        }
    }

    /// <summary>
    /// Sets the next interval to either 60 or 120 seconds at random.
    /// </summary>
    private void SetNextInterval()
    {
        nextPlayInterval = Random.value < 0.5f ? 60f : 120f;
    }

    /// <summary>
    /// Plays a random sound from the list at a random volume.
    /// Ensures the whole sound is played.
    /// </summary>
    public void PlayRandomSound()
    {
        if (soundClips == null || soundClips.Length == 0)
        {
            Debug.LogWarning("No sound clips assigned to RandomSound.");
            return;
        }

        int index = Random.Range(0, soundClips.Length);
        audioSource.clip = soundClips[index];
        audioSource.volume = Random.Range(minVolume, maxVolume);
        audioSource.Play();
    }

    /// <summary>
    /// Plays the special sound at a random volume.
    /// Ensures the whole sound is played.
    /// </summary>
    public void PlaySpecialSound()
    {
        audioSource.clip = specialClip;
        audioSource.volume = Random.Range(minVolume, maxVolume);
        audioSource.Play();
    }
}