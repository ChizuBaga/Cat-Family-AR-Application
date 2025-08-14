using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTrack : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public GameObject catPrefab;

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnChanged;
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs e)
    {
        foreach (var img in e.added)
        {
            Instantiate(catPrefab, img.transform.position, img.transform.rotation, img.transform);
        }
    }
}
