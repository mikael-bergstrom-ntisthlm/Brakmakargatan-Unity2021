using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetupController : MonoBehaviour
{
  [SerializeField]
  GameObject[] players;

  void Start()
  {
    // Reinstance every player; unfortunately necessary until civilized way to handle multiple inputs is implemented

    foreach(GameObject player in players)
    {
      var instance = PlayerInput.Instantiate(player, controlScheme: "Keyboard&Mouse", pairWithDevices: new InputDevice[] { Keyboard.current, Mouse.current });
      instance.transform.parent = player.transform.parent;
      Destroy(player);
    }
  }
}

public static class InputInitializer
{
    public static PlayerInput Initialize(this PlayerInput input)
    {
        var instance = PlayerInput.Instantiate(input.gameObject, controlScheme: "Keyboard&Mouse", pairWithDevices: new InputDevice[] { Keyboard.current, Mouse.current });
 
        instance.transform.parent = input.transform.parent;
        instance.transform.position = input.transform.position;
 
        Object.Destroy(input.gameObject);
        return instance;
    }
}