using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
  public enum DamageTypeEnum
  {
    physical
  }

  [SerializeField]
  float damageAmount = 10;

  [SerializeField]
  DamageTypeEnum damageType = DamageTypeEnum.physical;

  // Read only public properties
  public float DamageAmount => damageAmount;
  public DamageTypeEnum DamageType => damageType;

  private void OnCollisionEnter2D(Collision2D other)
  {
    other.gameObject.GetComponent<PlayerBasicDamage>()?.Hurt(this, other.GetContact(0).point);
  }
}
