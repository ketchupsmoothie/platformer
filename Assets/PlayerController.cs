using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private float sensitivity = 2.0f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI coinDisplay;
    private float rotationY = 0;
    private Rigidbody rb;
    private int coins = 0;

    private bool isOnGround()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }

    private void resetPlayer()
    {
        transform.position = Vector3.up;
        rb.velocity = Vector3.zero;
        rotationY = 0;
        updateCoins(0);
    }

    private void updateCoins(int newValue)
    {
        coins = newValue;
        coinDisplay.text = coins + " coins";
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

        if (transform.position.y < -5)
            resetPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
            resetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            updateCoins(coins + 1);
        }
    }
}
