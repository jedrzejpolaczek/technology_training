using UnityEngine;

public class PlayerCCMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private float x = 0;
    private float z = 0;

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGround();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if (isGrounded)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

    }

    public bool CheckGround()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
