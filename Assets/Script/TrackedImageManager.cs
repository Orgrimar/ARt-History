using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]

public class TrackedImageManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imageManager;
    [SerializeField] private Camera mainCanvasCamera;
    [SerializeField] private Texture2D mainDefaultTexture;
    
    public Camera canvasCamera
    {
        get { return mainCanvasCamera; }
        set { mainCanvasCamera = value; }
    }

    public Texture2D defaultTexture
    {
        get { return mainDefaultTexture; }
        set { mainDefaultTexture = value; }
    }


    public void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    public void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }


    void UpdateInfo(ARTrackedImage trackedImage)
    {
        // Set canvas camera
        var canvas = trackedImage.GetComponentInChildren<Canvas>();
        canvas.worldCamera = canvasCamera;

        // Update information about the tracked image
        var text = canvas.GetComponentInChildren<Text>();
        text.text = string.Format(
            "Nom de l'oeuvre: {0}",
            trackedImage.referenceImage.name);

        var planeParentGo = trackedImage.transform.GetChild(0).gameObject;
        var planeGo = planeParentGo.transform.GetChild(0).gameObject;

        // Disable the visual plane if it is not being tracked
        if (trackedImage.trackingState != TrackingState.None)
        {
            planeGo.SetActive(true);

            // The image extents is only valid when the image is being tracked
            trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);

            // Set the texture
            var material = planeGo.GetComponentInChildren<MeshRenderer>().material;
            material.mainTexture = (trackedImage.referenceImage.texture == null) ? defaultTexture : trackedImage.referenceImage.texture;
        }
        else
        {
            planeGo.SetActive(false);
        }
    }

    public void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            // Give the initial image a reasonable default scale
            trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);

            UpdateInfo(trackedImage);
        }

        foreach (var trackedImage in args.updated)
            UpdateInfo(trackedImage);
    }



}
