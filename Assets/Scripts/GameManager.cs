using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
   [SerializeField] private int baseMaxHealth = 10;
   [SerializeField] private int maxAllowedTower = 5;

   private int score;
   private int baseCurrentHealth;
   public bool IsGameEnded { get; set; } = false;

   protected override void Awake()
   {
      base.Awake();

      score = 0;
      baseCurrentHealth = baseMaxHealth;
   }

   public void ReduceHealth(int amount)
   {
      baseCurrentHealth -= amount;
      baseCurrentHealth = Mathf.Clamp(baseCurrentHealth, 0, baseMaxHealth);

      // Todo update ui

      if (baseCurrentHealth < 0)
      {
         IsGameEnded = true;

         //to do UIManager show End game menu
         //stop everything
      }
   }
}
