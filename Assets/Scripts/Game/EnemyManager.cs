using UnityEngine;

public class EnemyManager
{
    public delegate void OnEnemyDied(GameObject enemy);

    public event OnEnemyDied EnemyDied;

    public void SubscribeToNewEnemy(Enemy enemy)
    {
        enemy.SingleEnemyDied += OnSingleEnemyDied;
    }

    public void UnsubscribeFromEnemy(Enemy enemy)
    {
        enemy.SingleEnemyDied -= OnSingleEnemyDied;
    }

    private void OnSingleEnemyDied(GameObject enemy)
    {
        if (enemy)
        {
            if (EnemyDied != null)
            {
                EnemyDied(enemy);
            }

            Debug.Log("Enemy Dead!");
        }
    }
}