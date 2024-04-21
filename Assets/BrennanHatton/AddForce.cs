using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
	public Rigidbody rb;
	public float thrust = 100;
	
	void Reset()
	{
		rb = this.gameObject.GetComponent<Rigidbody>();
	}
	
    // Start is called before the first frame update
    void Start()
    {
	    if(!rb)
		    rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void AddForceLocalForward()
	{
		rb.AddForce(rb.transform.forward * thrust);
	}
    
	public void AddForceLocalUp()
	{
		rb.AddForce(rb.transform.up * thrust);
	}
    
	public void AddForceRotationForward()
	{
		rb.AddTorque(rb.transform.forward * thrust);
	}
    
	public void AddForceRotationRight()
	{
		rb.AddTorque(rb.transform.right * thrust);
	}
    
	public void AddForceRotationUp()
	{
		rb.AddTorque(rb.transform.up * thrust);
	}
}
