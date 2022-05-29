using System;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;


public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab = default;
    [SerializeField] private AsteroidField asteroidFieldPrefab = default;
    [SerializeField] private CinemachineVirtualCameraBase followCamera;

    [SerializeField] private List<AsteroidField> spawnedAsteroidFields;

    [SerializeField] private float fieldSize = 65f;
    [SerializeField] private float minDistanceBetweenAsteroids = 8f;

    [SerializeField] private bool generateNineField = true;

    [SerializeField] private bool followField = true;
    [SerializeField] private float checkInterval = 2f;

    [SerializeField] private FieldBounds asteroidFieldBounds;

    private Vector2 centerPoint;
    
    private void Start()
    {
        GetCameraFieldBounds();

        AsteroidField newField = Instantiate<AsteroidField>(asteroidFieldPrefab, 
            centerPoint, 
            Quaternion.identity, 
            gameObject.transform);
        
        newField.GenerateField(asteroidPrefab, asteroidFieldBounds, 20, minDistanceBetweenAsteroids);
        
        spawnedAsteroidFields.Add(newField);
        
    }

    private void GetCameraFieldBounds()
    {

        float halveDistance = fieldSize / 2;
        asteroidFieldBounds = new FieldBounds(new Vector2(-1f*halveDistance, halveDistance),new Vector2( halveDistance,-1f * halveDistance));

    }
}