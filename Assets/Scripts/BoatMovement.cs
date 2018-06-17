using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {

    //Get Wind
    public GameObject w;
    int windSpeed;
    int windDirection;

    //Points of sail
    [SerializeField]
    private float heading;
    [SerializeField]
    private float sailAngle;
    public bool irons;
    [SerializeField]
    private PointsOfSailData currentPointOfSail;

    Rigidbody2D rb;
    public float torqueForce = .3f;

    [SerializeField]
    //public float speed = 5f;
    float hSpeedModifier;
    float sSpeedModifier;
    float finalModifier;
    [SerializeField]
    public float optimalSpeed;

    [SerializeField]
    GameObject sail;

    public float sailRotationSpeed;

    public PointsOfSail pointsOfSail;
    List<PointsOfSailData> adjacentPointsOfSail;
    [SerializeField]
    float posAngle;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        adjacentPointsOfSail = new List<PointsOfSailData>();
        InvokeRepeating("getOptimalPointOfSail", 0f, 0.2f);
	}

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate () {
        //set heading of variable and gameObject
        heading = 360 - Mathf.Round(transform.rotation.eulerAngles.z);

        // -1 inverts the rudder controls to feel more natural
        rb.AddTorque(Input.GetAxis("Rudder") * torqueForce * -1);

        //Boom movement & clamping to reasonable angles
        //This got weird so the sail starts with a 180 degree rotation in the editor.
        //The pivot of the sprite is set to the bottom of the sprite.
        sail.transform.Rotate(0, 0, Input.GetAxis("Boom") * Time.deltaTime * sailRotationSpeed);
        sailAngle = sail.transform.localEulerAngles.z;

        //Set posAngle to lock sail according to wind direction
        posAngle = GetPosAngle(heading, windDirection);
        if (180 <= posAngle && posAngle <= 360)
        {
            if (0 < sailAngle && sailAngle < 180)
            {
                sail.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
            else if (255 < sailAngle && sailAngle < 360)
            {
                sail.transform.localEulerAngles = new Vector3(0, 0, 255);
            }
        }
        else if (0 <= posAngle && posAngle < 180)
        {
            if (sailAngle > 0 && sailAngle < 105)
            {
                sail.transform.localEulerAngles = new Vector3(0, 0, 105);
            }
            else if (sailAngle > 180 && sailAngle < 360)
            {
                sail.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
        }

        //Get windspeed and direction
        windSpeed = w.GetComponent<WindManager>().windSpeed;
        windDirection = w.GetComponent<WindManager>().windDirection;
    }

    void getOptimalPointOfSail()
    {
        if (posAngle < pointsOfSail.dataArray[5].Rangemin || posAngle > pointsOfSail.dataArray[6].Rangemax)
        {
            irons = true;
        }

        //assign point of sail
        for (int i = 0; i < pointsOfSail.dataArray.Length; i++)
        {
            if (pointsOfSail.dataArray[i].Rangemin <= posAngle && posAngle <= pointsOfSail.dataArray[i].Rangemax)
            {
                irons = false;
                currentPointOfSail = pointsOfSail.dataArray[i];
                optimalSpeed = windSpeed * pointsOfSail.dataArray[i].Optimalspeed;
                if (0 < i)
                {
                    adjacentPointsOfSail.Add(pointsOfSail.dataArray[i - 1]);
                }
                if (i < pointsOfSail.dataArray.Length - 1)
                {
                    adjacentPointsOfSail.Add(pointsOfSail.dataArray[i + 1]);
                }
                break;
            }
        }
        //check actual point of sail and apply speed
        float f = CheckActualPOS(currentPointOfSail, adjacentPointsOfSail, posAngle, sailAngle);
        Debug.Log("Modifier: " + f);
        Debug.Log("Optimal speed: " + optimalSpeed);
        rb.AddForce(transform.up * f * optimalSpeed);
        Debug.Log("Force: " + (transform.up * f * optimalSpeed));
        
       
    }
    
    //return a modifier for the optimal speed
    float CheckActualPOS(PointsOfSailData optimal, List<PointsOfSailData> adjacent, float h, float sA)
    {
        if (optimal.Rangemin <= h && h <= optimal.Rangemax)
        {
            Debug.Log("Optimal");
            hSpeedModifier = optimal.Optimalspeed;
            finalModifier = hSpeedModifier * CheckSailAngle(optimal, adjacent, sA);
            Debug.Log("Final modifier = " + finalModifier);
            return finalModifier;
            
        }
        else
        {
            foreach (PointsOfSailData item in adjacent)
            {
                if (item.Rangemin <= h && h <= item.Rangemax)
                {
                    Debug.Log("Adjacent");
                    hSpeedModifier = 0.8f;
                    finalModifier = hSpeedModifier * CheckSailAngle(optimal, adjacent, sA);
                    Debug.Log("Adjacent final modifier = " + finalModifier);
                    return finalModifier;
                }
            }
            return 0;
        }
    }
    float GetPosAngle(float h, float w)
    {
        if (h > w)
        {
            return 360 - Mathf.Abs(w - h);
        }
        else if (h < w)
        {
            return 0 + Mathf.Abs(w - h);
        }
        else
        {
            Debug.Log("Error: GetPosAngle");
            return 0;
        }
    }


    float CheckSailAngle(PointsOfSailData optimal, List<PointsOfSailData> adjacent, float sA)
    {
        if (optimal.Sailanglemin <= sA && sA <= optimal.Sailanglemax)
        {
            return 1.4f;
        }
        else
        {
            foreach (PointsOfSailData item in adjacent)
            {
                if (item.Sailanglemin <= sA && sA <= item.Sailanglemax)
                {
                    return 1.0F;
                }
            }
            return 0.5F;
        }
    }
}
