using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int Damage = 1;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        Crystal crystal = collision.gameObject.GetComponent<Crystal>();

        if( crystal )
        {
            crystal.DamageCrystal( Damage );
        }
    }
}
