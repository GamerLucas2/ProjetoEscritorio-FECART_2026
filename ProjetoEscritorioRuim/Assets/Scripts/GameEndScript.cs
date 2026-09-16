using UnityEngine;

public class GameEndScript : MonoBehaviour
{
    public static GameEndScript Instance  { get; private set; }
    
    public DialogueAsset[] motivationalText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
