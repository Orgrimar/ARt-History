using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tableaux = new List<GameObject>();

    public GameObject spawnItem { get; private set; }

    private static readonly List<ARRaycastHit> hit = new List<ARRaycastHit>();
    private ARRaycastManager raycastManager;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (!GetTouchPosition(out Vector2 touchPosition))
            return;

        if(raycastManager.Raycast(touchPosition, hit, TrackableType.PlaneWithinPolygon))
        {
            var hitPlace = hit[0].pose;

            if (spawnItem == null)
                spawnItem = Instantiate( Tableaux[1], hitPlace.position, hitPlace.rotation);
            else
                spawnItem.transform.position = hitPlace.position;
        }
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
}
