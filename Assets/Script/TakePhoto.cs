using UnityEngine;
using System.Collections;

public class TakePhoto : MonoBehaviour
{
    public void TakeScreenshot()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filename = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        NativeGallery.SaveImageToGallery(ss, "GalleryTest", filename, (success, path) =>
        {
            if (success)
                Debug.Log("Saved to: " + path);
            else
                Debug.Log("Failed to save screenshot!");
        });

        Destroy(ss);
    }
}
