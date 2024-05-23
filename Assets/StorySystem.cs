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
	public Line[] storyClips;
		
	// Frequency of skipping story clips
	public int storyClipFrequency = 0;
		
	public UnityEvent OnStartOfLastStoryClip;
		
	public bool onlyWhenSilent = false;
		
	int storyId = 0;
	int storySkipId = 0;
	float readtime;
		
	public void Next(){
		NextBool();
	}
		
	public bool NextBool()
	{
		Debug.Log("Next - Determine if a story clip should be played");
		// Determine if a story clip should be played
		if(storyId < storyClips.Length && storySkipId >= storyClipFrequency)
		{
			Debug.Log("Next - if (onlyWhenSilent && audioSource.isPlaying)");
			if (onlyWhenSilent && audioSource.isPlaying)
				return false;
					
			Debug.Log("Next - Play the current story clip");
			// Play the current story clip
			storyClips[storyId].Play(textFeild, audioSource);
									
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
				
			return true;
		}
		else
		{
				
			Debug.Log("Next - Increment skip counter and scan the item");
				
			// Increment skip counter and scan the item
			storySkipId++;
								
			return false;
		}
				
	}
}