﻿using System;
using System.Linq;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
	[Header("Asset References")]
	[SerializeField] private InputReader _inputReader = default;
	[SerializeField] private ShipController _playerPrefab = default;
	//[SerializeField] private TransformAnchor _playerTransformAnchor = default;
	[SerializeField] private TransformEventChannelSO _playerInstantiatedChannel = default;
	//[SerializeField] private PathStorageSO _pathTaken = default;

	[Header("Scene Ready Event")]
	[SerializeField] private VoidEventChannelSO _onSceneReady = default; //Raised by SceneLoader when the scene is set to active
	
	// For later Multi Scene switches
	//private LocationEntrance[] _spawnLocations;
	private Transform _defaultSpawnPoint;

	private void Awake()
	{
		//_spawnLocations = GameObject.FindObjectsOfType<LocationEntrance>();
		_defaultSpawnPoint = transform.GetChild(0);
	}

	private void OnEnable()
	{
		_onSceneReady.OnEventRaised += SpawnPlayer;
	}

	private void OnDisable()
	{
		_onSceneReady.OnEventRaised -= SpawnPlayer;

		//_playerTransformAnchor.Unset();
	}

	private Transform GetSpawnLocation()
	{
		//if (_pathTaken == null)
			return _defaultSpawnPoint;
			
/*      //This is all for Multi point entry to scenes. Which is planed for later.
 
		//Look for the element in the available LocationEntries that matches tha last PathSO taken
		int entranceIndex = Array.FindIndex(_spawnLocations, element =>
			element.EntrancePath == _pathTaken.lastPathTaken );

		if (entranceIndex == -1)
		{
			Debug.LogWarning("The player tried to spawn in an LocationEntry that doesn't exist, returning the default one.");
			return _defaultSpawnPoint;
		}
		else
			return _spawnLocations[entranceIndex].transform;
*/
	}

	private void SpawnPlayer()
	{
		Transform spawnLocation = GetSpawnLocation();
		ShipController playerInstance = Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);

		_playerInstantiatedChannel.RaiseEvent(playerInstance.transform);
		
		//TODO: Probably move this to the GameManager once it's up and running
		_inputReader.EnableGameplayInput();
	}
}

