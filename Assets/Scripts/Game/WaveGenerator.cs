using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    enum EnemySpawnType
    {
        Left,
        Right
    }

    /* Unity Properties */
    public GameObject LeftWaveGenerator;
    public GameObject RightWaveGenerator;
    public int ScalePeriod = 3;
    public float BaseSpawnTime = 9.5f;
    public float SpawnScaleFactor = 0.3f;
    public float StartEnemySpeed = 1.0f;
    public float EnemySpeedFactor = 0.1f;
        
    /* Private Fileds */
    private List<GameObject> enemiesList = new List<GameObject>();
    int currentEnemyIndex = 0;
    float currentSpawnTimer = 0f;
    public int _currentScore = 0;

    public int CurrentScore
    {
        get { return _currentScore; }
        set { _currentScore = value; }
    }

    void FixedUpdate()
    {
        currentSpawnTimer += Time.fixedDeltaTime;

        if( currentSpawnTimer >= BaseSpawnTime )
        {
            currentSpawnTimer = 0;

            float randomNumber = Random.value;
            EnemySpawnType spawnType = EnemySpawnType.Left;

            if( randomNumber >= 0.5f )
            {
                spawnType = EnemySpawnType.Right;
            }

            CreateEnemy( spawnType );

            currentEnemyIndex++;

            if( currentEnemyIndex % ScalePeriod == 0 )
            {
                IncreaseDifficult();
            }
        }
    }

    private void CreateEnemy( EnemySpawnType enemySpawnType )
    {
        Vector3 enemySpawnPosition = LeftWaveGenerator.transform.position;
        Enemy.EnemySpawnPoint spawnPoint = Enemy.EnemySpawnPoint.Left;

        if ( enemySpawnType == EnemySpawnType.Right)
        {
            spawnPoint = Enemy.EnemySpawnPoint.Right;
            enemySpawnPosition = RightWaveGenerator.transform.position;
        }

        GameObject newEnemy = GameObject.Instantiate( ( GameObject )( Resources.Load( "Prefabs/GameEnemy", typeof( GameObject ) ) ) );

        if( newEnemy )
        { 
            newEnemy.name = "Enemy" + currentEnemyIndex;
            enemiesList.Add( newEnemy );

            newEnemy.transform.position = enemySpawnPosition;

            Enemy enemyScript = newEnemy.GetComponent<Enemy>();

            if( enemyScript )
            {
                enemyScript.SpawnPoint = spawnPoint;

                enemyScript.SingleEnemyDied += OnEnemyDied;
            }
        }
    }

    private void IncreaseDifficult()
    {
        BaseSpawnTime -= SpawnScaleFactor;

        if( BaseSpawnTime < 0.9f )
        {
            BaseSpawnTime = 0.9f;
        }

        if( StartEnemySpeed < 3.0f )
        {
            StartEnemySpeed += EnemySpeedFactor;
        }
        else
        {
            StartEnemySpeed += EnemySpeedFactor / 3.0f;
        }
    }

    private void OnEnemyDied( GameObject enemy )
    {
        enemiesList.Remove( enemy );

        CurrentScore++;
    }
}
