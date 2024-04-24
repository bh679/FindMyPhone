using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.PhotoPrinter
{
	 
	public class PhotoPrinter : MonoBehaviour
	{
		
		public CameraCapture camera;
		public PhotoPrint printPrefab;
		public Transform spawnPoint;
		public float delay = 1f;
		public float printDistance = 0.5f, printTime = 2f;
		
		public List<PhotoPrint> prints = new List<PhotoPrint>();
		
		void Start()
		{
			camera.onCapture.AddListener(Print);
		}
	    
		public void Print(Texture2D image)
		{
			StartCoroutine(printAfterTime(image, delay));
		}
		
		IEnumerator printAfterTime(Texture2D image, float time)
		{
			if(prints.Count > 0)
			prints[prints.Count-1].Drop();
			
			Debug.Log(image);
			Texture2D texture = CopyTexture(image);
			Debug.Log(texture);
			yield return new WaitForSeconds(time);
			
			Debug.Log(texture);
			
			PhotoPrint print = Instantiate(printPrefab,spawnPoint.position,spawnPoint.rotation,spawnPoint);
			print.image.texture = texture;
			print.Print(printDistance, printTime);
			
			prints.Add(print);
		}
		
		Texture2D CopyTexture(Texture2D source)
		{
			// Create a new empty texture with the same dimensions and format
			Texture2D copy = new Texture2D(source.width, source.height, source.format, source.mipmapCount > 1);
			// Copy the pixel data from the source texture to the new one
			Graphics.CopyTexture(source, copy);
			// Apply changes to the new texture
			copy.Apply();

			return copy;
		}

	}
}