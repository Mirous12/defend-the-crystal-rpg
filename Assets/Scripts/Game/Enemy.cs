using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyListener
{
    Enemy.OnSingleEnemyDied GetSingleEnemyDiedDelegate();
    Enemy.OnEnemyHitted GetEnemyHittedDelegate();
}

public class Enemy : MonoBehaviour
{
    public enum EnemySpawnPoint
    {
        Left = 1,
        Right
    }
    public enum EventType
    {
        EnemyDied = 1,
        EnemyHitted
    }

    public int MaxHealth;

    public delegate void OnSingleEnemyDied(GameObject enemy);
    public event OnSingleEnemyDied SingleEnemyDied;

    public delegate void OnEnemyHitted(GameObject hitObject, Enemy enemy, int damage);
    public event OnEnemyHitted EnemyHitted;

    private EnemySpawnPoint spawnPoint = EnemySpawnPoint.Left;
    public EnemySpawnPoint SpawnPoint
    {
        private set { spawnPoint = value; }

        get { return spawnPoint; }
    }

    private int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }

        private set
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

    private void Kill()
    {

    }

    public void SendDamage(int damage, GameObject damageRefer = null)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
            EnemyHitted(damageRefer, this, damage);
        }
    }

    public void SubscribeToEvent(EventType eventType, IEnemyListener listener)
    {
        switch(eventType)
        {
            case EventType.EnemyDied:
                if (listener.GetSingleEnemyDiedDelegate() != null)
                {
                    SingleEnemyDied += listener.GetSingleEnemyDiedDelegate();
                }
                break;
            case EventType.EnemyHitted:
                if (listener.GetEnemyHittedDelegate() != null)
                {
                    EnemyHitted += listener.GetEnemyHittedDelegate();
                }
                break;
        }
    }
    public void UnsubscribeFromEvent(EventType eventType, IEnemyListener listener)
    {
        switch (eventType)
        {
            case EventType.EnemyDied:
                SingleEnemyDied -= listener.GetSingleEnemyDiedDelegate();
                break;
            case EventType.EnemyHitted:
                EnemyHitted -= listener.GetEnemyHittedDelegate();
                break;
        }
    }
}
