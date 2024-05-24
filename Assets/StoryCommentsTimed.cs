﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BrennanHatton.Scanning;

public class StoryCommentsTimed : MonoBehaviour
{
	
	public AudioSource audioSource;
	public AudioSource[] checkSources;
	public Text textFeild;
	
	public float initalDelay = 0;
	public Description[] storyClips;
	
	public bool onlyWhenSilent = false;
	
	public bool PlayOnEnable = true;
	
	
	//int storyId = 0;
	int storySkipId = 0;
	float readtime;
	bool isPlaying = false;
	
	void Reset()
	{
		audioSource = this.GetComponentInChildren<AudioSource>();
	}
	
	void OnEnable()
	{
		if(PlayOnEnable)
		{
			StartAudioList();
		}
	}
	
	public void StartAudioList()
	{
		StartCoroutine(runAudioList());
	}
	
	bool SourceIsPlaying()
	{
		for(int i = 0; i < checkSources.Length; i++)
		{
			if(checkSources[i].isPlaying)
				return true;
		}
		
		return false;
	}
	
	
	// Update is called once per frame
	void Update()
	{
		if(onlyWhenSilent && SourceIsPlaying())
			audioSource.Stop();
	}
	
	IEnumerator runAudioList()
	{
		
		yield return new WaitForSeconds(initalDelay);
		
		for(int storyId = 0; storyId < storyClips.Length; storyId++)
		{
			
			storyClips[storyId].StartFromStart();
			float delay;
			
			do
			{
				while(onlyWhenSilent && SourceIsPlaying())
				{
					yield return new WaitForSeconds(1);
				}
				
				Debug.Log("delay = storyClips[storyId].PlayNextLine(textFeild, audioSource);");
				delay = storyClips[storyId].PlayNextLine(textFeild, audioSource);
				
				yield return new WaitForSeconds(delay);
				
			}while(!storyClips[storyId].NoMoreLines());
			Debug.Log("while(!storyClips[storyId].NoMoreLines()); - ended");
		}
		
		yield return null;
	}
	
	
}
