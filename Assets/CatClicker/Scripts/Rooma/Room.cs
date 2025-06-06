using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> Bowls;
    [SerializeField] private List<Cat> Cats;

    private GameState _gameState;
    private List<Cat> _spawnedCats;

    public void Initialize(GameState gameState)
    {
        _spawnedCats = new List<Cat>();
        _gameState = gameState;
        _gameState.OnBuyItem += DrawItems;
        DrawItems();

    }

    private void OnDestroy()
    {
        _gameState.OnBuyItem -= DrawItems;
    }

    private void DrawItems()
    {

        for (int i = 0; i < Bowls.Count; i++)
        {
            if (i < _gameState.CurrentBowl)
            {
                Bowls[i].SetActive(true);
            }
            else
            {
                Bowls[i].SetActive(false);
            }
        }

        for (int i = 0; i < Cats.Count; i++)
        {
            if (i < _gameState.CurrentCat)
            {
                Cats[i].gameObject.SetActive(true);
            }
            else
            {
                Cats[i].gameObject.SetActive(false);
            }
        }
    }

    private void SpawnCat(Cat cat)
    {
        var spawnPoint = Vector3.zero;
        var newCat = Instantiate(cat, spawnPoint, Quaternion.identity);
        _spawnedCats.Add(newCat);
    }
}
