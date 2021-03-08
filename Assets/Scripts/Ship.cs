using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    [SerializeField] private GameObject splatPrefab = null;
    [SerializeField] private GameObject selfPrefab = null;

    [SerializeField] private SpriteRenderer thrustBottom = null;
    [SerializeField] private SpriteRenderer thrustLeft = null;
    [SerializeField] private SpriteRenderer thrustRight = null;

    private const float hitResistance = 3f;
    private const float crushDistance = 0.1f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Vector3 startPosition;
    private Vector2 force;
    private bool dead = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Mathf.Max(0f, Input.GetAxis("Vertical"));
        transform.rotation = Quaternion.AngleAxis(-x * 15f, Vector3.forward);
        force = new Vector2(x * 15f, y * 20f);
        audioSource.volume = Mathf.Pow(Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)), 2) * 0.2f;
        thrustBottom.transform.localScale = new Vector3(2f, 5f * Mathf.Abs(y), 1f);
        thrustLeft.transform.localScale = new Vector3(1.5f, 3f * Mathf.Max(x, 0f), 1f);
        thrustRight.transform.localScale = new Vector3(1.5f, 3f * Mathf.Max(-x, 0f), 1f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.isTrigger)
        {
            return;
        }

        float hitImpulse = collision.contacts.Max((ContactPoint2D p) => p.normalImpulse);

        if (hitImpulse > hitResistance || collision.collider.CompareTag("Hot Potato"))
        {
            Death();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider.isTrigger)
        {
            return;
        }

        // Crushing check
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 normal = contact.normal;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, normal, ~LayerMask.GetMask("Ignore Raycast"));
        if (hit.collider != null)
        {
            if (hit.distance < crushDistance)
            {
                Death();
            }
        }
    }


    private void Death()
    {
        if (dead)
        {
            return;
        }
        dead = true;

        Vector3 position = new Vector3(transform.position.x, transform.position.y, 10f);
        Instantiate(splatPrefab, position, Quaternion.identity);
        GameObject next = Instantiate(selfPrefab, startPosition, Quaternion.identity);
        next.name = "Ship";
        Destroy(gameObject);
    }
}
