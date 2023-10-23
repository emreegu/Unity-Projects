using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float loadDelay = 2f;
    [SerializeField] ParticleSystem CrashEffect;
    [SerializeField] private AudioClip crashSFX;
    
    private string GROUND_TAG = "Ground";
    private bool hasCrashed = false;
    
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == GROUND_TAG && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerControler>().DisableControls();
            CrashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", loadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
