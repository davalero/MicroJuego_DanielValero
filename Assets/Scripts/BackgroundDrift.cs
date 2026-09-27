using UnityEngine;

public class BackgroundDrift : MonoBehaviour
{
    public float amount = 0.3f; // cuánto se desplaza (en unidades)
    public float speed = 0.2f;  // lo lento que va

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Seno y coseno dan un vaivén suave y continuo; con velocidades distintas
        // en X e Y hace una trayectoria "flotante" que no se nota repetitiva
        float x = Mathf.Sin(Time.time * speed) * amount;
        float y = Mathf.Cos(Time.time * speed * 0.7f) * amount;
        transform.position = startPos + new Vector3(x, y, 0);
    }
}