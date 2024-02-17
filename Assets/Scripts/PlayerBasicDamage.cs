using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerBasicDamage : MonoBehaviour
{

  [SerializeField]
  ProgressHandler[] healthMonitors;

  [SerializeField]
  float maxHealth = 100;

  [SerializeField]
  GameObject damageSplash;

  float health;
  float Health
  {
    set
    {
      health = Mathf.Clamp(value, 0, maxHealth);

      foreach (ProgressHandler handler in healthMonitors)
      {
        handler.UpdateValues(health, maxHealth);
      }

      CheckDeath();
    }
    get => health;
  }

  private void Start()
  {
    health = maxHealth;
  }

  public void Hurt(DamageDealer damageDealer, Vector3 location)
  {
    Instantiate(damageSplash, location, Quaternion.identity);

    Health -= damageDealer.DamageAmount;
  }

  private void CheckDeath()
  {
    if (Health <= 0)
    {
      GameStateManager.winnerName = GetComponent<PlayerController>().Opponent.FighterName;
      SceneManager.LoadScene("GameOver");
    }
  }
}
