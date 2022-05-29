using UnityEngine;
using Random = System.Random;

public class MultiObjectSpawner : ObjectSpawner
{
    [SerializeField] int minNumber = 1;
    [SerializeField] private int maxNumber = 1;

    protected override void SpawnObject()
    {
        int spawncount = minNumber;

        if (minNumber != maxNumber)
        {
            var rand = new Random();

            spawncount = rand.Next(minNumber, maxNumber);
        }

        for (int i = 0; i < spawncount; i++)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
            
    }
}