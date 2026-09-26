using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerDetection : MonoBehaviour
{
   [SerializeField] private InputActionAsset inputActionAsset;

   public static event Action OnControllerChange;
   private string currentController;
   

   private void Awake()
   {
      inputActionAsset.FindAction("Controller").performed += GetControllerName;
   }

   private void Start()
   {
      currentController = PlayerPrefs.GetString("ControllerName");
      if (currentController == null)
         currentController = "Keyboard";
   }

   private void GetControllerName(InputAction.CallbackContext ctx)
   {
      string newController = ctx.action.activeControl.device.name;
      Debug.Log(newController);
      
      if (newController != currentController)
      {
         currentController = newController;
         PlayerPrefs.SetString("ControllerName", currentController);
         OnControllerChange?.Invoke();
         print("Controller Changed");
      }
   }

   private void OnEnable()
   {
      inputActionAsset.Enable();
   }

   private void OnDisable()
   {
      inputActionAsset.Disable();
      inputActionAsset.FindAction("Controller").performed -= GetControllerName;
   }

}
