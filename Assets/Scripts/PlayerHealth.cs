using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public float invulnerableTime = 1.5f; // segundos sin recibir daño tras un golpe
    public TMP_Text livesText;

    int lives;
    bool invulnerable = false;
    SpriteRenderer sr;

    void Start()
    {
        lives = maxLives;
        sr = GetComponent<SpriteRenderer>();
        UpdateText();
    }

    // Se ejecuta sola cuando la nave choca con algo sólido (no trigger)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Explode();
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (invulnerable) return; // si está parpadeando, no le afecta

        lives--;
        UpdateText();

        if (lives <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Blink());
        }
    }

    void Die()
    {
        gameObject.SetActive(false); // la nave desaparece (en el siguiente paso: pantalla de Game Over)
        GameController.Instance.GameOver();
    }

    // Parpadea durante invulnerableTime
    IEnumerator Blink()
    {
        invulnerable = true;
        float t = 0f;
        while (t < invulnerableTime)
        {
            sr.enabled = !sr.enabled; // encender/apagar el dibujo
            yield return new WaitForSeconds(0.1f);
            t += 0.1f;
        }
        sr.enabled = true;
        invulnerable = false;
    }

    void UpdateText()
    {
        livesText.text = "Vidas: " + lives;
    }
}