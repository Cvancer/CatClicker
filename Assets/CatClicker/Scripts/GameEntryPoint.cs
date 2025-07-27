using System.Collections;
using UnityEngine;


public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private UIRoot UIRootPrefab;
    [SerializeField] private Sounds _sounds;


    private Room _room;
    private UIRoot _uiRoot;
    private GameState _gameState;

    

    private void OnApplicationQuit()
    {
        _gameState.SaveGame();
    }

    private void RespawnRoom()
    {
        if (_room != null)
        {
            Destroy(_room.gameObject);

        }
        var roomPrefab = Resources.Load<Room>($"Prefabs/Rooms/Room{_gameState.CurrentRoom}");
        if (roomPrefab != null)
        {
            _room = Instantiate(roomPrefab, new Vector3(0, -0.34f, 0), Quaternion.identity);
            _room.gameObject.name = _room.gameObject.name.Replace("(Clone)", "");
            _room.Initialize(_gameState);
        }
    }


    private IEnumerator ChangeRoomCutScene()
    {
        _uiRoot.DeactivateAll();
        //_sounds.StopSound(Sounds.MUSIC);
        yield return new WaitForSeconds(1.5f);
        _sounds.PlaySound(Sounds.LIGHTON);
        _uiRoot.LightOff();
        yield return new WaitForSeconds(1.5f);
        _sounds.PlaySound(Sounds.REPAIR);
        RespawnRoom();
        yield return new WaitForSeconds(_sounds.GetSoundDuration(Sounds.REPAIR)+ 1f);
        _sounds.PlaySound(Sounds.LIGHTON);
        _uiRoot.LightOn();
        yield return new WaitForSeconds(1.5f);
        //_sounds.PlaySound(Sounds.MUSIC);
        _uiRoot.ActivateAll();

    }
   





    private void StartGame()
    {
        RespawnRoom();
        _uiRoot = Instantiate(UIRootPrefab);
        _uiRoot.Initialize(_gameState);
        StartCoroutine(AddFishPerSecond());
        _sounds.Initialize(_gameState);
        _sounds.PlaySound(Sounds.MUSIC);
        

    }

   private void Reset()
    {
        StopAllCoroutines();
        Destroy(_uiRoot.gameObject);
        StartGame();

    }

    private void OnDestroy()
    {
        _gameState.OnBuyRoom -= RunCutScene;
        _gameState.OnGameStateChanged -= Reset;
    }

    private void RunCutScene()
    {
        StartCoroutine(ChangeRoomCutScene());

    }

    private void Awake()
    {
        _gameState = new GameState();
        _gameState.LoadGame(new GameState());
        _gameState.OnBuyRoom += RunCutScene;
        _gameState.OnGameStateChanged += Reset;
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
