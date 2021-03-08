using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float fanSpeed = 90f;
    [SerializeField] private bool on = true;

    private AudioSource audioSource;
    private float volume = 0f;
    private float volumeVel = 0f;
    private float fanSpeedAcc = 0f;
    private float fanSpeedVel = 0f;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            volume = audioSource.volume;
        }

        if (on)
        {
            fanSpeedVel = fanSpeed;
        }
        else
        {
            audioSource.volume = 0f;
        }
        
    }

    private void Update()
    {
        if (on)
        {
            audioSource.volume = Mathf.SmoothDamp(audioSource.volume, volume, ref volumeVel, 1f);
            fanSpeedVel = Mathf.SmoothDamp(fanSpeedVel, fanSpeed, ref fanSpeedAcc, 1f);
            transform.rotation *= Quaternion.AngleAxis(fanSpeedVel * Time.deltaTime, Vector3.forward);
        }
    }


    public void Toggle()
    {
        on = !on;
    }
}
