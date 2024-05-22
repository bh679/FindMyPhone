using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BrennanHatton.Scanning{

	public class StoryClipReset : MonoBehaviour
	{
		public Scanner scanner;
		
		public AudioClip[] storyClips;
		public int storyClipFrequency = 0;
		
		public UnityEvent OnStartOfLastStoryClip;
		
		public void Set()
		{
			scanner.ResetStory(storyClips, storyClipFrequency, OnStartOfLastStoryClip);
		}
	}

}