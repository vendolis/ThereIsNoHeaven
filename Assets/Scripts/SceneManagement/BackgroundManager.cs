using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


/*
 *  This background Manager fixes a static background tot he camera and has a variable amount of parallax layers for endless movement in x and Y axis.
 * The code for the infinite parallax is from https://pavcreations.com/parallax-scrolling-in-pixel-perfect-2d-unity-games/
 * Since the camera is not pixel perfect, I did not include that code.
 */

public class parallaxLayerInfo
{
    public GameObject layerGameObject;
    public float layerParallaxFactor;
    public float layerStartX;
    public float layerStartY;
    public float layerSizeX;
    public float layerSizeY;
}

public class BackgroundManager : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCameraBase followCamera;

    [SerializeField] private GameObject staticBackground;

    [SerializeField] private List<GameObject> parallaxLayers;
    [SerializeField] private float parallaxFactor = 0.1f;

    private List<parallaxLayerInfo> parallaxLayerInfos = new List<parallaxLayerInfo>();

   
    // Start is called before the first frame update
    void Start()
    {
        GenerateParallaxLayerInfo();
    }

    private void GenerateParallaxLayerInfo()
    {
        var layerCount = parallaxLayers.Count;

        for (int i = 0; i < layerCount; i++)
        {
            var currentLayerInfo = new parallaxLayerInfo();
            
            currentLayerInfo.layerGameObject = parallaxLayers[i];

            currentLayerInfo.layerStartX = parallaxLayers[i].transform.position.x;
            currentLayerInfo.layerSizeX = parallaxLayers[i].GetComponent<SpriteRenderer>().bounds.size.x;
            currentLayerInfo.layerStartY = parallaxLayers[i].transform.position.y;
            currentLayerInfo.layerSizeY = parallaxLayers[i].GetComponent<SpriteRenderer>().bounds.size.y;
            
            currentLayerInfo.layerParallaxFactor = 0.9f - (i * parallaxFactor); 
            
            parallaxLayerInfos.Add(currentLayerInfo);
        }
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void LateUpdate()
    {
        // Set Static Background
        staticBackground.transform.position = new Vector3(followCamera.transform.position.x, followCamera.transform.position.y, staticBackground.transform.position.z);
        
        UpdateParallaxPosition();
    }

    void UpdateParallaxPosition()
    {
        foreach (var layerInfo in parallaxLayerInfos)
        {
            float tempX     = followCamera.transform.position.x * (1 - layerInfo.layerParallaxFactor);
            float distanceX = followCamera.transform.position.x * layerInfo.layerParallaxFactor;
            float tempY     = followCamera.transform.position.y * (1 - layerInfo.layerParallaxFactor);
            float distanceY = followCamera.transform.position.y * layerInfo.layerParallaxFactor;
 
            Vector3 newPosition = new Vector3(layerInfo.layerStartX + distanceX, 
                layerInfo.layerStartY + distanceY,
                layerInfo.layerGameObject.transform.position.z);
 
            layerInfo.layerGameObject.transform.position = newPosition;
 
            // Adjust Start X position for infinite Scroll 
            if (tempX > layerInfo.layerStartX + (layerInfo.layerSizeX / 2))  layerInfo.layerStartX += layerInfo.layerSizeX;
            else if (tempX < layerInfo.layerStartX - (layerInfo.layerSizeX / 2)) layerInfo.layerStartX -= layerInfo.layerSizeX;
            // Adjust Start Y Position for infinite Scroll
            if (tempY > layerInfo.layerStartY + (layerInfo.layerSizeY / 2))  layerInfo.layerStartY += layerInfo.layerSizeY;
            else if (tempY < layerInfo.layerStartY - (layerInfo.layerSizeY / 2)) layerInfo.layerStartY -= layerInfo.layerSizeY;
        }
    }
}
