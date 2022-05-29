using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameStateSO _gameState = default;

	//[Header("Inventory")]
	//[SerializeField] private InventorySO _inventory = default;

	//[Header("Broadcasting on")]

	private void Start()
	{
		StartGame();
	}

	private void OnEnable()
	{

	}

	private void OnDisable()
	{
	}

	void AddRockCandyRecipe()
	{
	}

	void AddSweetDoughRecipe()
	{
	}

	void AddFinalRecipes()
	{
	}

	void StartGame()
	{
		_gameState.UpdateGameState(GameState.Gameplay);
	}
}
