using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip highScoreGameOver;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStartClip()
    {
        audioSource.PlayOneShot(start);
    }

    public void PlayGameOverClip()
    {
        audioSource.PlayOneShot(gameOver);
    }

    public void PlayHighScoreGameOverClip()
    {
        audioSource.PlayOneShot(highScoreGameOver);
    }
}
