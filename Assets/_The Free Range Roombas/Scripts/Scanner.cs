using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BrennanHatton.Scanning
{

	public class Scanner : MonoBehaviour
	{
		// Time required to recognize an item
		public float recognizingLoadTime = 2f;
		
		// Currently scanning item
		ItemDescription itemScanning;
		
		// UI text to display item information
		public Text textDisplay;
		// Source for playing audio clips
		public AudioSource audioSource;
		// Audio clips for scanning and scanned sounds
		public AudioClip scanningClip, scanedClip;
		// Event triggered at the start of the last story clip
		public UnityEvent OnStartOfLastStoryClip;
		
		// Array of story audio clips
		public AudioClip[] storyClips;
		// Frequency of skipping story clips
		public int storyClipFrequency = 0;
		// Current story clip index
		int storyId = 0;
		// Counter for skipped story clips
		int storySkipId = 0;
		
		// Layer mask to define what objects the scanner can interact with
		public LayerMask layerMask;
		
		// Maximum scanning distance
		public float scanDistance = 5f;
		// Time to wait between scans
		public float scanReloadTime = 0f;
		// Reference to the camera
		public Camera camera;
		
		// Events triggered during scanning
		public UnityEvent OnScanning, OnNotScanning;
		// Events triggered when a scan is successful and when reading is finished
		public UnityEvent OnScan = new UnityEvent(), OnScanReadFinish = new UnityEvent();
		// UI image to show scanning animation
		public Image scanningAnimationImage;
		
		
		// Recognition progress
		float recognizing = 0;
		
		// State flags for scanning
		bool scanned = false;
		bool readingScan = false;
		
		// FixedUpdate is called every physics step
		void FixedUpdate()
		{
			// Exit if currently processing a reading scan
			if(readingScan)
				return;
			
			// Update the fill amount of the scanning animation based on recognition progress
			if(scanningAnimationImage != null)
				scanningAnimationImage.fillAmount = recognizing / recognizingLoadTime;
	
			// Prepare for a raycast
			RaycastHit hit;
			// Perform the raycast forward from the scanner's position up to a certain distance
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, scanDistance, layerMask))
			{
				// Visualize the raycast in the scene view for debugging
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
				
				// Attempt to get an ItemDescription component from the hit object
				ItemDescription itemToScan = hit.collider.gameObject.GetComponent<ItemDescription>();
				
				// Process the item if found
				if(itemToScan != null)
				{
					// Check if the same item is still under scan
					if(itemScanning == itemToScan)
					{
						// Check if the item has not been fully scanned yet
						if(!scanned)
						{
							// Increase recognition progress
							recognizing += Time.deltaTime;
							// Trigger the scanning event
							OnScanning.Invoke();
							
							// Check if the item has been recognized
							if(recognizing > recognizingLoadTime)
							{
								// Trigger scan success event
								OnScan.Invoke();
								
								// Play scanned sound
								audioSource.PlayOneShot(scanedClip);
								scanned = true;
								readingScan = true;
								
								// Temporarily disable the camera
								camera.gameObject.SetActive(false);
								
								float readtime;
								
								// Determine if a story clip should be played
								if(storyId < storyClips.Length && storySkipId >= storyClipFrequency)
								{
									// Play the current story clip
									audioSource.PlayOneShot(storyClips[storyId]);
									
									// If it is the last story clip, trigger the corresponding event
									if(storyId == storyClips.Length - 1)
										OnStartOfLastStoryClip.Invoke();
									
									// Set the read time based on the audio clip length
									readtime = storyClips[storyId].length;
									
									// Reset skip counter and increment story clip index
									storySkipId = 0;
									storyId++;
								}
								else
								{
									// Increment skip counter and scan the item
									storySkipId++;
									
									// Start reading the scanned item
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
				OnNotScanning.Invoke();
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
		
		public void ResetStory(AudioClip[] newStory, int frequency, UnityEvent onFinish)
		{
			storyId = 0;
			storyClips = newStory;
			storyClipFrequency = frequency;
			OnStartOfLastStoryClip = onFinish;
		}
	}

}
