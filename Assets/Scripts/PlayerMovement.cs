using UnityEngine;
using UnityEngine.InputSystem; // sistema de teclas nuevo

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 1f;          // fuerza al avanzar
    public float rotationSpeed = 500f; // velocidad de giro (grados/segundo)
    public float margin = 0.5f;         // distancia al borde
    public float brakeStrength = 3f; // cuanto más alto, más rápido se estabiliza
    public float maxSpeed = 2f; // velocidad tope

    Rigidbody rb;
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return; // por si no hay teclado detectado

        // A/D o flechas: girar
        float turn = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)  turn += 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) turn -= 1f;

        transform.Rotate(0, 0, turn * rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        // W/S o flechas: avanzar / retroceder
        float forward = 0f;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed)   forward += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) forward -= 1f;

        // Hacia la dirección del spirte, en este caso a la dcha
        rb.AddForce(transform.right * forward * thrust);

        // No dejar que supere la velocidad máxima
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);

        // Shift: estabilizar poco a poco
        if (kb.leftShiftKey.isPressed || kb.rightShiftKey.isPressed)
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, brakeStrength * Time.fixedDeltaTime);
        }

        KeepInsideScreen();
    }

    void KeepInsideScreen()
    {
        // Tamaño de lo que ve la cámara (mitad de alto y mitad de ancho)
        float halfHeight = cam.orthographicSize;
        float halfWidth  = halfHeight * cam.aspect;

        Vector3 pos = rb.position;
        Vector3 vel = rb.linearVelocity;

        // Si se pasa por la izquierda/derecha, la devolvemos al borde y frenamos en X
        if (pos.x >  halfWidth - margin)  { pos.x =  halfWidth - margin;  vel.x = 0; }
        if (pos.x < -halfWidth + margin)  { pos.x = -halfWidth + margin;  vel.x = 0; }

        // Lo mismo arriba/abajo en Y
        if (pos.y >  halfHeight - margin) { pos.y =  halfHeight - margin; vel.y = 0; }
        if (pos.y < -halfHeight + margin) { pos.y = -halfHeight + margin; vel.y = 0; }

        rb.position = pos;
        rb.linearVelocity = vel;
    }
}