using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float loadDelay = 2f;
    [SerializeField] private ParticleSystem finishEffect;
    
    private string PLAYER_TAG = "Player";
    private AudioSource audioSource;
    
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == PLAYER_TAG)
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", loadDelay);
            
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
