using UnityEngine;

public class GameStateManager : MonoBehaviour
{
  public static string winnerName;

  private void Start() {
    DontDestroyOnLoad(this);  
  }
}
