using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInstantiator : MonoBehaviour 
{
    EnemyManager enemyManager;
	
	void Start () 
    {
        enemyManager = new EnemyManager();
	}
	
	
	void Update () 
    {
		
	}

    public EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }
}
