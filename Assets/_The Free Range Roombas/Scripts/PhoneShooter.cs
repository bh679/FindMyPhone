using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneShooter : MonoBehaviour
{
	public List<Rigidbody> phones;
	
	public Transform startPoint;
	
	public float velocity;
	
	int i = 0;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void Shoot()
	{
		phones[i].transform.forward = startPoint.forward;
		phones[i].velocity = startPoint.forward * (velocity/4f + velocity*3/4f*Random.value);
		phones[i].angularVelocity = new Vector3(Random.value*2f,Random.value*2f,Random.value*2f);
		
		phones[i].transform.position = startPoint.position;
		
		i++;
		
		if(i > phones.Count-1)
		 i = 0;
	}
}
