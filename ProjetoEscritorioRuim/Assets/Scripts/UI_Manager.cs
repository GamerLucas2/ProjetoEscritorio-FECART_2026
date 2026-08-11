using System;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject endScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameHUD.SetActive(true);
        endScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLevelScreen()
    {
        endScreen.SetActive(true);
        
    }
}
