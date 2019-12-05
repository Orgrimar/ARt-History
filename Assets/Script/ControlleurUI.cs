using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlleurUI : MonoBehaviour
{
    [SerializeField] private Button Build_toggle_btn;
    [SerializeField] private Button Scan_toggle_btn;
    [SerializeField] private Button Library_btn;

    // Start is called before the first frame update
    void Start()
    {
        // Default scan mode

        Build_toggle_btn.gameObject.SetActive(false);
        Library_btn.gameObject.SetActive(false);
        Scan_toggle_btn.gameObject.SetActive(true);

        // Change mode

        Scan_toggle_btn.onClick.AddListener(() => BuildMode());
        Build_toggle_btn.onClick.AddListener(() => ScanMode());
    }

    void BuildMode()
    {
        Scan_toggle_btn.gameObject.SetActive(false);
        Build_toggle_btn.gameObject.SetActive(true);
        Library_btn.gameObject.SetActive(true);

    }

    void ScanMode()
    {
        Scan_toggle_btn.gameObject.SetActive(true);
        Build_toggle_btn.gameObject.SetActive(false);
        Library_btn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
