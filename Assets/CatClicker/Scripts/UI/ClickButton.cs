using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Button))]
public class ClickButton : MonoBehaviour
{

    private ParticleSystem _particles;

     public void Initialize(GameState gameState)
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => gameState.AddFishPerClick());
        _particles = FindFirstObjectByType<ParticleSystem>();
        button.onClick.AddListener(() => PlayParicles());
        var clickSound = GetComponentInChildren<AudioSource>();
        button.onClick.AddListener(() => clickSound.Play());
    }

    private void PlayParicles()
    {
        
        _particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        _particles.Play();
    }

}
