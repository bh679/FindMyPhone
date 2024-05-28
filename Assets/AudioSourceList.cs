using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceList : MonoBehaviour
{
	public AudioSource[] sources;
	
	public bool IsPlaying(AudioSource exclude = null)
	{
		for(int i = 0; i < sources.Length; i++)
		{
			if(exclude == null || sources[i] != exclude)
				if(sources[i].isPlaying)
					return true;
		}
		
		return false;
	}
	
	public void StopAll()
	{
		for(int i = 0; i < sources.Length; i++)
		{
			sources[i].Stop();
		}
	}
}
