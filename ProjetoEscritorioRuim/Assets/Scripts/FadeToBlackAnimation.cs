using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlackAnimation : MonoBehaviour
{
    [SerializeField] private Image blackBox;
    [SerializeField] Color targetColor = Color.black; 
    [SerializeField] private bool isIntermission;
    private bool movingToNextLevel;
    
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = gameObject.GetComponent<LevelManager>();
    }

    private void OnEnable()
    {
        if (isIntermission)
        {
            DialogueController.OnDialogueEnded += LevelTransition;
        }
    }

    private void OnDisable()
    {
        DialogueController.OnDialogueEnded -= LevelTransition;
    }

    private void Start()
    {
        blackBox.gameObject.SetActive(true);
        movingToNextLevel = false;
        blackBox.color = Color.black;
        StartCoroutine(FadeFromBlack(Color.clear, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (blackBox.color == targetColor && movingToNextLevel)
        {
            levelManager.MoveToNextLevel(PlayerPrefs.GetInt("LevelID") + 1);
        }
    }
    
    public void LevelTransition()
    {
        blackBox.gameObject.SetActive(true);
        print("moving to next level");
        movingToNextLevel = true;
        StartCoroutine(FadeToBlack(Color.black, 1f));
    }
    
    IEnumerator FadeToBlack(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = blackBox.color;

        while (time < duration)
        {
            blackBox.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        blackBox.color = endValue;
    }

    IEnumerator FadeFromBlack(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = blackBox.color;

        while (time < duration)
        {
            blackBox.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        blackBox.color = endValue;
        blackBox.gameObject.SetActive(false);
        StopCoroutine(FadeFromBlack(endValue, duration));
    }
}
