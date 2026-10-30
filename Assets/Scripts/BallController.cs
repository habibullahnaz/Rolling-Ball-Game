using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField, Min(0f)] private float speed = 10f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Read input once per rendered frame, then apply it during the physics step.
        moveInput = Vector2.ClampMagnitude(
            new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")),
            1f
        );
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.AddForce(movement * speed, ForceMode.Force);
    }

    private void OnDisable()
    {
        moveInput = Vector2.zero;
    }
}
