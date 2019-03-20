using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackScript : MonoBehaviour {

    private Animator currentAnimator;
	void Start () 
    {
        currentAnimator = GetComponent<Animator>();
	}
	
	void Update () 
    {
		if (Input.GetMouseButton(0))
        {
            if (!currentAnimator.GetBool("Attack"))
            {
                currentAnimator.SetBool("Attack", true);
            }
        }
	}

    public void OnAttackEnd()
    {
        currentAnimator.SetBool("Attack", false);
    }
}
