using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        rb.AddForce(Mouse.current.delta.ReadValue() * force);
    }
}
