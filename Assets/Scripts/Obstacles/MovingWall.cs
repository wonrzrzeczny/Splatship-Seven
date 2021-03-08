using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float range = 3f;

    private Vector3 right = Vector3.zero;
    private Vector3 startPos = Vector3.zero;

    private float timer = 0f;


    private void Start()
    {
        startPos = transform.position;
        right = transform.right;
    }

    private void Update()
    {
        timer += Time.deltaTime * speed;
        if (timer > 2f * Mathf.PI)
        {
            timer -= 2f * Mathf.PI;
        }
        transform.position = startPos + range * right * Mathf.Sin(timer);
    }
}
