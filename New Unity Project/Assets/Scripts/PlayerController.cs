using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed;

    public GameObject interaction;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private Vector3 change;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GetComponentInChildren<MeshRenderer>().sortingLayerName = "Overlay";
        GetComponentInChildren<MeshRenderer>().sortingOrder = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        
        if(canMove)
        {
            change.x = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
            change.y = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime;

        }

        if(change != Vector3.zero)
        {
            rigidbody.MovePosition(transform.position + change);
            animator.SetFloat("movex", change.x);
            animator.SetFloat("movey", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Interact());
        }

    }

    public void AssertMovement(bool move)
    {
        canMove = move;
    }

    IEnumerator Interact()
    {
        interaction.SetActive(true);
        Debug.Log("Interaction");
        yield return new WaitForSeconds(.1f);
        interaction.SetActive(false);

        yield return null;
    }
}
