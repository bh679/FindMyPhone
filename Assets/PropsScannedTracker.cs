using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsScannedTracker : MonoBehaviour
{
	
	public static PropsScannedTracker Instance { get; private set; }

	public Dictionary<string, int> propsScanned = new Dictionary<string, int>();
	
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
	
	public int ScanProp(string id)
	{
		if(!propsScanned.ContainsKey(id))
			propsScanned.Add(id,0);
				
		propsScanned[id]++;
		return propsScanned[id];
	}
}
