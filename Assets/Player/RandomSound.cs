using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [Header("Assign audio clips to be played at random")]
    public AudioClip[] soundClips;

    [Header("Sound that plays at random intervals (60, 90, or 120 seconds)")]
    public AudioClip specialClip;

    [Header("Audio source settings")]
    [Range(0f, 1f)]
    public float minVolume = 0.5f;
    [Range(0f, 1f)]
    public float maxVolume = 1.0f;

    private AudioSource audioSource;
    private float timer = 0f;
    private float nextPlayInterval = 60f; // Will be randomized between 30, 60, and 120
    private float specialTimer = 0f;
    private float nextSpecialInterval = 120f; // Will be randomized between 60, 90, and 120
    private Coroutine playPartialCoroutine;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound (global)
        audioSource.loop = false;
        SetNextInterval();
        SetNextSpecialInterval();
    }
        
    void Update()
    {
        timer += Time.deltaTime;
        specialTimer += Time.deltaTime;

        // Play random sound at random interval (30, 60, or 120 seconds)
        if (timer >= nextPlayInterval && !audioSource.isPlaying && playPartialCoroutine == null)
        {
            PlayRandomSound();
            timer = 0f;
            SetNextInterval();
        }

        // Play special sound at random interval (60, 90, or 120 seconds)
        if (specialClip != null && specialTimer >= nextSpecialInterval && !audioSource.isPlaying && playPartialCoroutine == null)
        {
            PlaySpecialSound();
            specialTimer = 0f;
            SetNextSpecialInterval();
        }
    }

    /// <summary>
    /// Sets the next interval to either 30, 60, or 120 seconds at random.
    /// </summary>
    private void SetNextInterval()
    {
        float[] intervals = { 30f, 60f, 120f };
        int idx = Random.Range(0, intervals.Length);
        nextPlayInterval = intervals[idx];
    }

    /// <summary>
    /// Sets the next special interval to either 60, 90, or 120 seconds at random.
    /// </summary>
    private void SetNextSpecialInterval()
    {
        float[] intervals = { 60f, 90f, 120f };
        int idx = Random.Range(0, intervals.Length);
        nextSpecialInterval = intervals[idx];
    }

    /// <summary>
    /// Plays a random sound from the list at a random volume.
    /// Randomizes between playing the full audio or half its length.
    /// </summary>
    public void PlayRandomSound()
    {
        if (soundClips == null || soundClips.Length == 0)
        {
            Debug.LogWarning("No sound clips assigned to RandomSound.");
            return;
        }

        int index = Random.Range(0, soundClips.Length);
        AudioClip clip = soundClips[index];
        audioSource.clip = clip;
        audioSource.volume = Random.Range(minVolume, maxVolume);

        bool playHalf = Random.value < 0.5f;
        if (playHalf)
        {
            // Play only half the clip
            playPartialCoroutine = StartCoroutine(PlayPartialClip(clip.length * 0.5f));
        }
        else
        {
            // Play full clip
            audioSource.Play();
        }
    }

    /// <summary>
    /// Coroutine to play only part of an audio clip.
    /// </summary>
    private System.Collections.IEnumerator PlayPartialClip(float duration)
    {
        audioSource.Play();
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
        playPartialCoroutine = null;
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