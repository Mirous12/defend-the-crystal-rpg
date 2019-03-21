using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxHealth;

    public delegate void OnSingleEnemyDied(GameObject enemy);
    public event OnSingleEnemyDied SingleEnemyDied;

    private int _currentHealth;
    private int CurrentHealth
    {
        get { return _currentHealth; }

        set
        {
            _currentHealth = value;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                SingleEnemyDied(gameObject);
            }
        }
    }

    void Start()
    {
        var camera = GameObject.Find("Main Camera");

        if (camera != null)
        {
            GameManagerInstantiator gmInit = camera.GetComponent<GameManagerInstantiator>();

            if (gmInit != null)
            {
                EnemyManager enemyManager = gmInit.GetEnemyManager();

                if (enemyManager != null)
                {
                    enemyManager.SubscribeToNewEnemy(this);
                }
            }
        }

        CurrentHealth = MaxHealth;
    }

    
    void Update()
    {
        
    }

    public void SendDamage(int damage)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
        }
    }
}
