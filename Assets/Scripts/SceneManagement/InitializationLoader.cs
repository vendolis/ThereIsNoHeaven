using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for starting the game by loading the persistent managers scene 
/// and raising the event to load the Main Menu
/// </summary>

public class InitializationLoader : MonoBehaviour
{
	[SerializeField] private GameSceneSO _managersScene = default;
	//[SerializeField] private GameSceneSO _menuToLoad = default;
	[SerializeField] private GameSceneSO _locationToLoad = default;

	[Header("Broadcasting on")]
	//[SerializeField] private LoadEventChannelSO _menuLoadChannel = default;
	[SerializeField] private LoadEventChannelSO _locationLoadChannel = default;
	
	private void Start()
	{
		//Load the persistent managers scene
		 //SceneManager.LoadSceneAsync(_managersScene.name, LoadSceneMode.Additive).completed += LoadMainMenu;
		 SceneManager.LoadSceneAsync(_managersScene.getSceneReference, LoadSceneMode.Additive).completed += LoadFirstLocation;
	}

	
/*	private void LoadMainMenu(AsyncOperation obj)
	{
		_menuLoadChannel.RaiseEvent(_menuToLoad, true);

		SceneManager.UnloadSceneAsync((int) SceneIndexes.INITIALIZATION); 
	}
*/	
	private void LoadFirstLocation(AsyncOperation obj)
	{
		_locationLoadChannel.RaiseEvent(_locationToLoad, true);

		SceneManager.UnloadSceneAsync((int) SceneIndexes.INITIALIZATION); 
	}
}
