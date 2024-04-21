using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateRotate : MonoBehaviour
{
	public Transform objectToRotate;
	
	public float startApm = 25,
		speed = 1,
		resistance = 1,
		resetTime = 1,
		offset = 180,
		amp;
	public int resetNumber = 3;
		
	float time;
	
	
    // Start is called before the first frame update
    void Start()
    {
	    amp = startApm; 
	    time = 0;
	    
	    StartCoroutine(ResetTime());
    }

	public void ReStart()
	{
		amp = startApm; 
		time = 0;
	    
		StartCoroutine(ResetTime());
	}

    // Update is called once per frame
    void Update()
	{
		time += Time.deltaTime * speed;
    	
	    objectToRotate.rotation 
		    = Quaternion.EulerRotation(
		    objectToRotate.rotation.x,
		    objectToRotate.rotation.y + Mathf.Sin(time)*amp + offset,
		    objectToRotate.rotation.z);
		    
		if(amp > 0)
			amp -= resistance * Time.deltaTime;
		else
			amp = 0;
	}
    
	IEnumerator ResetTime()
	{
		int repeat = resetNumber;
		
		while(repeat >= 0)
		{
			yield return new WaitForSeconds(resetTime);
			
			amp = startApm; 
			
			repeat--;
		}
	}
}
