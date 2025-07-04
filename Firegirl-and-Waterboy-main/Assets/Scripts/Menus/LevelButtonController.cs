using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonController : MonoBehaviour
{
    public Button button;
    public Image lockIcon;

    [Header("Setup Data")]
    public string levelSceneName;       // misal: "Level1"
    public int requiredUnlockLevel;     // misal: 1 untuk Level1, 2 untuk Level2, dll

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        bool isUnlocked = unlockedLevel >= requiredUnlockLevel;

        button.interactable = isUnlocked;
        lockIcon.gameObject.SetActive(!isUnlocked);

        if (isUnlocked)
        {
            button.onClick.AddListener(() => LoadLevel());
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(levelSceneName);
    }
}