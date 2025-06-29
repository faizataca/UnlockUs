using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 0.1f;

    private Image imageComponent;
    private int currentFrame;
    private float timer;

    void Start()
    {
        imageComponent = GetComponent<Image>();
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            imageComponent.sprite = frames[currentFrame];
            timer = 0f;
        }
    }
}
