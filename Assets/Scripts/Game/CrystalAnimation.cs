using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalAnimation : MonoBehaviour 
{
    //Interface
    [Range(0.01f, 0.5f)]
    public float MaxRangeCoefficient = 0.2f;
    [Range(0.4f, 4.0f)]
    public float Speed = 2.0f;

    private enum AnimationDirection
    {
        UP = 1,
        DOWN
    }

    private float normalYPos;
    private float maxDifRange;
    private float currentDif;
    private AnimationDirection currentDirection;

	void Start () 
    {
        Vector2 crystalSize = GetComponent<SpriteRenderer>().size;

        normalYPos = transform.position.y;

        maxDifRange = crystalSize.y * MaxRangeCoefficient;
        currentDif = 0f;
        currentDirection = AnimationDirection.DOWN;
	}
	
	void FixedUpdate()
    {
        if (currentDif < maxDifRange)
        {
            float yAxesTransform = 0.0f;

            yAxesTransform = Time.deltaTime * ( Speed / 10.0f );

            if (currentDirection == AnimationDirection.DOWN)
            {
                yAxesTransform = -yAxesTransform;
            }

            transform.position = new Vector2(transform.position.x, transform.position.y + yAxesTransform);

            currentDif = Mathf.Abs( normalYPos - transform.position.y );
        }
        else
        {
            if (currentDirection == AnimationDirection.DOWN)
            {
                currentDirection = AnimationDirection.UP;
            }
            else
            {
                currentDirection = AnimationDirection.DOWN;
            }

            currentDif = -currentDif;
        }
    }
}
