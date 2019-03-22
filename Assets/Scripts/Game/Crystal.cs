using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour 
{
    public int MaxHealth = 5;

    public delegate void OnCrystalDestroyed();
    public event OnCrystalDestroyed CrystalDestroyed;

    private float testTime = 0.0f;

    private int _currentHealth = 0;
    private int CurrentHealth 
    {
        get { return _currentHealth; }
        set 
        {
            _currentHealth = value;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                CrystalDestroyed();
            }
        }
    }

	void Start () 
    {
        CurrentHealth = MaxHealth;
	}
	
	void Update () 
    {

	}

    void DamageCrystal(int damage)
    {
        if (damage > 0)
        {
            CurrentHealth -= damage;
        }
    }
}
