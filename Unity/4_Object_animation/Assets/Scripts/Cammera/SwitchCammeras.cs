using UnityEngine;

public class SwitchCammeras : MonoBehaviour
{
    public GameObject player_CC;
    public GameObject player_R;
    public GameObject player_TPP;

    public GameObject cammera_CC;
    public GameObject cammera_R;
    public GameObject cammera_TPP;
    public GameObject cammera_overlook;


    private void Start()
    {
        player_CC = GameObject.Find("PlayerCC");
        player_R = GameObject.Find("PlayerR");
        player_TPP = GameObject.Find("PlayerTPP");

        player_CC.GetComponent<PlayerCCMovement>().enabled = false;
        player_R.GetComponent<PlayerRMovement>().enabled = false;
        player_TPP.GetComponent<PlayerTPPMovement>().enabled = true;

        cammera_CC.SetActive(false);
        cammera_CC.GetComponent<AudioListener>().enabled = false;
        cammera_R.SetActive(false);
        cammera_R.GetComponent<AudioListener>().enabled = false;
        cammera_TPP.SetActive(true);
        cammera_TPP.GetComponent<AudioListener>().enabled = true;
        cammera_overlook.SetActive(false);
        cammera_overlook.GetComponent<AudioListener>().enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("CameraCC"))
        {
            // Cammeras disable/enable
            cammera_CC.SetActive(true);
            cammera_CC.GetComponent<AudioListener>().enabled = true;
            cammera_R.SetActive(false);
            cammera_R.GetComponent<AudioListener>().enabled = false;
            cammera_TPP.SetActive(false);
            cammera_TPP.GetComponent<AudioListener>().enabled = false;
            cammera_overlook.SetActive(false);
            cammera_overlook.GetComponent<AudioListener>().enabled = false;

            // Players movement disable/enable
            player_R.GetComponent<PlayerRMovement>().enabled = false;
            player_TPP.GetComponent<PlayerTPPMovement>().enabled = false;
            player_CC.GetComponent<PlayerCCMovement>().enabled = true;
        }

        if (Input.GetButtonDown("CameraR"))
        {
            // Cammeras disable/enable
            cammera_CC.SetActive(false);
            cammera_CC.GetComponent<AudioListener>().enabled = false;
            cammera_R.SetActive(true);
            cammera_R.GetComponent<AudioListener>().enabled = true;
            cammera_TPP.SetActive(false);
            cammera_TPP.GetComponent<AudioListener>().enabled = false;
            cammera_overlook.SetActive(false);
            cammera_overlook.GetComponent<AudioListener>().enabled = false;

            // Players movement disable/enable
            player_CC.GetComponent<PlayerCCMovement>().enabled = false;
            player_TPP.GetComponent<PlayerTPPMovement>().enabled = false;
            player_R.GetComponent<PlayerRMovement>().enabled = true;
        }

        if (Input.GetButtonDown("CameraTPP"))
        {
            // Cammeras disable/enable
            cammera_CC.SetActive(false);
            cammera_CC.GetComponent<AudioListener>().enabled = false;
            cammera_R.SetActive(false);
            cammera_R.GetComponent<AudioListener>().enabled = false;
            cammera_TPP.SetActive(true);
            cammera_TPP.GetComponent<AudioListener>().enabled = true;
            cammera_overlook.SetActive(false);
            cammera_overlook.GetComponent<AudioListener>().enabled = false;

            // Players movement disable/enable
            player_CC.GetComponent<PlayerCCMovement>().enabled = false;
            player_R.GetComponent<PlayerRMovement>().enabled = false;
            player_TPP.GetComponent<PlayerTPPMovement>().enabled = true;
        }

        if (Input.GetButtonDown("CameraOverlook"))
        {
            // Cammeras disable/enable
            cammera_CC.SetActive(false);
            cammera_CC.GetComponent<AudioListener>().enabled = false;
            cammera_R.SetActive(false);
            cammera_R.GetComponent<AudioListener>().enabled = false;
            cammera_TPP.SetActive(false);
            cammera_TPP.GetComponent<AudioListener>().enabled = false;
            cammera_overlook.SetActive(true);
            cammera_overlook.GetComponent<AudioListener>().enabled = true;

            // Players movement disable/enable
            player_CC.GetComponent<PlayerCCMovement>().enabled = false;
            player_R.GetComponent<PlayerRMovement>().enabled = false;
            player_TPP.GetComponent<PlayerTPPMovement>().enabled = false;
        }
    }
}
