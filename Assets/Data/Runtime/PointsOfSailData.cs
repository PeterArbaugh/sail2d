using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class PointsOfSailData
{
  [SerializeField]
  string name;
  public string Name { get {return name; } set { name = value;} }
  
  [SerializeField]
  float optimalspeed;
  public float Optimalspeed { get {return optimalspeed; } set { optimalspeed = value;} }
  
  [SerializeField]
  float sailanglemin;
  public float Sailanglemin { get {return sailanglemin; } set { sailanglemin = value;} }
  
  [SerializeField]
  float sailanglemax;
  public float Sailanglemax { get {return sailanglemax; } set { sailanglemax = value;} }
  
  [SerializeField]
  float rangemin;
  public float Rangemin { get {return rangemin; } set { rangemin = value;} }
  
  [SerializeField]
  float rangemax;
  public float Rangemax { get {return rangemax; } set { rangemax = value;} }
  
}