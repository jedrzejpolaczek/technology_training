using UnityEngine;

public class EnemyHumanMovement : MonoBehaviour
{
    public PathGameObject path;
    public bool infinitPath = true;
    private int currentPoint = 0;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float ray = 1.0f;

    private CharacterController controller = null;    
    private Animator animator = null;
    
    private Vector3 targetDirection;
    private float dot;
    Quaternion rotation;

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
            int nextPoint = currentPoint + 1;
            if (nextPoint >= path.points.Count)
            {
                nextPoint = 0;
            }
            if ((currentPoint >= path.points.Count) && infinitPath) 
            {
                currentPoint = 0;
                nextPoint = currentPoint + 1;
            }
            

            targetDirection = (path.points[nextPoint].transform.position - transform.position).normalized;
            dot = Vector3.Dot(targetDirection, transform.forward);            

            if (Vector3.Distance(transform.position, path.points[currentPoint].transform.position) > ray)
            {
                animator.SetFloat("speed", 1);
                transform.position += transform.forward * speed * Time.deltaTime;

                rotation = Quaternion.LookRotation(path.points[currentPoint].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
            }
            else if ((dot < 0.99999) || (dot < 0))
            {
                animator.SetFloat("speed", 0);
                rotation = Quaternion.LookRotation(path.points[nextPoint].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);                
            }    
            else
            {
                currentPoint += 1;
            }
        }
    }
}
