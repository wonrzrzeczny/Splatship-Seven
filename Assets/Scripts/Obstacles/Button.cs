using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Door door = null;
    [SerializeField] private Fan fan = null;

    [SerializeField] private AudioClip pressSfx = null;
    [SerializeField] private SpriteRenderer led = null;

    private AudioSource audioSource;
    private float time = 0f;
    private bool triggered = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (triggered)
        {
            return;
        }

        if (collider.CompareTag("Player"))
        {
            time += Time.fixedDeltaTime;
            if (time > 0.1f)
            {
#pragma warning disable UNT0008 // Null propagation on Unity objects
                door?.Open();
                fan?.Toggle();
#pragma warning restore UNT0008 // Null propagation on Unity objects
                triggered = true;

                if (audioSource != null && pressSfx != null)
                {
                    led.color = Color.green;
                    audioSource.PlayOneShot(pressSfx, 1f);
                }
            }
        }
    }
}
