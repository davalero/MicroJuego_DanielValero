using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f; // segundos entre enemigos
    public float spawnMargin = 1f;   // cuánto fuera de pantalla aparecen

    float timer;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Cuenta el tiempo; cuando llega al intervalo, crea un enemigo y reinicia
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth  = halfHeight * cam.aspect;

        // Elegimos un lado al azar: 0 arriba, 1 abajo, 2 izquierda, 3 derecha
        Vector3 pos = Vector3.zero;
        switch (Random.Range(0, 4))
        {
            case 0: pos = new Vector3(Random.Range(-halfWidth, halfWidth),  halfHeight + spawnMargin, 0); break;
            case 1: pos = new Vector3(Random.Range(-halfWidth, halfWidth), -halfHeight - spawnMargin, 0); break;
            case 2: pos = new Vector3(-halfWidth - spawnMargin, Random.Range(-halfHeight, halfHeight), 0); break;
            case 3: pos = new Vector3( halfWidth + spawnMargin, Random.Range(-halfHeight, halfHeight), 0); break;
        }

        // Apuntamos a un punto aleatorio cerca del centro, para que crucen la pantalla
        Vector3 target = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        Vector3 direction = (target - pos).normalized;

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemy.GetComponent<Enemy>().Launch(direction);
    }
}