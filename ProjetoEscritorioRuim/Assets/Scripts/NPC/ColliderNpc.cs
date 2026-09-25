using System;
using UnityEngine;

public class ColliderNpc : MonoBehaviour
{
    [Header("Interaction UI")]
    [SerializeField] private GameObject interactionIndicator;

    private void Awake()
    {
        interactionIndicator = GameObject.FindWithTag("Indicator");
        print(interactionIndicator.name);
    }

    private void Start()
    {
        if (interactionIndicator.activeSelf)
            interactionIndicator.SetActive(false);
        else
            print("Interaction Indicator Disabled");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionIndicator.SetActive(false);
        }
    }
}
