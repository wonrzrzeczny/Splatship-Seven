using UnityEngine;

public class Splat : MonoBehaviour
{
    [SerializeField] private AudioClip[] sfx = null;

    private AudioSource audioSource;
    private bool stopped = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sfx[Random.Range(0, sfx.Length)], 0.4f);
    }

    private void Update()
    {
        if (stopped && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleSystemStopped()
    {
        stopped = true;
    }
}
