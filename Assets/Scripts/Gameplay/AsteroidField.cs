using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidField: MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnedAsteroids;
    
    [SerializeField] [ReadOnly] private FieldBounds fieldBounds = new FieldBounds();

    [SerializeField] [ReadOnly] private int asteroidsInField;
    [SerializeField] [ReadOnly] private int asteroidsDestroyed = 0;

    [SerializeField] [ReadOnly] private List<Vector2> suggestedPoints;

    private GameObject usedPrefab;
    private float usedMiniumDistance;

    public void MoveField(FieldBounds newBounds)
    {
        MoveField(newBounds, asteroidsInField);
    }
    
    public void MoveField(FieldBounds newBounds, int asteroidCount)
    {
        asteroidsInField = asteroidCount;
        
        GenerateSuggestedPoints();

        int spawnedNumber = spawnedAsteroids.Count;

        if (spawnedNumber > asteroidsInField)
        {
            for (int i = 0; i < spawnedNumber - asteroidsInField; i++)
            {
                Destroy(spawnedAsteroids[i]);
                spawnedAsteroids.Remove(spawnedAsteroids[i]);
            }
        }

        for (int i = 0; i < asteroidsInField && i < spawnedAsteroids.Count; i++)
        {
            spawnedAsteroids[i].transform.position = suggestedPoints[i];
        }

        if (spawnedAsteroids.Count < asteroidsInField)
        {
            for (int i = spawnedAsteroids.Count; i < asteroidsInField; i++)
            {
                SpawnAsteroid(suggestedPoints[i],usedPrefab);
            }
        }

    }

    public void GenerateField(GameObject entityPrefab, FieldBounds newBounds, int numAsteroids, float minimumDistance)
    {
        if (spawnedAsteroids.Count > 0)
        {
            PurgeField();
        }
        
        asteroidsInField = numAsteroids;
        asteroidsDestroyed = 0;
        fieldBounds = newBounds;
        
        usedPrefab = entityPrefab;
        usedMiniumDistance = minimumDistance;
        
        GenerateSuggestedPoints();

        foreach (var point in suggestedPoints)
        {
            SpawnAsteroid(point, entityPrefab);
        }
       
    }
    

    private void GenerateSuggestedPoints()
    {
        System.Random rand = new System.Random(DateTime.Now.ToString().GetHashCode());
        
        List<Vector2> allSuggestedPoints = UniformPoissonDiskSampler.SampleRectangle(
            fieldBounds.TopLeft,
            fieldBounds.BottomRight,
            usedMiniumDistance);
        
        for (int i = 0; i < asteroidsInField; i++)
        {
            int index = rand.Next(0, allSuggestedPoints.Count);
            suggestedPoints.Add(allSuggestedPoints[index]);
            allSuggestedPoints.RemoveAt(index);
        }
        
    }

    private void OnDestroy()
    {
        foreach (var asteroid in spawnedAsteroids)
        {
            asteroid.GetComponent<IDestroyable>().OnDestoryed -= RemoveAsteroidFromField;
        }
    }

    private void SpawnAsteroid(Vector2 point, GameObject entityPrefab)
    {
        GameObject asteroid = Instantiate(entityPrefab, point, Quaternion.Euler(0,0,Random.Range(0f,359.9f)), gameObject.transform);
        asteroid.GetComponent<IDestroyable>().OnDestoryed += RemoveAsteroidFromField;
    }

    private void PurgeField()
    {
        foreach (var asteroid in spawnedAsteroids)
        {
            spawnedAsteroids.Remove(asteroid);
            Destroy(asteroid);
        }
        asteroidsInField = 0;
        asteroidsDestroyed = 0;
    }

    public void RemoveAsteroidFromField(GameObject removedAsteroid)
    {
        spawnedAsteroids.Remove(removedAsteroid);
        asteroidsDestroyed++;
    }
    
}