using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] private ParticleSystem BoardEffect;

    private string GROUND_TAG = "Ground";
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            BoardEffect.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            BoardEffect.Stop();
        }
    }
}
