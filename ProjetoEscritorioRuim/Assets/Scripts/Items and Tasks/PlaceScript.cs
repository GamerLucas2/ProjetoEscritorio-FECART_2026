using UnityEngine;

public class PlaceScript : MonoBehaviour
{
    public string TaskID;

    public bool hasTask;

    public bool hasItemOnTop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindChildObjectWithTag();
    }

    private void FindChildObjectWithTag()
    {
        Transform parent = transform;
        foreach (Transform child in parent)
        {
            if (child.CompareTag("Item"))
                hasItemOnTop = true;
            else
                hasItemOnTop = false;
        }
    }
}
