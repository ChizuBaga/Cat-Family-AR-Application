using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTrackingAdjustable : MonoBehaviour
{
    [Header("Prefab Settings")]
    [SerializeField] private GameObject prefab; // Your model
    [SerializeField] private float objectScale = 0.02f; // Model size
    [SerializeField] private Vector3 rotationOffset = new Vector3(0f, 180f, 0f); // Facing direction
    [SerializeField] private float distanceFromMarker = 0.1f; // Meters forward from marker

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            string imageName = trackedImage.referenceImage.name;

            if (!spawnedPrefabs.ContainsKey(imageName))
            {
                // Calculate spawn position (closer to marker)
                Vector3 spawnPosition = trackedImage.transform.position +
                                        trackedImage.transform.forward * distanceFromMarker;

                // Apply rotation offset
                Quaternion spawnRotation = trackedImage.transform.rotation *
                                           Quaternion.Euler(rotationOffset);

                // Spawn object
                GameObject newObj = Instantiate(prefab, spawnPosition, spawnRotation);
                newObj.transform.localScale = Vector3.one * objectScale;

                spawnedPrefabs[imageName] = newObj;
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            string imageName = trackedImage.referenceImage.name;

            if (spawnedPrefabs.TryGetValue(imageName, out GameObject obj))
            {
                obj.transform.position = trackedImage.transform.position +
                                         trackedImage.transform.forward * distanceFromMarker;

                obj.transform.rotation = trackedImage.transform.rotation *
                                         Quaternion.Euler(rotationOffset);

                obj.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            string imageName = trackedImage.referenceImage.name;

            if (spawnedPrefabs.ContainsKey(imageName))
            {
                Destroy(spawnedPrefabs[imageName]);
                spawnedPrefabs.Remove(imageName);
            }
        }
    }

    public void ClearAllObjects()
    {
        foreach (var obj in spawnedPrefabs.Values)
        {
            Destroy(obj); // 删除对象
        }
        spawnedPrefabs.Clear(); // 清空记录
    }





}
