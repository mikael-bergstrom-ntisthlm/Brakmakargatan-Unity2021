using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterAnimation : MonoBehaviour
{
  void Start()
  {
    float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
    Destroy(this.gameObject, animTime);
  }
}
