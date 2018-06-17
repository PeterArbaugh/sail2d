using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    GameObject player;
    Vector3 pt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        pt = player.transform.position;
        gameObject.transform.position = new Vector3(pt.x, pt.y, -5);
	}
}
