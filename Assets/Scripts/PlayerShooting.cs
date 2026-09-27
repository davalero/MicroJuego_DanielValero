using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // la plantilla de la bala
    public float spawnDistance = 0.6f; // cuánto por delante de la nave sale

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        // wasPressedThisFrame = solo una bala por pulsación
        if (kb.spaceKey.wasPressedThisFrame)
        {
            // Posición: delante del morro (que apunta a la derecha)
            Vector3 spawnPos = transform.position + transform.right * spawnDistance;

            // Crear una copia de la bala con la misma rotación que la nave
            Instantiate(bulletPrefab, spawnPos, transform.rotation);
        }
    }
}