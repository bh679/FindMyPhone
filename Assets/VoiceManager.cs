using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace BrennanHatton.Scanning
{
	public class VoiceManager : MonoBehaviour
	{
		public AudioSource source;
		public TMP_Text textAsset;
		public List<Line> toPlay;
		
		public UnityEvent onPlay, onFinishPlay;
		
		public static VoiceManager Instance { get; private set; }

		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
		
		void Reset()
		{
			source = this.GetComponent<AudioSource>();
		}
		
		public void PlayWhenReady(Description description)
		{
			for(int i= 0; i < description.lines.Length; i++)
				toPlay.Add(description.lines[i]);
			
			PlayNext();
		}
		
		public void PlayWhenReady(Line clip)
		{
			toPlay.Add(clip);
			
			PlayNext();
		}
		
		public void PlayNow(Line clip, bool clear = true)
		{
			onPlay.Invoke();
			
			if(clear)
				ClearList();
			
			textAsset.text = "";
			StopAllCoroutines();
			source.Stop();
			
			toPlay.Add(clip);
			
			PlayNext();
		}
		
		void PlayNext()
		{
			if(toPlay.Count == 0)
			{
				onFinishPlay.Invoke();
				return;
			}
			
			source.clip = toPlay[0].audioLine;
			textAsset.text = toPlay[0].textLine;
			
			StartCoroutine(playNextWhenFinished());
		}
		
		IEnumerator playNextWhenFinished()
		{
			while(source.isPlaying)
			{
				yield return new WaitForEndOfFrame();
			}
			
			yield return new WaitForSeconds(toPlay[0].delayafter);
			
			toPlay.RemoveAt(0);
			
			PlayNext();
		}
		
		void ClearList()
		{
			toPlay = new List<Line>();
		}
	}
}