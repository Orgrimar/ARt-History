using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceItem : MonoBehaviour
{
    [SerializeField] private Text screen;
    /*[SerializeField] private GameObject _percistance;
    public GameObject _Percistance
    {
        get { return _percistance; }
        set { _percistance = value; }
    }

    [SerializeField] private GameObject _nuit;
    public GameObject _Nuit
    {
        get { return _nuit; }
        set { _nuit = value; }
    }
    [SerializeField] private GameObject _fille;
    public GameObject _Fille
    {
        get { return _fille; }
        set { _fille = value; }
    }*/
    [SerializeField] private GameObject _guernica;
    public GameObject _Guernica
    {
        get { return _guernica; }
        set { _guernica = value; }
    }

    public GameObject spawnItem { get; private set; }

    private static readonly List<ARRaycastHit> hit = new List<ARRaycastHit>();
    private ARRaycastManager raycastManager;

    private void Start()
    {
        screen.gameObject.SetActive(false);
    }

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private bool GetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (!GetTouchPosition(out Vector2 touchPosition))
            return;

        if(raycastManager.Raycast(touchPosition, hit, TrackableType.PlaneWithinPolygon))
        {
            var hitPlace = hit[0].pose;
            screen.gameObject.SetActive(true);

            if (spawnItem == null)
                spawnItem = Instantiate(_guernica, hitPlace.position, hitPlace.rotation);
            else
                spawnItem.transform.position = hitPlace.position;
        }
    }
}
