using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float destroyMargin = 2f; // cuánto fuera de pantalla puede estar antes de borrarse
    public int points = 10;

    // Para la fragmentación de meteorítos 
    public int size = 3;          // 3 grande, 2 mediano, 1 pequeño
    public int fragments = 2;     // en cuántos trozos se parte
    public float fragmentScale = 0.6f; // tamaño de cada trozo respecto al padre
    public GameObject explosionPrefab;

    Rigidbody rb;
    Camera cam;

    void Awake()
    {
        // Awake va antes que Start: así el Rigidbody ya está listo cuando el spawner llame a Launch
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

     // Lo llama la bala al golpearlo
        public void Hit()
    {
        ScoreManager.Instance.AddPoints(points);

        // Si aún no es el tamaño mínimo, se parte en trozos
        if (size > 1)
        {
            for (int i = 0; i < fragments; i++)
            {
                // Crea una copia de sí mismo en la misma posición
                GameObject frag = Instantiate(gameObject, transform.position, Quaternion.identity);

                // La copia es más pequeña y de un tamaño menos
                frag.transform.localScale = transform.localScale * fragmentScale;
                Enemy e = frag.GetComponent<Enemy>();
                e.size = size - 1;

                // Sale disparada en una dirección aleatoria
                Vector3 dir = Random.insideUnitCircle.normalized;
                e.Launch(dir);
            }
        }

        Explode();
    }

    // El spawner llama a esto justo al crearlo, diciéndole hacia dónde ir
    public void Launch(Vector3 direction)
    {
        rb.linearVelocity = direction * Random.Range(minSpeed, maxSpeed);
        rb.angularVelocity = new Vector3(0, 0, Random.Range(-2f, 2f)); // giro aleatorio
    }

    // Crea la explosión y destruye el meteorito
    public void Explode()
    {
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void Update()
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth  = halfHeight * cam.aspect;
        Vector3 pos = transform.position;

        if (pos.x >  halfWidth + destroyMargin || pos.x < -halfWidth - destroyMargin ||
            pos.y >  halfHeight + destroyMargin || pos.y < -halfHeight - destroyMargin)
        {
            Destroy(gameObject);
        }
    }
}