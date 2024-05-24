using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondClassAudioSource : MonoBehaviour
{
	public AudioSource source;
	public AudioSource[] firstClass;

	void Reset()
	{
		source = this.GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
	{
		for(int i = 0; i < firstClass.Length; i++)
			if(firstClass[i].isPlaying)
		    	source.Stop();
    }
}
