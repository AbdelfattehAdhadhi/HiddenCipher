using UnityEngine;


[RequireComponent(typeof(AudioSource))]
[DefaultExecutionOrder(1)]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip matchClip;
    [SerializeField] private AudioClip nonMatchClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip flipClip;

    private void OnEnable()
    {
        if(CardMatchManager.Instance != null)
        {
            CardMatchManager.Instance.OnMatchMade += PlayMatchSound;
            CardMatchManager.Instance.OnNoMatchMade += PlayNonMatchSound;
        }
    }

    private void OnDisable()
    {
        if (CardMatchManager.Instance != null)
        {
            CardMatchManager.Instance.OnMatchMade -= PlayMatchSound;
            CardMatchManager.Instance.OnNoMatchMade -= PlayNonMatchSound;
        }
    }

    [ContextMenu("Init")]
    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayMatchSound()
    {
        if (matchClip != null)
        {
            audioSource.PlayOneShot(matchClip);
        }
    }

    public void PlayNonMatchSound()
    {
        if (nonMatchClip != null)
        {
            audioSource.PlayOneShot(nonMatchClip);
        }
    }

    public void PlayWinSound()
    {
        if (winClip != null)
        {
            audioSource.PlayOneShot(winClip);
        }
    }

    public void PlayFlipSound()
    {
        if (flipClip != null)
        {
            audioSource.PlayOneShot(flipClip);
        }
    }
}