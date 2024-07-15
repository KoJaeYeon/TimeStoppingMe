using UnityEngine;
using System.Collections;
 
public class ZoomInOut : MonoBehaviour 
{	
    public float distance = 50;
    public float sensitivityDistance = 50;
    public float damping = 50;
    public float minFOV = 5;
    public float maxFOV = 100;
 	
	
    void  Start ()
    {
       distance = GetComponent<Camera>().fieldOfView;
    }
    void  Update ()
    {
		
       distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityDistance;
       distance = Mathf.Clamp(distance, minFOV, maxFOV);
       GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, distance,  Time.deltaTime * damping);
    }
	
	///===================================
	
	
	
	
	
	
	
	    
	 
}