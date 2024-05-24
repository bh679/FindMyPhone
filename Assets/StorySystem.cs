using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using BrennanHatton.Scanning;


public class StorySystem: MonoBehaviour
{
	public AudioSource audioSource;
	public Text textFeild;
		
	// Array of story audio clips
	public Description[] storyClips;
		
	// Frequency of skipping story clips
	public int storyClipFrequency = 0;
		
	public UnityEvent OnStartOfLastStoryClip;
		
	public bool onlyWhenSilent = false;
	public bool startOnFirst = true;
	
	public AudioSourceList voiceSources;
	
	
		
	int storyId = 0;
	int storySkipId = 0;
	float readtime;
	bool isPlaying = false;
	
	void Start()
	{
		if(startOnFirst)
			storySkipId = storyClipFrequency;
	}
	
	
	
	// Update is called once per frame
	void Update()
	{
		if(onlyWhenSilent && voiceSources.IsPlaying(audioSource))
			audioSource.Stop();
	}
		
	public void PlayNextStory_Editor(){
		PlayNextStory();
	}
		
	public bool PlayNextStory()
	{
		Debug.Log("Next - Determine if a story clip should be played");
		// Determine if a story clip should be played
		if(storyId < storyClips.Length && storySkipId >= storyClipFrequency && !isPlaying)
		{
			Debug.Log("Next - if (onlyWhenSilent && audioSource.isPlaying)");
			if (onlyWhenSilent && audioSource.isPlaying)
			{
				storySkipId++;
				return false;
			}
					
			
			Debug.Log("Next - If it is the last story clip, trigger the corresponding event");
			// If it is the last story clip, trigger the corresponding event
			if(storyId == storyClips.Length - 1)
				OnStartOfLastStoryClip.Invoke();
									
			// Set the read time based on the audio clip length
			//readtime = storyClips[storyId].length;
									
			Debug.Log("Next - Reset skip counter and increment story clip index");
			// Reset skip counter and increment story clip index
			storySkipId = 0;
			storyId++;
				
				
			Debug.Log("Next - Play the current story clip");
			// Play the current story clip
			//storyClips[storyId].Play(textFeild, audioSource);
			StartCoroutine(PlayAllLines());
			
			return true;
		}
		else
		{
				
			Debug.Log("Next - Increment skip counter and scan the item " + storySkipId);
				
			// Increment skip counter and scan the item
			storySkipId++;
								
			return false;
		}
				
	}
		
	IEnumerator PlayAllLines()
	{
		isPlaying = true;
		storyClips[storyId-1].StartFromStart();
		float delay;
			
		do
		{
			if(onlyWhenSilent && voiceSources.IsPlaying(audioSource))
			{
				isPlaying = false;
				yield return null;
			}
			
			
			Debug.Log("delay = storyClips[storyId].PlayNextLine(textFeild, audioSource);");
			delay = storyClips[storyId-1].PlayNextLine(textFeild, audioSource);
				
			yield return new WaitForSeconds(delay);
				
		}while(!storyClips[storyId-1].NoMoreLines());
		Debug.Log("while(!storyClips[storyId].NoMoreLines()); - ended");
			
		isPlaying = false;
		yield return null;
	}
}