using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


	[System.Serializable]
	public class Line
	{
		public string textLine;
		public AudioClip audioLine;
		public float delayafter = 0.5f;

		public void Play(Text textDisplay, AudioSource audioSource)
		{
			/*if(audioLine != null)
				Debug.Log("Play " + " " + audioLine.name);
			else
			Debug.Log("Play " + " " + audioLine);*/
				
			if(textLine != null)
				textDisplay.text = textLine;
			else
				textDisplay.text = "";
		
			if(audioSource != null && audioLine != null)
			{
				audioSource.PlayOneShot(audioLine);
				//Debug.Log("audioSource.PlayOneShot(audioLine);");
			}
		}
		
		public float Length()
		{
			
			if(audioLine != null)
				return audioLine.length;
				
			else return textLine.Length * 0.085f;
		}
	}


	[System.Serializable]
	public class Description
	{
		public Line[] lines;
	
		int nextLineNumber = 0;
		
		public void StartFromStart()
		{
			nextLineNumber = 0;
		}
		
		public bool NoMoreLines()
		{
			return (nextLineNumber >= lines.Length);
			
		}
	
		public float PlayNextLine(Text textDisplay, AudioSource audioSource)
		{
			float delay = lines[nextLineNumber].delayafter + lines[nextLineNumber].Length();
			
			lines[nextLineNumber].Play(textDisplay, audioSource); // right now it only plays one line
			nextLineNumber++;
			
			return delay;
			
		}
		
		public float TotalLength()
		{
			float total = 0;
			
			for(int i = 0; i < lines.Length; i++)
			{
				total += lines[i].delayafter + lines[i].Length();
			}
			
			return total;
		}
		
		
	}

public class ItemDescription : MonoBehaviour
{
	
	
	public Description[] descriptions;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	static int i = 0;
	public float Scan(Text textDisplay, AudioSource audioSource)
	{
		i++;
		
		if(i > descriptions.Length -1)
		 i = descriptions.Length -1;
		
		
		if(descriptions.Length > 1)
			StartCoroutine(PlayAllLines(textDisplay, audioSource, i));
		else
			StartCoroutine(PlayAllLines(textDisplay, audioSource, 0));
		
		return descriptions[i].TotalLength();
		
	}
	
	IEnumerator PlayAllLines(Text textDisplay, AudioSource audioSource, int _i)
	{
		descriptions[_i].StartFromStart();
		float delay;
		
		do
		{
			delay = descriptions[_i].PlayNextLine(textDisplay, audioSource);
			
			yield return new WaitForSeconds(delay);
			
		}while(!descriptions[_i].NoMoreLines());
		
		yield return null;
	}
}
