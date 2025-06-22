using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            FindObjectOfType<ScoreManager>().CollectKey();
            Destroy(gameObject); // Hapus kunci dari scene
        }
    }
}
