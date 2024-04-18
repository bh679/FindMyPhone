using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
	public Text textUI;
	public float charTime = 0.1f;
	public float spaceTime = 0.4f;
	
	public UnityEvent OnComplete = new UnityEvent();
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void Type(string newText)
	{
		StartCoroutine(_Type(newText));
	}
	
	IEnumerator _Type(string newText)
	{
		if(textUI.text != null)
		{
			for(int i = 0; i < textUI.text.Length; i++)
			{
				textUI.text = textUI.text.Remove(textUI.text.Length-1,1);
				
				yield return new WaitForSeconds(charTime/2f);
			}
		}
		
		textUI.text = "";
		
		for(int i = 0; i < newText.Length; i++)
		{
			textUI.text += newText[i];
			
			if(newText[i] == ' ')
				yield return new WaitForSeconds(spaceTime);
			else
				yield return new WaitForSeconds(charTime);
		}
		
		OnComplete.Invoke();
		
		yield return null;
	}
}
