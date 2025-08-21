using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    public Button captureButton; // assign in Inspector
    public string fileNamePrefix = "ARPhoto_";

    private void Start()
    {
        if (captureButton != null)
            captureButton.onClick.AddListener(PhotoTakingHandler);
    }

    public void PhotoTakingHandler()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame(); // wait until frame is rendered

        // Create a texture the size of the screen
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        // Encode texture to PNG
        byte[] imageBytes = screenImage.EncodeToPNG();

        // Save to persistent data path
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = fileNamePrefix + timestamp + ".png";
        //string folderPath = "/storage/emulated/0/Pictures/Screenshots";
        // Change to public path
        //string pubfilName = folderPath + fileName;
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        File.WriteAllBytes(filePath, imageBytes);

        Debug.Log("Photo saved to: " + filePath);

#if UNITY_ANDROID

        AndroidJavaClass mediaScan = new AndroidJavaClass("android.media.MediaScannerConnection");
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        mediaScan.CallStatic("scanFile", context, new string[] { filePath }, null, null);
#endif

    }
}
