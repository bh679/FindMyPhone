//originally from https://forum.unity.com/threads/how-to-save-manually-save-a-png-of-a-camera-view.506269/

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
			
			StartCoroutine(_capture());
		}
		
		IEnumerator _capture()
		{
			source.PlayOneShot(snapClip);
			
			RenderTexture activeRenderTexture = new RenderTexture(Camera.targetTexture.width, Camera.targetTexture.height, 24, RenderTextureFormat.RGB565)//ARGB32
			{
				antiAliasing = 4
			};
			
			//RenderTexture activeRenderTexture = RenderTexture.active;
			RenderTexture.active = Camera.targetTexture;
	 
			Camera.Render();
	 
			Texture2D image = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height, TextureFormat.RGB565, false, true);
			image.ReadPixels(new Rect(0, 0, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
			
			image.Apply();
			onCapture.Invoke(image);
			RenderTexture.active = activeRenderTexture;
	 
			byte[] bytes = image.EncodeToPNG();
			Destroy(image);
	 
			File.WriteAllBytes(Application.dataPath + "/Snaps/" + fileCounter + ".png", bytes);
			fileCounter++;
			
			yield return new WaitForSeconds(delay);
			
			ready = true;
			
			yield return null;
		}
	}

}