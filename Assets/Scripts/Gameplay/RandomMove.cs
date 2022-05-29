using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class RandomMove : MonoBehaviour
    {
        [SerializeField] private float minVelocity;
        [SerializeField] private float maxVelocity;
        
        private Rigidbody2D myRigidbody2D = default;
        
        private void Awake()
        {
            myRigidbody2D = GetComponent<Rigidbody2D>();

            if (myRigidbody2D == null)
            {
                Debug.Log("No Ridgitbody2D found. Not Moving Object");
                Destroy(this);
            }


            var movementVector = Vector2.up * Random.Range(minVelocity, maxVelocity);

            var direction = Quaternion.Euler(0, 0, Random.Range(0f, 359.99f)) * movementVector;
            myRigidbody2D.velocity = direction;
            
        }
    }
