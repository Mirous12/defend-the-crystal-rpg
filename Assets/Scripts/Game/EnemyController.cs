using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyListener
{
    public float DistanceToAttack = 1.5f;
    public float MaxDyingAnimationTime = 2.5f;

    public delegate void OnEnemyDiedAnimationFinished(Enemy enemy);
    public event OnEnemyDiedAnimationFinished EnemyDyingAnimationFinished;

    private Animator animator;
    private GameObject crystalObject;
    private Transform currentEnemy;
    private Enemy enemy;

    private bool isMovingEnabled = true;
    private bool isDead = false;
    private bool isDeadAnimationOngoing = false;
    private float timeOnAnimationDelay = 0f;

    private bool isNearCrystal = false;
    public bool IsNearCrystal
    {
        private set { isNearCrystal = value; }
        get { return isNearCrystal; }
    }

    private bool isMoving = false;
    public bool IsMoving
    {
        private set 
        { 
            isMoving = value;
            animator.SetBool("IsWalking", value);
        }
        get { return isMoving; }
    }

    private float speed = 1.0f;
    public float Speed
    {
        private set { speed = value; }
        get { return speed; }
    }

	void Start () 
    {
        currentEnemy = GetComponent<Transform>();
        crystalObject = GameObject.Find("Crystal");
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();

        if (enemy != null)
        {
            enemy.SubscribeToEvent(Enemy.EventType.EnemyHitted, this);
            enemy.SubscribeToEvent(Enemy.EventType.EnemyDied, this);
        }
	}
	
	void FixedUpdate () 
    {
        if (!isDead)
        {
            float distance = Mathf.Abs(crystalObject.transform.position.x - transform.position.x);

            IsNearCrystal = distance < DistanceToAttack;

            if (!IsNearCrystal)
            {
                IsMoving = true;

                float movingFactor = 0.0f;

                if (enemy.SpawnPoint == Enemy.EnemySpawnPoint.Left) { movingFactor = 1f; }
                else if (enemy.SpawnPoint == Enemy.EnemySpawnPoint.Right) { movingFactor = -1f; }

                Vector3 positionToSet = new Vector3();

                positionToSet.x = (Speed * movingFactor) * Time.fixedDeltaTime;

                if (isMovingEnabled)
                {
                    transform.position = transform.position + positionToSet;
                }
            }
            else
            {
                IsMoving = false;
            }
        }
        else if (isDeadAnimationOngoing)
        {
            float percent = 1 - (timeOnAnimationDelay / MaxDyingAnimationTime);

            if (percent > 1) percent = 1;

            SetEnemyOpacity(percent);

            timeOnAnimationDelay += Time.fixedDeltaTime;
        }
	}

    private void SetEnemyOpacity(float percent)
    {
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>(transform.GetComponentsInChildren<SpriteRenderer>());

        foreach (var item in spriteRenderers)
        {
            if (item != null && percent >= 0 && percent <= 1)
            {
                item.color = new Color(1f, 1f, 1f, percent);

                if (percent == 0)
                {
                    EnemyDyingAnimationFinished(this.enemy);
                    isDeadAnimationOngoing = false;
                }
            }
        }
    }

    private void OnEnemyHitted(GameObject hitObject, Enemy enemy, int damage)
    {
        if (hitObject != null && enemy == this.enemy)
        {
            if (hitObject.GetComponent<Weapon>() != null)
            {
                Rigidbody2D rgbd = GetComponent<Rigidbody2D>();

                Vector2 forceVector = new Vector2();

                if (enemy.SpawnPoint == Enemy.EnemySpawnPoint.Left) forceVector.x = -8f;
                else if (enemy.SpawnPoint == Enemy.EnemySpawnPoint.Right) forceVector.x = 8f;

                forceVector.y = 8f;

                rgbd.AddForce(forceVector, ForceMode2D.Impulse);

                isMovingEnabled = false;

                StartCoroutine(MovingEnableCoroutine(0.5f));
            }
        }
    }

    private void OnEnemyDead(GameObject enemy)
    {
        if (enemy == this.enemy.gameObject)
        {
            isDead = true;
            isDeadAnimationOngoing = true;
            IsMoving = false;
        }
    }

    IEnumerator MovingEnableCoroutine(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        isMovingEnabled = true;
    }

    /* Enemy Listener */
    public Enemy.OnSingleEnemyDied GetSingleEnemyDiedDelegate()
    {
        return OnEnemyDead;
    }

    public Enemy.OnEnemyHitted GetEnemyHittedDelegate()
    {
        return OnEnemyHitted;
    }
}
