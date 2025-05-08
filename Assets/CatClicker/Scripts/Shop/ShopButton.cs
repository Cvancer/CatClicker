using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _name;

    private ShopItem _shopItem;
    private GameState _gameState;

    public void Initialize (GameState gameState, ShopItem shopItem)
    {
        if (shopItem == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        var button = GetComponent<Button>();
        button.interactable = gameState.FishStorage.FishCount >= shopItem.Price;
        _icon.sprite = shopItem.Image;
        _price.text = $"Price: {shopItem.Price}";
        _name.text = shopItem.Name;
        _shopItem = shopItem;
        _gameState = gameState;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => _gameState.FishStorage.TrySpendFish(_shopItem.Price, SuccessfulBuy));
        

    }


    private void SuccessfulBuy()
    {
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
