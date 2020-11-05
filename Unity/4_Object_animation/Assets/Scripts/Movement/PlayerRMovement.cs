using UnityEngine;

public class PlayerRMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    public float jumpRaycastDistance = 1.5f;

    private float hAxis = 0;
    private float vAxis = 0;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //every frame
    private void Update()
    {
        Jump();
    }

    //not depande on frame
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movment = new Vector3(0, 0, 0);
        Vector3 newPosition = new Vector3(0, 0, 0);

        if (IsGrounded())
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
        }

        movment = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
        newPosition = rb.position + rb.transform.TransformDirection(movment);
        rb.MovePosition(newPosition);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }

    public bool IsGrounded()
    {
        //Debug.DrawRay(transform.position, Vector3.down * jumpRaycastDistance, Color.blue);
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);
    }
}
