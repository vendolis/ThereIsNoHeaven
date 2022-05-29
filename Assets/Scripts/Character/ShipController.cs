using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    [Header("Event Connection")]
    [SerializeField] private InputReader inputReader;

    [Header("Turn Parameters")]
    [SerializeField] private float turnSpeedup = 200.0f;
    [SerializeField] private float turnSlowdown = 200.0f;
    [SerializeField] private float maxTurnSpeed = 120.0f;

    [Header("Movement Parameters")] 
    [SerializeField] private float movementSpeedup = 9.0f;
    [SerializeField] private float movementSlowdown = 10.0f;
    [SerializeField] private float maxMovementSpeed = 10.0f;

    [SerializeField] private float movementBWSpeedup = 2.0f;
    [SerializeField] private float maxMovementBWSpeed = 3.0f;

    public static event Action OnPlayerDie = delegate {  };
    
    private Vector2 currentInput;

    private float currentTurnSpeed = 0f;
    private float currentMoveSpeed = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        inputReader.MoveEvent += GetMovementInput;
    }

    // Update is called once per frame
    void Update()
    {
        ControllTurnspeed();
        ControllMoveSpeed();

        
        if(currentMoveSpeed != 0f || currentTurnSpeed != 0f)
            MoveShip();
    }

    private void MoveShip()
    {
        //Debug.Log("Current Speed: " + currentMoveSpeed + " -- Current TurnSpeed: " + currentTurnSpeed);
        transform.Translate(Vector2.up * (currentMoveSpeed * Time.deltaTime));
        transform.Rotate(Vector3.forward, currentTurnSpeed * Time.deltaTime);
    }

    void GetMovementInput(Vector2 moveVector)
    {
        
        currentInput = moveVector;
    }

    void ControllTurnspeed()
    {
        if(Mathf.Abs(currentInput.x) <= 0.05f)
        {
            if (Mathf.Abs(currentTurnSpeed) > 0.5f)
                currentTurnSpeed -= Mathf.Sign(currentTurnSpeed) * turnSlowdown * Time.deltaTime;
            else
                currentTurnSpeed = 0f;
            return;
        }
        
        float additionalBreak = (Mathf.Sign(currentTurnSpeed) != Mathf.Sign(currentInput.x)? 0f: 1f );
            
        currentTurnSpeed += currentInput.x * -1f * (turnSpeedup + turnSlowdown * additionalBreak) * Time.deltaTime;
        
        
        currentTurnSpeed = Mathf.Clamp(currentTurnSpeed, maxTurnSpeed * -1.0f, maxTurnSpeed);
    }

    void ControllMoveSpeed()
    {
        
        if(Mathf.Abs(currentInput.y) <= 0.05f)
        {
            if (Mathf.Abs(currentMoveSpeed) > 0.5f)
                currentMoveSpeed -= Mathf.Sign(currentMoveSpeed) * (movementSlowdown * Time.deltaTime);
            else
                currentMoveSpeed = 0f;

            return;
        }

        float additionalBreak = (Mathf.Sign(currentMoveSpeed) != Mathf.Sign(currentInput.y)? 1f: 0f );
        
        if (currentInput.y > 0f)
        {
            currentMoveSpeed += currentInput.y * (movementSpeedup + movementBWSpeedup * additionalBreak) * Time.deltaTime;
        }
        else
        {
            currentMoveSpeed += currentInput.y * (movementBWSpeedup + movementSpeedup * additionalBreak) * Time.deltaTime;
        }
  
        currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, maxMovementBWSpeed * -1.0f, maxMovementSpeed);
    }

}
