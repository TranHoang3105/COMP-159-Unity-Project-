using UnityEngine;

public class PlaySFX : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    public float playDelay = 0.25f;
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

    private AudioSource audioSource; // keep a reference so we can stop it

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // get or add AudioSource on the Animator's GameObject
        if (audioSource == null)
        {
            audioSource = animator.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = animator.gameObject.AddComponent<AudioSource>();
            }
        }

        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;

        if (playOnEnter && soundToPlay != null)
        {
            audioSource.clip = soundToPlay;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasDelayedSoundPlayed && soundToPlay != null)
        {
            timeSinceEntered += Time.deltaTime;

            if (timeSinceEntered > playDelay)
            {
                audioSource.clip = soundToPlay;
                audioSource.volume = volume;
                audioSource.Play();
                hasDelayedSoundPlayed = true;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            // stop the sound when animation exits
            audioSource.Stop();
        }

        if (playOnExit && soundToPlay != null)
        {
            audioSource.clip = soundToPlay;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}
