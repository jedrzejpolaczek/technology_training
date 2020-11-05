using UnityEngine;

public class SwitchCammeras : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject cam1;
    public GameObject cam2;


    private void Start()
    {
        player1 = GameObject.Find("First Person Player");
        player2 = GameObject.Find("First Person Player R");

        player2.GetComponent<PlayerMovmentRigidbody>().enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Camera1"))
        {            
            cam1.SetActive(true);
            cam1.GetComponent<AudioListener>().enabled = true;
            cam2.SetActive(false);
            cam2.GetComponent<AudioListener>().enabled = false;

            player2.GetComponent<PlayerMovmentRigidbody>().enabled = false;
            player1.GetComponent<PlayerMovment>().enabled = true;


        }

        if (Input.GetButtonDown("Camera2"))
        {            
            cam1.SetActive(false);
            cam1.GetComponent<AudioListener>().enabled = false;
            cam2.SetActive(true);
            cam2.GetComponent<AudioListener>().enabled = true;            
            
            player1.GetComponent<PlayerMovment>().enabled = false;
            player2.GetComponent<PlayerMovmentRigidbody>().enabled = true;


        }
    }
}
