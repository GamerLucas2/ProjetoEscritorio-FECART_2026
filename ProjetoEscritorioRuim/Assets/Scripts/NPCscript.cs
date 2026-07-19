using UnityEngine;

public class NPCscript : MonoBehaviour
{
    public int TaskID = 0;
    public bool hasTask;
    public bool taskCompleted;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTask && taskCompleted)
            hasTask = false;
    }
}
