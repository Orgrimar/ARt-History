using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanInterface : MonoBehaviour
{
    public void Scan()
    {
        gameObject.SetActive(true);
    }

    public void Build()
    {
        gameObject.SetActive(false);
    }
}
