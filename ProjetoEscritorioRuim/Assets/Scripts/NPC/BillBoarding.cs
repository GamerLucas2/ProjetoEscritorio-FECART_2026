using System.Collections;
using UnityEngine;
public class BillBoarding : MonoBehaviour
{
    private Transform cameraTransform;
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        StartCoroutine(StarePlayer());
    }
    private IEnumerator StarePlayer()
    {
        WaitForSeconds lookTime = new WaitForSeconds(0.1f);
        while (true)
        {
            transform.LookAt(cameraTransform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            yield return lookTime;
        }
    }
}
