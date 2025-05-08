using System.Collections;

using UnityEngine;


public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private UIRoot UIRootPrefab;
    [SerializeField] private Room _room;
    [SerializeField] private AudioSource _music; 

    private UIRoot _uiRoot;
    private GameState _gameState;

    private void SaveGame()
    {
        var json = JsonUtility.ToJson(_gameState);
        PlayerPrefs.SetString("Game", json);
        Debug.Log("GameSaved");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private GameState LoadGame(GameState defaultData)
    {
        if (!PlayerPrefs.HasKey("Game"))
        {
            return defaultData;
        }
        var json = PlayerPrefs.GetString("Game");
        return JsonUtility.FromJson<GameState>(json);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _gameState = LoadGame(new GameState());
            Reset();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gameState = new GameState();
            Reset();
        }
    }


    private void SetVolume()
    {
        _music.volume = _gameState.Volume;
    }


    private void StartGame()
    {

        _room.Initialize(_gameState);
        _uiRoot = Instantiate(UIRootPrefab);
        _uiRoot.Initialize(_gameState);
        StartCoroutine(AddFishPerSecond());

        _gameState.OnVolumeChange += SetVolume;
        SetVolume();
        _music.Play();

    }

    private void Reset()
    {
        StopAllCoroutines();
        Destroy(_uiRoot.gameObject);
        _gameState.OnVolumeChange -= SetVolume;
        StartGame();

    }



    private void Awake()
    {
        _gameState = LoadGame(new GameState());
        StartGame();
    }

    private IEnumerator AddFishPerSecond()
    {
        while (true)
        {
            _gameState.AddFishPerSecond();
            yield return new WaitForSeconds(1f);
        }
    }

}
