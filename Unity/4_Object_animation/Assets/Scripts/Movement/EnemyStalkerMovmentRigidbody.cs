using UnityEngine;

public class EnemyStalkerMovmentRigidbody : MonoBehaviour
{
    private float MoveSpeed = 4;
    private float MaxDist = 10;
    private float MinDist = 3;
    private GameObject Enemy = GameObject.Find("Enemy");

    public Transform player_CC;
    public Transform player_R;
    public Transform player_TPP;

    void Update()
    {
        if (GameObject.Find("PlayerCC").GetComponent<PlayerCCMovement>().enabled)
        {
            followPlayer(player_CC);
        }    
        
        if (GameObject.Find("PlayerR").GetComponent<PlayerRMovement>().enabled)
        {
            followPlayer(player_R);
        }

        if (GameObject.Find("PlayerTPP").GetComponent<PlayerTPPMovement>().enabled)
        {
            followPlayer(player_TPP);
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