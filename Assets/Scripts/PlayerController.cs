using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  string fighterName;

  [SerializeField]
  PlayerController opponent;

  public PlayerController Opponent {
    get {
      if (opponent == null) opponent = FindOpponent();
      return opponent;
    }
  }

  private void Update()
  {
    if (opponent == null) opponent = FindOpponent();
  }

  public string FighterName => fighterName;

  public PlayerController FindOpponent()
  {
    GameObject[] candidates = GameObject.FindGameObjectsWithTag("Player");

    return candidates.First(c => c != this.gameObject).GetComponent<PlayerController>();
  }
}
