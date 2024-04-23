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
		public float printDistance = 0.5f, printTime = 2f;
		
		void Start()
		{
			camera.onCapture.AddListener(Print);
		}
	    
		public void Print(Texture2D image)
		{
			PhotoPrint print = Instantiate(printPrefab,spawnPoint.position,spawnPoint.rotation,spawnPoint);
			print.image.texture = image;
			print.Print(0.5f, printTime);
		}
	}
}