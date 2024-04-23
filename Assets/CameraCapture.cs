using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace BrennanHatton.PhotoPrinter
{
	/// <summary>
	/// A UnityEvent with a Grabber as the parameter
	/// </summary>
	[System.Serializable]
	public class Texture2DEvent : UnityEvent<Texture2D> { }
 
	public class CameraCapture : MonoBehaviour
	{
		public int fileCounter;
		public KeyCode screenshotKey;
		public Camera Camera;
		public AudioClip snapClip;
		public AudioSource source;
		public float delay = 0; 
		public Texture2DEvent onCapture;
		
		public bool ready = true;
	 
		private void LateUpdate()
		{
			if (Input.GetKeyDown(screenshotKey))
			{
				Capture();
			}
		}
	 
		public void Capture()
		{
			if(!ready)
				return;
				
			ready = false;
			
			source.PlayOneShot(snapClip);
			
			RenderTexture activeRenderTexture = RenderTexture.active;
			RenderTexture.active = Camera.targetTexture;
	 
			Camera.Render();
	 
			Texture2D image = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height);
			image.ReadPixels(new Rect(0, 0, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
			image.Apply();
			RenderTexture.active = activeRenderTexture;
	 
			byte[] bytes = image.EncodeToPNG();
			Destroy(image);
	 
			File.WriteAllBytes(Application.dataPath + "/Snaps/" + fileCounter + ".png", bytes);
			fileCounter++;
			
			onCapture.Invoke(image);
			
			StartCoroutine(prepare());
		}
		
		IEnumerator prepare()
		{
			yield return new WaitForSeconds(delay);
			
			ready = true;
			
			yield return null;
		}
	}

}