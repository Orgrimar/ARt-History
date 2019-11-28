using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInterface : MonoBehaviour
{
    public void Build()
    {
        gameObject.SetActive(true);
    }

    public void Scan()
    {
        gameObject.SetActive(false);
    }
}
