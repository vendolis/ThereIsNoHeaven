﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the scene loading and unloading.
/// </summary>
public class SceneLoader : MonoBehaviour
{
	[SerializeField] private GameSceneSO _gameplayScene = default;
	[SerializeField] private InputReader _inputReader = default;

	[Header("Listening to")]
	[SerializeField] private LoadEventChannelSO _loadLocation = default;
	[SerializeField] private LoadEventChannelSO _loadMenu = default;
	[SerializeField] private LoadEventChannelSO _coldStartupLocation = default;

	[Header("Broadcasting on")]
	[SerializeField] private BoolEventChannelSO _toggleLoadingScreen = default;
	[SerializeField] private VoidEventChannelSO _onSceneReady = default; //picked up by the SpawnSystem
	[SerializeField] private FadeChannelSO _fadeRequestChannel = default;

	private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
	
	private AsyncOperation _loadingOperationHandle;
	private AsyncOperation _gameplayManagerLoadingOpHandle;

	//Parameters coming from scene loading requests
	private GameSceneSO _sceneToLoad;
	private GameSceneSO _currentlyLoadedScene;
	private bool _showLoadingScreen;

	private float _fadeDuration = .5f;
	private bool _isLoading = false; //To prevent a new loading request while already loading a new scene

	private void OnEnable()
	{
		_loadLocation.OnLoadingRequested += LoadLocation;
		_loadMenu.OnLoadingRequested += LoadMenu;
#if UNITY_EDITOR
		_coldStartupLocation.OnLoadingRequested += LocationColdStartup;
#endif
	}

	private void OnDisable()
	{
		_loadLocation.OnLoadingRequested -= LoadLocation;
		_loadMenu.OnLoadingRequested -= LoadMenu;
#if UNITY_EDITOR
		_coldStartupLocation.OnLoadingRequested -= LocationColdStartup;
#endif
	}

#if UNITY_EDITOR
	/// <summary>
	/// This special loading function is only used in the editor, when the developer presses Play in a Location scene, without passing by Initialisation.
	/// </summary>
	private void LocationColdStartup(GameSceneSO currentlyOpenedLocation, bool showLoadingScreen, bool fadeScreen)
	{
		_currentlyLoadedScene = currentlyOpenedLocation;

		if (_currentlyLoadedScene.sceneType == GameSceneSO.GameSceneType.Location)
		{
			//Gameplay managers is loaded synchronously
			SceneManager.LoadScene(_gameplayScene.getSceneReference, LoadSceneMode.Additive);
			
			StartGameplay();
		}
	}
#endif

	/// <summary>
	/// This function loads the location scenes passed as array parameter
	/// </summary>
	private void LoadLocation(GameSceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen)
	{
		//Prevent a double-loading, for situations where the player falls in two Exit colliders in one frame
		if (_isLoading)
			return;

		_sceneToLoad = locationToLoad;
		_showLoadingScreen = showLoadingScreen;
		_isLoading = true;
		
		//In case we are coming from the main menu, we need to load the Gameplay manager scene first
		if (!SceneManager.GetSceneByPath(_gameplayScene.getSceneReference).isLoaded)
		{
			_gameplayManagerLoadingOpHandle = SceneManager.LoadSceneAsync(_gameplayScene.getSceneReference, LoadSceneMode.Additive);
			_gameplayManagerLoadingOpHandle.completed += OnGameplayManagersLoaded;
		}
		else
		{
			StartCoroutine(UnloadPreviousScene());
		}
	}

	private void OnGameplayManagersLoaded(AsyncOperation obj)
	{
		StartCoroutine(UnloadPreviousScene());
	}

	/// <summary>
	/// Prepares to load the main menu scene, first removing the Gameplay scene in case the game is coming back from gameplay to menus.
	/// </summary>
	private void LoadMenu(GameSceneSO menuToLoad, bool showLoadingScreen, bool fadeScreen)
	{
		//Prevent a double-loading, for situations where the player falls in two Exit colliders in one frame
		if (_isLoading)
			return;

		_sceneToLoad = menuToLoad;
		_showLoadingScreen = showLoadingScreen;
		_isLoading = true;

		//In case we are coming from a Location back to the main menu, we need to get rid of the persistent Gameplay manager scene
		if (SceneManager.GetSceneByPath(_gameplayScene.getSceneReference).isLoaded)
			SceneManager.UnloadSceneAsync(_gameplayScene.getSceneReference);

		StartCoroutine(UnloadPreviousScene());
	}

	/// <summary>
	/// In both Location and Menu loading, this function takes care of removing previously loaded scenes.
	/// </summary>
	private IEnumerator UnloadPreviousScene()
	{
		_inputReader.DisableAllInput();
		_fadeRequestChannel.FadeOut(_fadeDuration);

		yield return new WaitForSeconds(_fadeDuration);

		if (_currentlyLoadedScene != null) //would be null if the game was started in Initialisation
		{
			if (SceneManager.GetSceneByPath(_currentlyLoadedScene.getSceneReference).IsValid())
			{
				//Unload the scene through its AssetReference, i.e. through the Addressable system
				SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath(_currentlyLoadedScene.getSceneReference));
			}
		}

		LoadNewScene();
	}

	/// <summary>
	/// Kicks off the asynchronous loading of a scene, either menu or Location.
	/// </summary>
	private void LoadNewScene()
	{
		if (_showLoadingScreen)
		{
			_toggleLoadingScreen.RaiseEvent(true);
		}

		_loadingOperationHandle = SceneManager.LoadSceneAsync(_sceneToLoad.getSceneReference,LoadSceneMode.Additive);
		_loadingOperationHandle.completed += OnNewSceneLoaded;
	}

	private void OnNewSceneLoaded(AsyncOperation obj)
	{
		//Save loaded scenes (to be unloaded at next load request)
		_currentlyLoadedScene = _sceneToLoad;

		Scene s = SceneManager.GetSceneByPath(_sceneToLoad.getSceneReference);
		SceneManager.SetActiveScene(s);

		_isLoading = false;

		if (_showLoadingScreen)
			_toggleLoadingScreen.RaiseEvent(false);

		_fadeRequestChannel.FadeIn(_fadeDuration);

		StartGameplay();
	}

	private void StartGameplay()
	{
		_onSceneReady.RaiseEvent();
	}

	private void ExitGame()
	{
		Application.Quit();
		Debug.Log("Exit!");
	}
}
