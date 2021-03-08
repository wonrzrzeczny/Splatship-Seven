using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float distance = 4f;
    [SerializeField] private float reload = 1f;
    [SerializeField] private Vector3 direction = Vector3.left;

    [SerializeField] private AudioClip shofSfx = null;

    private AudioSource audioSource;
    private SplatSurface splatSurface;
    private Vector3 startPos;
    private float reloadTimer = 0f;
    private bool reloading = true;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        splatSurface = GetComponent<SplatSurface>();
        direction = Vector3.Normalize(direction);
        startPos = transform.position;
    }

    private void Update()
    {
        if (reloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > reload)
            {
                reloading = false;
                transform.position = startPos;
                splatSurface.Reset();
                if (audioSource != null && shofSfx != null)
                {
                    audioSource.PlayOneShot(shofSfx, 0.3f);
                }
            }
        }
        else
        {
            Vector3 target = startPos + direction * distance;
            float dist = (transform.position - target).magnitude;
            float step = Time.deltaTime * speed;
            
            transform.position += Mathf.Min(step, dist) * direction;
            if (step > dist)
            {
                reloading = true;
                reloadTimer = 0f;
            }
        }
    }
}
