using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _price;

    

    private ShopItem _shopItem;
    private GameState _gameState;
    private UIRoot _uIRoot;

    public void Initialize (GameState gameState, ShopItem shopItem, UIRoot uIRoot)
    {
        _uIRoot = uIRoot;
        if (shopItem == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        var button = GetComponent<Button>();
        button.interactable = gameState.FishStorage.FishCount >= shopItem.Price;
        _price.text = $"Price: {shopItem.Price}";
        _shopItem = shopItem;
        _gameState = gameState;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => _gameState.FishStorage.TrySpendFish(_shopItem.Price, SuccessfulBuy));
    }

    private ItemNotification GetPrefab()
    {
        var prefabName = _shopItem.Type == ShopItemType.BOWL ? "Bowl" : _shopItem.name;
        return Resources.Load<ItemNotification>($"Prefabs/Notifications/{prefabName}");
    }

    private void SuccessfulBuy()
    {
        var panel = Instantiate(GetPrefab(),_uIRoot.transform);
        panel.transform.position += Vector3.down * 50;
        panel.Initialize(_shopItem);
        switch (_shopItem.Type)
        {
            case ShopItemType.CAT:
                _gameState.IncreaseFishPerSecond(_shopItem.Amount);
                _gameState.IncreaseCurrentCat();
                break;
            case ShopItemType.BOWL:
                _gameState.IncreaseFishPerClick(_shopItem.Amount);
                _gameState.IncreaseCurrentBowl();
                break;
            default:
                throw new ArgumentException("Unexpected type");

        }

    }
    
}
