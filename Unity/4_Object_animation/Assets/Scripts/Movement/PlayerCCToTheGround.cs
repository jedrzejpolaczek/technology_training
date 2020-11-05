using UnityEngine;

public class PlayerCCToTheGround : MonoBehaviour
{
    public CharacterController controller;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;



    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGround();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

    }

    public bool CheckGround()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
