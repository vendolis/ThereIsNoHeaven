using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;
//using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows a "cold start" in the editor, when pressing Play and not passing from the Initialisation scene.
/// </summary> 
public class EditorColdStartup : MonoBehaviour
{

	[SerializeField] private GameSceneSO _thisSceneSO = default;
	[SerializeField] private PersistentManagersSO _persistentManagersSO = default;
	[SerializeField] private LoadEventChannelSO _notifyColdStartupChannel = default;
	[SerializeField] private VoidEventChannelSO _onSceneReadyChannel = default;
//	[SerializeField] private PathStorageSO _pathStorage = default;
//	[SerializeField] private SaveSystem _saveSystem = default;

	private bool isColdStart = false;
	private void Awake()
	{
		if (!SceneManager.GetSceneByPath(_persistentManagersSO.getSceneReference).isLoaded)
		{
			isColdStart = true;

			//Reset the path taken, so the character will spawn in this location's default spawn point
//			_pathStorage.lastPathTaken = null;
		}
//		CreateSaveFileIfNotPresent();
	}

	private void Start()
	{
		if (isColdStart && !SceneManager.GetSceneByPath(_persistentManagersSO.getSceneReference).isLoaded)
		{
			SceneManager.LoadSceneAsync( _persistentManagersSO.getSceneReference ,LoadSceneMode.Additive).completed += OnNotifyChannelLoaded;
		}
		else if (isColdStart)
		{
			OnNotifyChannelLoaded(null);
		}
		
//		CreateSaveFileIfNotPresent();
	}
/*	private void CreateSaveFileIfNotPresent()
	{
		if (_saveSystem != null && !_saveSystem.LoadSaveDataFromDisk())
		{
			_saveSystem.SetNewGameData();
		}
	}
*/

private void OnNotifyChannelLoaded(AsyncOperation obj)
{
	if (_thisSceneSO != null)
	{
		_notifyColdStartupChannel.RaiseEvent(_thisSceneSO);
	}
	else
	{
		//Raise a fake scene ready event, so the player is spawned
		_onSceneReadyChannel.RaiseEvent();
		//When this happens, the player won't be able to move between scenes because the SceneLoader has no conception of which scene we are in
	}
}



}
