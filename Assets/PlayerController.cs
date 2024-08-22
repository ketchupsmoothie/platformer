using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private float sensitivity = 2.0f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float rotationY = 0;
    private Rigidbody rb;

    private bool isOnGround()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.y);

        float forward = Input.GetAxisRaw("Vertical") * moveSpeed;
        float strafe = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float vertical = rb.velocity.y;

        if(isOnGround() && Input.GetKey(KeyCode.Space))
        {
            vertical = jumpPower;
        }

        Vector3 move = transform.TransformDirection(strafe, 0, forward);
        move.y = vertical;
        rb.velocity = move; 
    }
}
