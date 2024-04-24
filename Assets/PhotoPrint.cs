using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BNG;

namespace BrennanHatton.PhotoPrinter
{
	 
	public class PhotoPrint : MonoBehaviour
	{
		public RawImage image;
		public Grabbable grabbable;
		public Rigidbody rb;
		public float timeToDrop = 5f;
		public bool grabbed = false;
		
		public void Print(float distance, float time)
		{
			StartCoroutine(printing(distance, time));
		}
		
		IEnumerator printing(float distance, float time)
		{
			float distanceLeft = distance;
			
			while(distanceLeft > 0)
			{
				yield return new WaitForSeconds(0.1f);
				
				distanceLeft -= distance * 0.1f / time;
				
				//move
				this.transform.position += this.transform.up * distance * 0.1f / time;
			}
			
			//enable grabbable
			grabbable.enabled = true;
			
			while(timeToDrop > 0)
			{
				timeToDrop -= 0.1f;
				yield return new WaitForSeconds(0.1f);
				
				if(grabbed)
					yield return null; 
			}
			
			Drop();
			
			yield return null; 
		}
		
		public void Drop()
		{
			if(grabbed)
				return;
				
			this.transform.SetParent(this.transform.root);
			rb.isKinematic = false;
			rb.useGravity = true;
		}
		
		
		
	}
}