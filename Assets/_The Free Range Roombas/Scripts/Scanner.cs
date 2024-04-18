using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Scanner : MonoBehaviour
{
	public float recognizingLoadTime = 2f;
	
	ItemDescription itemScanning;
	
	public Text textDisplay;
	public AudioSource audioSource;
	public AudioClip scanningClip, scanedClip;
	public UnityEvent OnStartOfLastStoryClip;
	
	
	public AudioClip[] storyClips;
	public int storyClipFrequency = 0;
	//bool timeForStory = true;
	int storyId = 0;
	int storySkipId = 0;
	
	public LayerMask layerMask;
	
	public float scanDistance = 5f;
	public float scanReloadTime = 0f;
	public Camera camera;
	
	public UnityEvent OnScan = new UnityEvent(), OnScanReadFinish = new UnityEvent();
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	float recognizing = 0;
	
	bool scanned = false;
	bool readingScan = false;
	// See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
	void FixedUpdate()
	{
		
		if(readingScan)
			return;
		// Bit shift the index of the layer (8) to get a bit mask
		//int layerMask = 1 << 8;

		// This would cast rays only against colliders in layer 8.
		// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
		//layerMask = ~layerMask;

		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, scanDistance, layerMask))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			//Debug.Log("Did Hit " + hit.collider.gameObject.name);
			
			ItemDescription itemToScan = hit.collider.gameObject.GetComponent<ItemDescription>();
			
			if(itemToScan != null)
			{
				//Debug.Log("is item ");
				if(itemScanning == itemToScan)
				{
					if(!scanned)
					{
						recognizing+= Time.deltaTime;
						
						//Debug.Log("is recognizing ");
						
						if(recognizing > recognizingLoadTime)
						{
							//scan success sound
							audioSource.PlayOneShot(scanedClip);
							scanned = true;
							readingScan = true;
							OnScan.Invoke();
							camera.gameObject.SetActive(false);
							
							float readtime;
							
							//if 
							if(
								//there are storyclips left to read 
							(storyId < storyClips.Length)
								//and we are not waiting to to skip it
								&& storySkipId >= storyClipFrequency
							)
							{
								//read story clip
								audioSource.PlayOneShot(storyClips[storyId]);
								
								//if last story clip
								if(storyId == storyClips.Length - 1)
									OnStartOfLastStoryClip.Invoke();
								
								//wait for clip to finish
								readtime = storyClips[storyId].length;
								
								//reset story skip id
								storySkipId = 0;
								
								//increase story clip id
								storyId++;
								
							}else
							{
								//actually scan the item
								storySkipId++;
								
								readtime = itemToScan.Scan(textDisplay, audioSource);
								
							}
							
							
							StartCoroutine(waitForScanRead(readtime));
						}
					}
					
				}else
				{
					recognizing = 0;
					itemScanning = itemToScan;
					scanned = false;
					audioSource.PlayOneShot(scanningClip);
				}
			}
			//found a new object
		}
		else
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
			//Debug.Log("Did not Hit");
		}
	}
	
	IEnumerator waitForScanRead(float readtime)
	{
		yield return new WaitForSeconds(readtime);
		OnScanReadFinish.Invoke();
		yield return new WaitForSeconds(scanReloadTime);
		readingScan = false;
		camera.gameObject.SetActive(true);
	}

	public void SetReloadTime(float newTime)
	{
		scanReloadTime = newTime;
	}
    
	/*void OnTriggerEnter(Collider collider)
	{
		ItemDescription newItemToScan = this.collider.gameObject.GetComponent<ItemDescription>();
		
		//if it is an item to scan
		if(newItemToScan != null)
		{
			//get distance to item
			float distance = Vector3.Distance(originForDistance.position,newItemToScan.transform.position);
			
			//make keypair value
			KeyValuePair itemPair = new KeyValuePair<ItemDescription, float>(newItemToScan,distance);
			
			//put in right position
			for(int i = 0 ; i < itemsInView.Count; i++)
			{
				if(itemsInView[i].Value > distance)
				{
					itemsInView.Insert(0,itemPair);
				}
				
				if(i == 0)
				{
					recognizing = 0;
				}
			}
		}
		
	}
    
	void OnTriggerStay(Collider collider)
	{
		ItemDescription newItemToScan = this.collider.gameObject.GetComponent<ItemDescription>();
		
		//if it is an item to scan
		if(newItemToScan != null)
		{
			
		}
		recognizing+= Time.deltaTime;
		
		
		if(recognizing > recognizingLoadTime)
		{
			ScanItem();
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		recognizing = 0
	}*/
}
