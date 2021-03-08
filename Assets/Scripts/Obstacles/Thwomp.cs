using UnityEngine;

public class Thwomp : MonoBehaviour
{
    [SerializeField] private float liftSpeed = 0.75f;
    [SerializeField] private float crushSpeed = 5f;
    [SerializeField] private float liftDistance = 3f;

    [SerializeField] private AudioClip thwompSfx = null;

    private AudioSource audioSource;
    private Vector3 up = Vector3.zero;
    private Vector3 startPos = Vector3.zero;
    private bool lifting = true;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
        up = transform.up;
    }

    private void Update()
    {
        Vector3 target = startPos + (lifting ? up * liftDistance : Vector3.zero);
        float dist = (transform.position - target).magnitude;
        float step = Time.deltaTime * (lifting ? liftSpeed : crushSpeed);

        transform.position += Mathf.Min(step, dist) * (lifting ? up : -up);
        if (step > dist)
        {
            if (!lifting)
            {
                if (audioSource != null && thwompSfx != null)
                {
                    audioSource.PlayOneShot(thwompSfx, 0.3f);
                }
            }
            lifting = !lifting;
        }
    }
}
