using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameStateSO _gameState = default;
	
	[Header("Listening on")]
	[SerializeField] private LongEventChannelSO incomeEvents = default;
	[Header("Broadcasting on")]
	[SerializeField] private LongEventChannelSO debtUpdateEvent = default;

	[Header("Money Things")]
	[SerializeField] private long startingDebt = -99999999999;
	
	private long _currentDebt = 0;
	
	private long CurrentDebt  
	{
		get
		{
			return _currentDebt;
		}
		set
		{
			_currentDebt = value;
			debtUpdateEvent.RaiseEvent(_currentDebt);
		} 
	}
	
	
	//[Header("Inventory")]
	//[SerializeField] private InventorySO _inventory = default;

	

	private void Start()
	{
		incomeEvents.OnEventRaised += AddIncome;
		StartGame();
	}

	private void OnEnable()
	{

	}

	private void OnDestroy()
	{
		incomeEvents.OnEventRaised -= AddIncome;
	}

	void AddIncome(long amount)
	{
		CurrentDebt += amount;
	}

	void StartGame()
	{
		CurrentDebt = startingDebt;
		_gameState.UpdateGameState(GameState.Gameplay);
	}
}
