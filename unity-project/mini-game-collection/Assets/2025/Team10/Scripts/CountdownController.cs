using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownController : MonoBehaviour
{
    public Image countdownImage;      // UI Image that displays the numbers
    public Sprite[] numberSprites;    // 3, 2, 1 (make sure in order!)
    public float interval = 1f;       // Time between each number

    public bool countdownFinished { get; private set; } = false;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownFinished = false;

        for (int i = 0; i < numberSprites.Length; i++)
        {
            countdownImage.sprite = numberSprites[i];
            countdownImage.enabled = true;
            yield return new WaitForSeconds(interval);
        }

        countdownImage.enabled = false;
        countdownFinished = true;
    }
}
