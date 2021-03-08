using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    private float time = 0f;
    private bool triggered = false;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (triggered)
        {
            return;
        }

        if (collider.CompareTag("Player") && collider.attachedRigidbody.velocity.magnitude < 0.1f)
        {
            time += Time.fixedDeltaTime;
            if (time > 0.5f)
            {
                triggered = true;

                int next = SceneManager.GetActiveScene().buildIndex + 1;
                if (next < 10)
                {
                    Menu.LastLevel = next;
                }
                SceneManager.LoadScene(next);
            }
        }
    }
}
