using UnityEngine;


public class randomObjectRotator : objectRotator
{
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;

    private void Awake()
    {
        SetRotationSpeed(Random.Range(minRotationSpeed, maxRotationSpeed));
        
        System.Random r = new System.Random();
        SetRotationDirection(r.Next(0, 2) == 1? true : false);
        
    }
}