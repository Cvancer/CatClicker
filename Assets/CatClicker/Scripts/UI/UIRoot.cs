using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private ClickButton _clickButton;
    [SerializeField] private FishText _fishText;
    [SerializeField] private Shop _shop;
    [SerializeField] private Menu _menu;
    [SerializeField] private GameObject _lightOff;
    public void Initialize(GameState gameState)
    {
        _shop.Initialize(gameState, this);
        _clickButton.Initialize(gameState);
        _fishText.Initialize(gameState.FishStorage);
        _menu.Initialize(gameState);
        
    }
    public void DeactivateAll()
    {
        _shop.SetActive(false);
        _fishText.gameObject.SetActive(false);
        _menu.gameObject.SetActive(false);
        _clickButton.gameObject.SetActive(false);

    }
    public void ActivateAll()
    {
        _shop.SetActive(true);
        _fishText.gameObject.SetActive(true);
        _menu.gameObject.SetActive(true);
        _clickButton.gameObject.SetActive(true);

    }

    public void LightOff()
    {
        _lightOff.gameObject.SetActive(true);
    }

    public void LightOn()
    {
        _lightOff.gameObject.SetActive(false);
    }


}
