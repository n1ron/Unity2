using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Geekbrains
{
	public class PhotoController
	{
		private bool _isProcessed;
		private string _path;
		private int _layers = 5;
		private int _resolution = 5;

		public PhotoController()
		{
			_path = Application.dataPath;
		}
		
		private async void Start()
		{
			await DoTapExampleAsync();
		}
    
		private async Task DoTapExampleAsync()
		{
			
			_isProcessed = true;
			
			await WaitSecondsAsync(Time.deltaTime);
			Main.Instance.MainCamera.cullingMask = ~(1 << _layers);
			var sw = Screen.width;
			var sh = Screen.height;
			var sc = new Texture2D(sw, sh, TextureFormat.RGB24, false);
			sc.ReadPixels(new Rect(0, 0, sw, sh), 0, 0);
			var bytes = sc.EncodeToPNG();
			var filename = String.Format("{0:ddMMyyyy_HHmmssfff}.png",
				DateTime.Now);
			File.WriteAllBytes(Path.Combine(_path, filename), bytes);
			await WaitSecondsAsync(2.3f);
			Main.Instance.MainCamera.cullingMask |= 1 << _layers;
			_isProcessed = false;
		}

		private async Task WaitSecondsAsync(float waitTime)
		{
			await Task.Delay(TimeSpan.FromSeconds(waitTime));
		}

		public void FirstMethod()
		{
			var filename = string.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
			ScreenCapture.CaptureScreenshot(Path.Combine(_path, filename),
				_resolution);
		}
		public void SecondMethod()
		{
			Start();
		}
	}
}