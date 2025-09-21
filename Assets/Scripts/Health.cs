using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts;
    public Animator animator;

    private int currentHealth;

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (transform.position.y < -4f)
        {
            ResetGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Saw"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (currentHealth <= 0)
            return;

        currentHealth--;

        hearts[currentHealth].SetActive(false);

        if (currentHealth == 0 && animator != null)
        {
            animator.SetTrigger("Death");
        }
    }

    public void ResetGame()
    {
        currentHealth = hearts.Length;

        foreach (GameObject heart in hearts)
        {
            heart.SetActive(true);
        }

        transform.position = new Vector3(-6.1f, -2.25f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = true;
    }
}
