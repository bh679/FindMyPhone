using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventOnDistance : MonoBehaviour
{
	public Transform object1, object2;
	public float distance;
	public UnityEvent whenDistanceReached = new UnityEvent();
	public bool onUpdate;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if(onUpdate)
		    CheckDistance();
    }
    
	public bool CheckDistance()
	{
		if(Vector3.Distance(object1.position, object2.position) < distance)
		{
			whenDistanceReached.Invoke();
			return true;
		}
		
		return false;
	}
	
    
}
