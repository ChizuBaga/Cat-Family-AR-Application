using System.Collections;
using UnityEngine;

public class TakePhoto : MonoBehaviour
{
    public void TakeScreenshotButton()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame(); // Wait until the frame is rendered

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to the Gallery/Photos
        NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) =>
        {
            Debug.Log("Media save result: " + success + " Path: " + path);
        });

        // Free memory
        Destroy(ss);
    }
}
