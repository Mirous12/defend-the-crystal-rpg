using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private enum LookDirector
    {
        Left = 1,
        Right
    }

    [Range(3.0f, 10.0f)]
    public float Speed = 2.0f;
    [Range(6.0f, 16.0f)]
    public float JumpStrength = 7.0f;
    public GameObject Crystal;

    private bool _jumping;
    private bool Jumping
    {
        get { return _jumping; }

        set
        {
            currentAnimator.SetBool( "IsJumping", value );
            _jumping = value;
        }
    }

    private Animator currentAnimator;
    private LookDirector currentLook;

    void Start()
    {
        currentAnimator = GetComponent<Animator>();
        currentLook = LookDirector.Left;

        Jumping = false;

        if (Crystal != null)
        {
            var CrystalClass = Crystal.GetComponent<Crystal>();

            if (CrystalClass != null)
            {
                CrystalClass.CrystalDestroyed += OnCrystalDestroyed;
            }
        }
    }

    void FixedUpdate()
    {
        if (!currentAnimator.GetBool("IsDead"))
        {
            float currentAxes = Input.GetAxis("Horizontal");

            if (currentAxes == 0f)
            {
                currentAnimator.SetBool("IsWalking", false);
            }
            else
            {
                currentAnimator.SetBool("IsWalking", true);
            }

            if (Jumping)
            {
                currentAnimator.SetBool("IsWalking", false);
            }

            Vector3 positionToSet = new Vector3();

            positionToSet.x = (Speed * currentAxes) * Time.fixedDeltaTime;

            transform.position = transform.position + positionToSet;

            LookDirector newLook = LookDirector.Left;

            if (positionToSet.x > 0)
            {
                newLook = LookDirector.Right;
            }
            else if (positionToSet.x < 0)
            {
                newLook = LookDirector.Left;
            }
            else
            {
                newLook = currentLook;
            }

            if (currentLook != newLook && !currentAnimator.GetBool("Attack"))
            {
                Vector3 scaleFactor = new Vector3(-transform.localScale.x, 1, 1);
                transform.localScale = scaleFactor;

                currentLook = newLook;
            }

            //Jump
            if (Input.GetKey(KeyCode.Space) && !Jumping)
            {
                Jumping = true;

                var rigidbody = GetComponent<Rigidbody2D>();

                rigidbody.AddRelativeForce(new Vector2(1, JumpStrength), ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if( collision.gameObject.name == "Ground" )
        {
            Jumping = false;
        }
    }

    void OnCrystalDestroyed()
    {
        currentAnimator.SetBool("IsDead", true);
    }
}
