using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float margin = 0.5f; // un poco fuera del borde, para que no desaparezca "a la vista"

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        GetComponent<Rigidbody>().linearVelocity = transform.right * speed;
    }

    void Update()
    {
        // Límites de lo que ve la cámara
        float halfHeight = cam.orthographicSize;
        float halfWidth  = halfHeight * cam.aspect;

        Vector3 pos = transform.position;

        // Si se sale por cualquier lado, se destruye
        if (pos.x >  halfWidth + margin || pos.x < -halfWidth - margin ||
            pos.y >  halfHeight + margin || pos.y < -halfHeight - margin)
        {
            Destroy(gameObject);
        }
    }
    
    // Se ejecuta sola cuando el trigger de la bala toca otro collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Hit();
            Destroy(gameObject); // la bala también desaparece
        }
    }
}