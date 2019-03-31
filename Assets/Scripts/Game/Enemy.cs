using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyListener
{
    Enemy.OnSingleEnemyDied GetSingleEnemyDiedDelegate();
    Enemy.OnEnemyHitted GetEnemyHittedDelegate();
    Enemy.OnEnemyAttacked GetEnemyAttackedDelegate();
    Enemy.OnEnemyAttacked GetEnemyStopAttackingDelegate();
}

public class Enemy : MonoBehaviour
{
    public enum EnemySpawnPoint
    {
        Left = 1,
        Right,
        Undefined
    }
    public enum EventType
    {
        EnemyDied = 1,
        EnemyHitted,
        EnemyAttacked,
        EnemyStopAttacking
    }

    /* Events */
    public event OnSingleEnemyDied SingleEnemyDied;
    public event OnEnemyHitted EnemyHitted;
    public event OnEnemyAttacked EnemyAttacked;
    public event OnEnemyAttacked EnemyStopedAttacking;

    /* Delegates */
    public delegate void OnSingleEnemyDied( GameObject enemy );
    public delegate void OnEnemyHitted( GameObject hitObject, Enemy enemy, int damage );
    public delegate void OnEnemyAttacked( GameObject enemy, int enemyDamage );

    /* Private */
    private readonly int maxHealth = 5;

    /* Properties variables */
    private EnemySpawnPoint _spawnPoint = EnemySpawnPoint.Undefined;
    private int _currentHealth;
    private int _damage = 1;

    /* Properties */
    public int Damage
    {
        get { return _damage; }
        set { _damage = ( value > 0 ) ? value : 0; }
    }
    public EnemySpawnPoint SpawnPoint
    {
        set { _spawnPoint = value; }
        get { return _spawnPoint; }
    }
    public int CurrentHealth
    {
        get { return _currentHealth; }

        private set
        {
            _currentHealth = value;

            if( _currentHealth <= 0 )
            {
                _currentHealth = 0;
                SingleEnemyDied( gameObject );
                Kill();
            }
        }
    }

    //===========================================

    void Start()
    {
        if( SpawnPoint == EnemySpawnPoint.Undefined )
        {
            SpawnPoint = EnemySpawnPoint.Left;
        }

        CurrentHealth = maxHealth;
    }

    private void Kill()
    {
        Destroy( gameObject, 3.5f );
    }

    private void AttackStart()
    {
        EnemyAttacked( gameObject, Damage );
    }

    private void AttackEnd()
    {
        EnemyStopedAttacking( gameObject, Damage );
    }

    public void SendDamage( int damage, GameObject damageRefer = null )
    {
        if( CurrentHealth > 0 )
        {
            CurrentHealth -= damage;
            EnemyHitted( damageRefer, this, damage );
        }
    }

    public void SubscribeToEvent( EventType eventType, IEnemyListener listener )
    {
        switch( eventType )
        {
            case EventType.EnemyDied:
                if( listener.GetSingleEnemyDiedDelegate() != null )
                {
                    SingleEnemyDied += listener.GetSingleEnemyDiedDelegate();
                }
                break;
            case EventType.EnemyHitted:
                if( listener.GetEnemyHittedDelegate() != null )
                {
                    EnemyHitted += listener.GetEnemyHittedDelegate();
                }
                break;
            case EventType.EnemyAttacked:
                if( listener.GetEnemyAttackedDelegate() != null )
                {
                    EnemyAttacked += listener.GetEnemyAttackedDelegate();
                }
                break;
            case EventType.EnemyStopAttacking:
                if( listener.GetEnemyStopAttackingDelegate() != null )
                {
                    EnemyStopedAttacking += listener.GetEnemyStopAttackingDelegate();
                }
                break;
        }
    }
    public void UnsubscribeFromEvent( EventType eventType, IEnemyListener listener )
    {
        switch( eventType )
        {
            case EventType.EnemyDied:
                SingleEnemyDied -= listener.GetSingleEnemyDiedDelegate();
                break;
            case EventType.EnemyHitted:
                EnemyHitted -= listener.GetEnemyHittedDelegate();
                break;
            case EventType.EnemyAttacked:
                EnemyAttacked -= listener.GetEnemyAttackedDelegate();
                break;
            case EventType.EnemyStopAttacking:
                EnemyStopedAttacking -= listener.GetEnemyStopAttackingDelegate();
                break;
        }
    }
}
