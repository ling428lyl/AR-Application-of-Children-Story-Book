/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARSubsystems;

public class TrackImage : MonoBehaviour
{
    public ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;

    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();
    private HashSet<string> detectedImages = new HashSet<string>(); // Track detected images

    public TextMeshProUGUI instruction;
    //public TextMeshProUGUI instruction1;
    void awake()
    {
        //trackedImages = GetComponent<ARTrackedImageManager>();
        //instruction1.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            detectedImages.Add(trackedImage.referenceImage.name);
            InstantiateOrUpdateObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            InstantiateOrUpdateObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            detectedImages.Remove(trackedImage.referenceImage.name); // Remove from detected set
            if (instantiatedObjects.ContainsKey(trackedImage.referenceImage.name))
            {
                if (trackedImage.referenceImage.name == "cave")
                {
                    detectedImages.Remove("cave");
                    
                }
                Destroy(instantiatedObjects[trackedImage.referenceImage.name]);
                instantiatedObjects.Remove(trackedImage.referenceImage.name);
            }
        }
    }

    private void InstantiateOrUpdateObject(ARTrackedImage trackedImage)
    {
        if (!instantiatedObjects.ContainsKey(trackedImage.referenceImage.name))
        {
            GameObject prefabToInstantiate = null;
            foreach (var prefab in ArPrefabs)
            {
                if (trackedImage.referenceImage.name == prefab.name)
                {
                    prefabToInstantiate = prefab;
                    break;
                }
            }

            if (prefabToInstantiate != null)
            {
                // Conditionally instantiate based on specific detection
                if (prefabToInstantiate.name == "cave")
                {
                    InstantiatePrefab(trackedImage, prefabToInstantiate);
                    instruction.gameObject.SetActive(false);
                    //instruction1.gameObject.SetActive(true);
                }
                else if (prefabToInstantiate.name == "gruffalo")
                {
                    InstantiatePrefab(trackedImage, prefabToInstantiate);
                }
            }
        }
        else
        {
            // Update the position and orientation of existing object
            instantiatedObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
            instantiatedObjects[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;
        }
    }

    private void InstantiatePrefab(ARTrackedImage trackedImage, GameObject prefab)
    {
        var instantiatedObject = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
        instantiatedObject.name = trackedImage.referenceImage.name;
        instantiatedObjects[trackedImage.referenceImage.name] = instantiatedObject;
    }
}
*/

