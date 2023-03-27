using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    public AudioSource Footsteps;
    public AudioSource wallrunfoot;
    public PlayerMovement movement;
    public WallRunning wallrun;
    public bool jumpCheck;
    public bool wallCheck;

    // Start is called before the first frame update
    void Start()
    {
        jumpCheck = movement.grounded;
        wallCheck = wallrun.wallrunning;
    }

    // Update is called once per frame
    void Update()
    {
        jumpCheck = movement.grounded;
        wallCheck = wallrun.wallrunning;

        if ( (Input.GetKey("w") == true || Input.GetKey("a") == true || Input.GetKey("s") == true || Input.GetKey("d") == true) && jumpCheck)
        {
            Footsteps.enabled = true;
            Footsteps.loop = true;
        }
        else if ((Input.GetKey("w") == false && Input.GetKey("a")== false && Input.GetKey("s")== false && Input.GetKey("d")== false) || !jumpCheck)
        {
            Footsteps.enabled = false; 
            Footsteps.loop = false;       
        }

        if(wallCheck)
        {
            wallrunfoot.enabled = true;
            wallrunfoot.loop = true;
        }
        else
        {
            wallrunfoot.enabled = false;
            wallrunfoot.loop = false;
        }



    }
}
