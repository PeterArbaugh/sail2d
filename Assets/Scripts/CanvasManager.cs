using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    //Irons Indicator
    public GameObject irons;
    Animator ironsAnim;
    public GameObject player;
    public bool status;


	// Use this for initialization
	void Start () {
        //Irons indicator
        ironsAnim = irons.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //Irons indicator
        status = player.GetComponent<BoatMovement>().irons;

        if (status)
        {
            ironsAnim.SetBool("InIrons", true);
        }
        if (!status)
        {
            ironsAnim.SetBool("InIrons", false);
        }
		
	}
}
