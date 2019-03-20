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

    public float Speed = 2.0f;

    private bool Jumping { get; set; }

    private Animator currentAnimator;
    private LookDirector currentLook;

	void Start () 
    {
        currentAnimator = GetComponent<Animator>();
        currentLook = LookDirector.Left;

        Jumping = false;
	}
	
	void FixedUpdate () 
    {
        float currentAxes = Input.GetAxis("Horizontal");

        //TODO: Animator Set

        Vector3 positionToSet = new Vector3();

        positionToSet.x = (Speed * currentAxes) * Time.fixedDeltaTime;

        transform.position = transform.position + positionToSet;

        LookDirector newLook = LookDirector.Left;

        if (positionToSet.x > 0)
        {
            newLook = LookDirector.Right;
        }

        if (currentLook != newLook)
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

            rigidbody.AddRelativeForce(new Vector2(1, 7), ForceMode2D.Impulse);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            Jumping = false;
        }
    }
}
