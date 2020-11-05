using UnityEngine;

public class EnemyStalkerMovmentRigidbody : MonoBehaviour
{
    private float MoveSpeed = 4;
    private float MaxDist = 10;
    private float MinDist = 3;
    private GameObject Enemy = GameObject.Find("Enemy");

    public Transform Player1;
    public Transform Player2;

    void Update()
    {
        if (GameObject.Find("First Person Player").GetComponent<PlayerMovment>().enabled)
        {
            followPlayer(Player1);
        }        
        if (GameObject.Find("First Person Player R").GetComponent<PlayerMovmentRigidbody>().enabled)
        {
            followPlayer(Player2);
        }            
    }

    private void followPlayer(Transform Player)
    {
        transform.LookAt(Player);

        float distance_to_player = Vector3.Distance(transform.position, Player.position);
        bool in_max_range = distance_to_player < MaxDist;
        bool in_min_range = distance_to_player > MinDist;

        if (in_max_range && in_min_range)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        else
        {
            Enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}