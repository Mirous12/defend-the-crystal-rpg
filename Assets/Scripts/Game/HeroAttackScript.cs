using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackScript : MonoBehaviour
{
    private Animator currentAnimator;
    private Transform weapon;
    void Start()
    {
        currentAnimator = GetComponent<Animator>();

        Transform body = transform.Find( "body" );

        if( body )
        {
            weapon = body.Find( "Weapon" );
        }

    }

    void Update()
    {
        if( Input.GetMouseButton( 0 ) && !currentAnimator.GetBool( "IsDead" ) )
        {
            if( !currentAnimator.GetBool( "Attack" ) )
            {
                currentAnimator.SetBool( "Attack", true );
            }
        }
    }

    public void OnAttackEnd()
    {
        if( weapon != null )
        {
            PolygonCollider2D collider = weapon.GetComponent<PolygonCollider2D>();

            if( collider != null )
            {
                collider.enabled = false;
            }
        }

        currentAnimator.SetBool( "Attack", false );
    }

    public void OnAttackStart()
    {
        if( weapon )
        {
            PolygonCollider2D collider = weapon.GetComponent<PolygonCollider2D>();

            if( collider )
            {
                collider.enabled = true;
            }
        }
    }
}
