using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController Current;

    public float speed  =  4f;
    private float gravity = -9.86f;
    private float jmphgt = 6f;
    private float smoothtime = 0.25f; 
    float smVelocity; 
    public Vector3 velocity;
    
    public Transform cam;
    bool isongrd; 
    
    public Animator animator;
    public float GroundDistance = 0.4f;
    public LayerMask groundMask;
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 mov = new Vector3(horizontal,0f,vertical);

        
        if(mov.magnitude > 0.1f){

            if(Input.GetKey(KeyCode.LeftShift)){
                speed = 8f;
            }
            else{
                    speed = 4f;
            }

            if(speed < 7.9){
                animator.SetFloat("speed", 4);
            }
            else{
                animator.SetFloat("speed", 8);
            }
            float tangle = Mathf.Atan2(mov.x, mov.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle =  Mathf.SmoothDampAngle(transform.eulerAngles.y, tangle, ref smVelocity, smoothtime);
            transform.rotation = Quaternion.Euler(0f, tangle, 0f);

            Vector3 movedir = Quaternion.Euler(0f , tangle, 0f)  * Vector3.forward;

            Current.SimpleMove(movedir.normalized * speed);
        }
        else{
            animator.SetFloat("speed",0);
        }
        if (Input.GetKeyDown(KeyCode.Space) ){
            velocity.y = jmphgt;
            animator.SetBool("jumpgrd", true);
        }
        else{
            velocity.y += gravity * Time.deltaTime;
        }
        Current.Move(velocity  * Time.deltaTime);
    }
} 
