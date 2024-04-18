using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerManager : MonoBehaviour
{
	public void ReloadLevel(){
		SceneManager.LoadScene(0);
	}
 
	public void Loadlevel(int levelId)
	{
		SceneManager.LoadScene(levelId);
	}
	
}
