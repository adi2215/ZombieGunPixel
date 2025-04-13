using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimations : MonoBehaviour
{
    private Animator animator;
    private Transform zombie;
    public bool facingRight = false;
    public PlayerMovement player;

    public GameObject Target;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        zombie = GetComponent<Transform>();
        Target = GameObject.FindGameObjectWithTag("Hero");
    }

    public void RotateToPointer(Vector2 lookDirection)
    {
        if (Target.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (Target.transform.position.x < transform.position.x  && facingRight)
        {
            Flip();
        }

        if (gameObject.tag == "Boss")
            return;
        //Vector3 scale = zombie.localScale;
        if (lookDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (lookDirection.x < 0 && facingRight)
        {
            Flip();
        }
        //zombie.localScale = new Vector3(scale.x, scale.y, scale.z);
    }

    private void Flip() 
    { 
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    public void PlayAnimation(Vector2 movementInput)
    {
        animator.SetBool("Moving", movementInput.magnitude > 0);
    }
}
