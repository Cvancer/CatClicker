
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopButton> _buttons;
    [SerializeField] private List<ShopItem> _items;

    private GameState _gameState;

    public void Initialize(GameState gameState)
    {
        _gameState = gameState;
        _gameState.OnBuyItem += DrawButtons;
        _gameState.FishStorage.OnFishCountChanged += HandleFishCountChanged;
        DrawButtons();
    }

    private void OnDestroy()
    {
        _gameState.OnBuyItem -= DrawButtons;
        _gameState.FishStorage.OnFishCountChanged -= HandleFishCountChanged;


    }
    private void DrawButtons()
    {
        var cat = GetCurrentCat();
        var bowl = GetCurrentBowl();
        _buttons[0].Initialize(_gameState, cat);
        _buttons[1].Initialize(_gameState, bowl);

        gameObject.SetActive(!(_gameState.CurrentCat + _gameState.CurrentBowl >= _items.Count));

    }
    private ShopItem GetCurrentBowl()
    {
        return _items.Find(item => item.Type == ShopItemType.BOWL && item.ID == _gameState.CurrentBowl + 1);
    }

    private void HandleFishCountChanged(long count)
    {
        DrawButtons();
    }
    private ShopItem GetCurrentCat()
    {
        return _items.Find(item => item.Type == ShopItemType.CAT && item.ID == _gameState.CurrentCat + 1);

    }

}
