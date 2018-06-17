using UnityEngine;
using System.Collections;

public class WindManager : MonoBehaviour
{

    public int windSpeed;
    public int windDirection;
    public GameObject indicator;

    private void Start()
    {
        InvokeRepeating("SetIndicator", 0f, 0.2f);
    }

    public void SetWindSpeed(int newSpeed)
    {
        windSpeed = newSpeed;
    }

    public void SetWindDirection(int newDirection)
    {
        windDirection = newDirection;
    }

    void SetIndicator()
    {
        indicator.transform.eulerAngles = new Vector3(indicator.transform.eulerAngles.x, indicator.transform.eulerAngles.y, windDirection);
    }
}