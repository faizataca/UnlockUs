using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Cari ScoreManager (bisa pilih FindFirstObjectByType atau FindAnyObjectByType)
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.CollectKey();
            }

            // Mainkan suara
            AudioManager.Instance.PlaySFX("Collected Key");

            // Hapus kunci dari scene
            Destroy(gameObject);
        }
    }
}
