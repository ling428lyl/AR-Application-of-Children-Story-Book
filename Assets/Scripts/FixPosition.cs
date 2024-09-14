using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FixPosition : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject forestPrefab;
    //public GameObject gruffaloPrefab;
    private ARAnchorManager anchorManager;
    private GameObject instantiatedForest;
    //private GameObject instantiatedGruffalo;
    private ARAnchor forestAnchor;
    //private ARAnchor gruffaloAnchor;
    private bool forestPlaced = false;
    //private bool gruffaloPlaced = false;
    public Button confirmButton;
    public TextMeshProUGUI instruction;

    void Start()
    {
        anchorManager = FindObjectOfType<ARAnchorManager>();
        trackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void ToggleForestPlacement()
    {
        forestPlaced = !forestPlaced;

        if (!forestPlaced && instantiatedForest != null)
        {
            Destroy(forestAnchor);
            Destroy(instantiatedForest);
            instantiatedForest = null;
            forestAnchor = null;
        }
        confirmButton.gameObject.SetActive(false);
        instruction.gameObject.SetActive(false);
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            if (trackedImage.referenceImage.name == "cave" && !forestPlaced)
            {

                forestAnchor = anchorManager.AddAnchor(new Pose(trackedImage.transform.position, trackedImage.transform.rotation));
                if (forestAnchor != null)
                {
                    instantiatedForest = Instantiate(forestPrefab, forestAnchor.transform.position, forestAnchor.transform.rotation, forestAnchor.transform);
                    forestPlaced = true;
                }
            }
            /*else if (trackedImage.referenceImage.name == "gruffalo" && !gruffaloPlaced && forestPlaced) // Check if forest is placed and Gruffalo is not placed
            {
                if (instantiatedGruffalo == null)
                {
                    gruffaloAnchor = anchorManager.AddAnchor(new Pose(trackedImage.transform.position, trackedImage.transform.rotation));
                    instantiatedGruffalo = Instantiate(gruffaloPrefab, gruffaloAnchor.transform.position, gruffaloAnchor.transform.rotation, gruffaloAnchor.transform);
                    gruffaloPlaced = true;  // Set Gruffalo placed to true
                }
            }*/
        }
    }

    void OnDestroy()
    {
        trackedImageManager.trackedImagesChanged -= OnImageChanged;
    }
}
