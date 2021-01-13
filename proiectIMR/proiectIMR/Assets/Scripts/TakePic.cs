using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using static NativeGallery;

public class TakePic : MonoBehaviour
{
	void Start()
	{
		if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera))
		{
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera);
		}
		if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageWrite))
		{
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageWrite);
		}
	}

		public void TaskOnClick()
	{
		NativeToolkit.SaveScreenshot("MyScreenshot", "MyScreenshotFolder", "jpeg");
		Thread.Sleep(200);
	}
    public void ClickShare()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        //NativeToolkit.SaveScreenshot("MyScreenshot", "MyScreenshotFolder", "jpeg");
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());
        

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Photo").SetText("Share my photo from Isekai app")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();


    }
}
