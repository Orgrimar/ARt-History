using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ControlleurUI : MonoBehaviour
{
    [SerializeField] private Button Build_toggle_btn;
    [SerializeField] private Button Scan_toggle_btn;
    [SerializeField] private Button Library_btn;
    [SerializeField] private GameObject AR_Session_Origin;

    [SerializeField] private GameObject LibrairieMenu;
    [SerializeField] private Button Scale_btn;
    [SerializeField] private Slider ScaleSlider;
    [SerializeField] private Button Rotate_btn;
    [SerializeField] private Button Validate_btn;

    // Start is called before the first frame update
    void Start()
    {

        // Chargement des composants
        ARPlaneManager planeManager = AR_Session_Origin.GetComponent<ARPlaneManager>();
        ARRaycastManager raycastManager = AR_Session_Origin.GetComponent<ARRaycastManager>();
        PlaceItem placeItem = AR_Session_Origin.GetComponent<PlaceItem>();

        // Default scan mode
        Scan_toggle_btn.gameObject.SetActive(true);

        Build_toggle_btn.gameObject.SetActive(false);
        Library_btn.gameObject.SetActive(false);
        Scale_btn.gameObject.SetActive(false);
        ScaleSlider.gameObject.SetActive(false);
        Rotate_btn.gameObject.SetActive(false);
        Validate_btn.gameObject.SetActive(false);
        planeManager.enabled = false;
        raycastManager.enabled = false;
        placeItem.enabled = false;

        LibrairieMenu.gameObject.SetActive(false);

        // Change mode
        Scan_toggle_btn.onClick.AddListener(() => BuildMode(planeManager, raycastManager, placeItem));
        Build_toggle_btn.onClick.AddListener(() => ScanMode(planeManager, raycastManager, placeItem));
    }

    private void BuildMode(ARPlaneManager planeManager, ARRaycastManager raycastManager, PlaceItem placeItem)
    {
        Scan_toggle_btn.gameObject.SetActive(false);

        Build_toggle_btn.gameObject.SetActive(true);
        Library_btn.gameObject.SetActive(true);
        Scale_btn.gameObject.SetActive(true);
        Rotate_btn.gameObject.SetActive(true);
        planeManager.enabled = true;
        raycastManager.enabled = true;
        placeItem.enabled = true;
    }

    private void ScanMode(ARPlaneManager planeManager, ARRaycastManager raycastManager, PlaceItem placeItem)
    {
        Scan_toggle_btn.gameObject.SetActive(true);

        Build_toggle_btn.gameObject.SetActive(false);
        Library_btn.gameObject.SetActive(false);
        Scale_btn.gameObject.SetActive(false);
        Rotate_btn.gameObject.SetActive(false);
        planeManager.enabled = false;
        raycastManager.enabled = false;
        placeItem.enabled = false;
        LibrairieMenu.gameObject.SetActive(false);
    }
}
