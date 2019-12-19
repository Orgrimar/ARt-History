using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceItem : MonoBehaviour
{
    [SerializeField] private GameObject _percistance;
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
    }
    [SerializeField] private GameObject _guernica;
    public GameObject _Guernica
    {
        get { return _guernica; }
        set { _guernica = value; }
    }

    public GameObject spawnItem { get; private set; }

    private static readonly List<ARRaycastHit> hit = new List<ARRaycastHit>();
    private ARRaycastManager raycastManager;

    [SerializeField] private GameObject LibrairieMenu;
    [SerializeField] private Button Library_btn;
    [SerializeField] private Button Guernica_btn;
    [SerializeField] private Button Percistance_btn;
    [SerializeField] private Button JeuneFille_btn;
    [SerializeField] private Button NuitEtoile_btn;

    [SerializeField] private Button Scale_btn;
    [SerializeField] private Slider ScaleSlider;
    [SerializeField] private Button Rotate_btn;
    [SerializeField] private Button Validate_btn;

    [SerializeField] private Text test;

    private int Id;
    public float value;

    private void Start()
    {
        test.gameObject.SetActive(false);
        LibrairieMenu.gameObject.SetActive(false);
        ScaleSlider.gameObject.SetActive(false);
        Validate_btn.gameObject.SetActive(false);
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

        if (raycastManager.Raycast(touchPosition, hit, TrackableType.PlaneWithinPolygon))
        {
            var hitPlace = hit[0].pose;
            switch (Id)
            {
                case 1:
                    if (spawnItem != null)
                        Destroy(spawnItem);
                    if (spawnItem == null)
                        spawnItem = Instantiate(_percistance, hitPlace.position, hitPlace.rotation);
                    break;
                case 2:
                    if (spawnItem != null)
                        Destroy(spawnItem);
                    if (spawnItem == null)
                        spawnItem = Instantiate(_nuit, hitPlace.position, hitPlace.rotation);
                    break;
                case 3:
                    if (spawnItem != null)
                        Destroy(spawnItem);
                    if (spawnItem == null)
                        spawnItem = Instantiate(_fille, hitPlace.position, hitPlace.rotation);
                    break;
                case 4:
                    if (spawnItem != null)
                        Destroy(spawnItem);
                    if (spawnItem == null)
                        spawnItem = Instantiate(_guernica, hitPlace.position, hitPlace.rotation);
                    break;
            }
        }

        Library_btn.onClick.AddListener(() => LibrairiePanel());
        Scale_btn.onClick.AddListener(() => ScaleItem());
        Rotate_btn.onClick.AddListener(() => RotateItem());
    }

    private void LibrairiePanel()
    {
        LibrairieMenu.gameObject.SetActive(true);

        Guernica_btn.onClick.AddListener(() => Picture(4));
        Percistance_btn.onClick.AddListener(() => Picture(1));
        JeuneFille_btn.onClick.AddListener(() => Picture(3));
        NuitEtoile_btn.onClick.AddListener(() => Picture(2));
    }

    private void Picture(int id)
    {
        Id = id;
        LibrairieMenu.gameObject.SetActive(false);
    }

    public void ScaleItem()
    {
        test.gameObject.SetActive(true);
        ScaleSlider.gameObject.SetActive(true);
        Validate_btn.gameObject.SetActive(true);

        Validate_btn.onClick.AddListener(() => OnValidate());
    }

    private void RotateItem()
    {

    }

    private void OnValidate()
    {
        ScaleSlider.gameObject.SetActive(false);
        Validate_btn.gameObject.SetActive(false);
    }

    private void ChangeScale(float _value)
    {
        value = _value;
    }
}
