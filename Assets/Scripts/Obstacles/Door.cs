using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float liftSpeed = 1f;
    [SerializeField] private float liftDistance = 3f;

    [SerializeField] private AudioClip doorSfx = null;

    private AudioSource audioSource;
    private Vector3 up = Vector3.zero;
    private Vector3 startPos = Vector3.zero;
    private bool lifting = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
        up = transform.up;
    }

    private void Update()
    {
        if (!lifting)
        {
            return;
        }

        Vector3 target = startPos + up * liftDistance;
        float dist = (transform.position - target).magnitude;
        float step = Time.deltaTime * liftSpeed;

        transform.position += Mathf.Min(step, dist) * up;
        if (step > dist)
        {
            lifting = false;
        }
    }


    public void Open()
    {
        if (audioSource != null && doorSfx != null)
        {
            audioSource.PlayOneShot(doorSfx, 0.3f);
        }
        lifting = true;
    }
}
