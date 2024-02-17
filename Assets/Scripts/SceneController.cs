using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  public void GotoScene(string name)
  {
    SceneManager.LoadScene(name);
  }
}
