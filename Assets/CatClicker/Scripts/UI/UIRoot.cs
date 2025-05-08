using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private ClickButton _clickButton;
    [SerializeField] private FishText _fishText;
    [SerializeField] private Shop _shop;
    [SerializeField] private Slider _slider;
    private GameState _gameState;
    public void Initialize(GameState gameState)
    {
        _gameState = gameState;
        _shop.Initialize(gameState);
        _clickButton.Initialize(gameState);
        _fishText.Initialize(gameState.FishStorage);
        _slider.SetValueWithoutNotify(_gameState.Volume);
    }

    public void SetVolume(float volume)
    {
        _gameState.SetVolume(volume);
    }
}
