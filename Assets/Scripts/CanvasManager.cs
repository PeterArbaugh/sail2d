using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    //Irons Indicator
    public GameObject irons;
    Animator ironsAnim;
    public GameObject player;
    public bool status;
    public GameObject hIndicator;
    Text hText;


	// Use this for initialization
	void Start () {
        //Irons indicator
        ironsAnim = irons.GetComponent<Animator>();
        hText = hIndicator.GetComponent<Text>();
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

        //Health indicator
        hText.text = "Health: " + player.GetComponent<BoatMovement>().health.ToString();
		
	}
}
