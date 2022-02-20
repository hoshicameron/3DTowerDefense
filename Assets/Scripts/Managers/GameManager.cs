using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
   [SerializeField] private int baseMaxHealth = 10;
   [SerializeField] private int maxAllowedTower = 5;

   public int MaxAllowedTower => maxAllowedTower;

   private int score=0;
   private int baseCurrentHealth;


   public bool IsGameEnded { get; set; } = false;

   protected override void Awake()
   {
      base.Awake();

      baseCurrentHealth = baseMaxHealth;
   }

   private void Start()
   {
      UIManager.Instance.UpdateScoreUI(score);
      UIManager.Instance.UpdateBaseHealthUI(baseCurrentHealth);
      UIManager.Instance.UpdateMaxAllowedTowerUI(maxAllowedTower);
   }

   public void ReduceHealth(int amount)
   {
      baseCurrentHealth -= amount;
      baseCurrentHealth = Mathf.Clamp(baseCurrentHealth, 0, baseMaxHealth);

      UIManager.Instance.UpdateBaseHealthUI(baseCurrentHealth);

      if (baseCurrentHealth <= 0)
      {
         IsGameEnded = true;

        UIManager.Instance.ShowGameOverUI();
      }
   }

   public void UpdateScore(int amount)
   {
      score += amount;
      UIManager.Instance.UpdateScoreUI(score);
   }
}
