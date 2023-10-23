using UnityEngine;

public class CollectorHolder : MonoBehaviour
{

    private string ENEMY_TAG = "Enemy";
    private string PLAYER_TAG = "Player";
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY_TAG) || collision.CompareTag(PLAYER_TAG))
        {
            Destroy(collision.gameObject);
        }
    }
    
    
    
    
} // class