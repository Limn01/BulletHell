using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed;

    
    Rigidbody2D playerRigid;
    Vector3 moveDirection;
    Vector3 playerDirection;
    Animator anim;
    Rigidbody2D body;
    GunRotation gunRotation;
    
    // Use this for initialization
    void Awake ()
    {
        gunRotation = GetComponentInChildren<GunRotation>();
        playerRigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        HandleMovement(h, v);
        
    }

    public void HandleMovement(float h, float v)
    {
        moveDirection.Set(h, v, 0f);
        moveDirection = moveDirection.normalized * moveSpeed;
        playerRigid.MovePosition(transform.position + moveDirection);

        if (h > 0)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            
            anim.SetBool("IsRunning", true);
        }

        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (h < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            
            anim.SetBool("IsRunning", true);
        }

        else
        {
            anim.SetBool("IsRunning", false);
        }
        
    }

   

}
    

