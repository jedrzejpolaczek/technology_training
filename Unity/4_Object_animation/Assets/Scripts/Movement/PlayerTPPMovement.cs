using UnityEngine;

public class PlayerTPPMovement : MonoBehaviour
{
    private float speed = 4f;
    private float gravity = 8f;

    Vector3 move_direction = Vector3.zero;

    private CharacterController controller = null;
    private Animator animator = null;


    private float x = 0;
    private float z = 0;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (controller.isGrounded)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("jump");
                move_direction = transform.right * x + transform.forward * z;
                move_direction *= speed;
            }

            if (Input.GetKey (KeyCode.W))
            {
                animator.SetFloat("speed", 1);
                move_direction = transform.right * x + transform.forward * z;
                move_direction *= speed;
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetFloat("speed", 0);
                move_direction = new Vector3(0, 0, 0);
                move_direction *= speed;
            }

            if (Input.GetKey(KeyCode.E))
            {
                animator.SetFloat("speed", 2);
                move_direction = transform.right * x + transform.forward * z;
                move_direction *= speed;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                animator.SetFloat("speed", 0);
                move_direction = new Vector3(0, 0, 0);
                move_direction *= speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                animator.SetFloat("speed", -1);
                move_direction = new Vector3(0, 0, -1);
                move_direction *= speed;
                move_direction = transform.TransformDirection(move_direction);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetFloat("speed", 0);
                move_direction = new Vector3(0, 0, 0);
                move_direction *= speed;
            }
        }

        move_direction.y -= gravity * Time.deltaTime;
        controller.Move(move_direction * Time.deltaTime);
    }
}
