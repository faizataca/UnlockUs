using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int coins = 0;
    private int customTime = 0;

    [SerializeField] public TextMeshProUGUI coinsCollected;
    [SerializeField] public TextMeshProUGUI coinsCollectedLevelComplete;
    [SerializeField] public TextMeshProUGUI scoreLevelComplete;

    [Header("Stars")]
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private Sprite starFilled;
    [SerializeField] private Sprite starEmpty;
    private int starsValue;

    [SerializeField] public Timer timer;
    private int score = 0;

    private bool hasKey = false;

    public void CollectKey()
    {
        hasKey = true;
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void LevelComplete()
    {
        CalculateFinalScore();
        CalculateStars();
        coinsCollectedLevelComplete.text = coins + "/16";
        scoreLevelComplete.text = score.ToString();

        PlayerData.Instance.Levels[PlayerData.Instance.CurrentLevel].AddNewScore(score, timer.GetTime(), coins, starsValue);
        PlayerData.Instance.UnlockNextLevel();
        PlayFabManager.Instance.SavePlayer();
    }

    public void UpdateScore()
    {
        coins += 1;
        coinsCollected.text = "Coins: " + coins;
    }

    private void CalculateStars()
    {
        // Tentukan jumlah bintang berdasarkan score
        if (score < 10000)
        {
            starsValue = 0;
        }
        else if (score <= 60000)
        {
            starsValue = 1;
        }
        else if (score <= 140000)
        {
            starsValue = 2;
        }
        else
        {
            starsValue = 3;
        }

        // Set tampilan sprite bintang
        star1.sprite = (starsValue >= 1) ? starFilled : starEmpty;
        star2.sprite = (starsValue >= 2) ? starFilled : starEmpty;
        star3.sprite = (starsValue >= 3) ? starFilled : starEmpty;
    }

    public void CalculateFinalScore()
    {
        score = coins * 10000 + (5000 - GetTime());
    }

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private int GetTime()
    {
        return (timer != null) ? timer.GetTime() : customTime;
    }

    public void SetCustomTime(int time)
    {
        customTime = time;
    }
}