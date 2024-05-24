using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Scanning;

public class AudioListToComments : MonoBehaviour
{
    // Start is called before the first frame update
	void Reset()
    {
	    PlayAudioList list = this.GetComponent<PlayAudioList>();
	    
	    StoryCommentsTimed comments = this.GetComponent<StoryCommentsTimed>();

	    if(comments == null)
	    {
		    comments = this.gameObject.AddComponent<StoryCommentsTimed>();
	    	comments.audioSource = list.source;
	    	comments.PlayOnEnable = list.PlayOnEnable;
	    
	    }
	    comments.storyClips = new Description[list.audioList.Count];
	    
	    for(int i =0 ; i < list.audioList.Count; i++)
	    {
	    	comments.storyClips[i] = new Description();
	    	comments.storyClips[i].lines = new Line[1];
	    	comments.storyClips[i].lines[0] = new Line();
	    	comments.storyClips[i].lines[0].audioLine = list.audioList[i].clip;
	    	
	    	if(i == 0)
		    	comments.initalDelay = list.audioList[i].delay;
		    else
		    	comments.storyClips[i-1].lines[0].delayafter = list.audioList[i].delay;
	    }
	    
	    list.enabled = false;
    }

   
}
