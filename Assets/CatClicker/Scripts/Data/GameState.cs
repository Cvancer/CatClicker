using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState 
{
    public event Action OnBuyItem;
    public event Action OnVolumeChange;
    public int FishPerClick => _fishPerClick;
    public int FishPerSecond => _fishPerSecond;
    public int CurrentBowl => _currentBowl;
    public int CurrentCat => _currentCat;
    public float Volume => _volume;
    public FishStorage FishStorage => _fishStorage;
    [SerializeField] private int _fishPerClick;
    [SerializeField] private int _fishPerSecond;
    [SerializeField] private FishStorage _fishStorage;
    [SerializeField] private int _currentBowl;
    [SerializeField] private int _currentCat;
    [SerializeField] private float _volume;
    public GameState()
    {
        _currentBowl = 0;
        _currentCat = 0;
        _fishPerClick = 1;
        _fishPerSecond = 0;
        _volume = 0.2f;

        _fishStorage = new FishStorage();
    }

    

    public GameState(int fishPerClick, int fishPerSecond, FishStorage fishStorage, int currentBowl, int currentCat, float volume)
    {
        _currentBowl = currentBowl;
        _currentCat = currentCat;
        _fishPerClick = fishPerClick;
        _fishPerSecond = fishPerSecond;
        _fishStorage = fishStorage;
        _volume = volume;
    }

    public GameState(GameState gameState)
    {
        _currentBowl = gameState.CurrentBowl;
        _currentCat = gameState.CurrentCat;
        _fishPerClick = gameState.FishPerClick;
        _fishPerSecond = gameState.FishPerSecond;
        _fishStorage = gameState.FishStorage;
        _volume = gameState.Volume;
    }

    public void SetVolume(float volume)
    {
        _volume = volume;
        OnVolumeChange?.Invoke();
    }



    public void AddFishPerClick()
    {
        _fishStorage.AddFish(_fishPerClick);
    }

    public void AddFishPerSecond()
    {
        _fishStorage.AddFish(_fishPerSecond);
    }

    public void IncreaseFishPerClick(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Value can not be negative");
        }
        _fishPerClick += value;
        
    }

    public void IncreaseFishPerSecond(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Value can not be negative");
        }
        _fishPerSecond += value;
    }
    
    public void IncreaseCurrentBowl()
    {
        _currentBowl++;
        OnBuyItem?.Invoke();
    }

    public void IncreaseCurrentCat()
    {
        _currentCat++;
        OnBuyItem?.Invoke();
    }

}
