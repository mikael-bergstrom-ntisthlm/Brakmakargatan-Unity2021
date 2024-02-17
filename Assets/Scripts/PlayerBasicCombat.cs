using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerBasicCombat : MonoBehaviour
{
  [SerializeField]
  Animator animator;

  [SerializeField]
  float delayBetweenMoves = 0.5f;

  private float punchTimer = 0;
  private float kickTimer = 0;

  void OnKick()
  {
    print("kick!");
    if(kickTimer <= 0)
    {
      animator.SetTrigger("kick");
      kickTimer = delayBetweenMoves;
    }
  }

  void OnPunch()
  {
    print("punch!");
    if (punchTimer <= 0)
    {
      animator.SetTrigger("punch");
      punchTimer = delayBetweenMoves;
    }
  }

  void Update()
  {
    if (punchTimer > 0) punchTimer -= Time.deltaTime;
    if (kickTimer > 0) kickTimer -= Time.deltaTime;
  }
}
