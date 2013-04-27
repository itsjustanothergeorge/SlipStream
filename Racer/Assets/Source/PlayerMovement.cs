using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
    protected float speed = 8.0F;
    [SerializeField]
    protected float jumpSpeed = 10.0F;
    [SerializeField]
    protected float gravity = 20.0F;
    [SerializeField]
    protected float turnSpeed = 2.0f; //this might want to match horizontalSpeed on the camera
    [SerializeField]
    protected float horizontalSpeed = 2;
    [SerializeField]
    protected float vertialForwardSpeed = 1;
    [SerializeField]
    protected float vertialBackwardSpeed = 1;
    [SerializeField]
    protected float airDragSpeed = 1;
    [SerializeField]
    private Renderer characterModel;
    [SerializeField]
    private CharacterController controller;

    protected bool isMouseXLock = false;

    private Vector3 moveDirection = Vector3.zero;
    private float currentJumpHeight;
    private float forwardForce = 1;
    private float forceBackward = 0;
    private float horizontalRate;

    private void Awake()
    {
    	//controller = GetComponent<CharacterController>();
        //pc = GetComponent<PlayerCamera>();
    }

    protected virtual void Update()
    {
    	CharacterMovement();
        MouseRotate();
        
        if(Input.GetButtonDown("Fire1"))
        {
            LeftMouseClickDown();
        }
        if(Input.GetButton("Fire1"))
        {
            LeftMouseClick();
        }
        if(Input.GetButtonUp("Fire1"))
        {
            LeftMouseClickUp();
        }
    }

    protected virtual void LeftMouseClickDown()
    {
    }
    protected virtual void LeftMouseClickUp()
    {
    }

    protected virtual void LeftMouseClick()
    {
    }

    //Use this to see if I tapped the space Button
    protected virtual void SpaceButtonDown()
    { 
        Jump(jumpSpeed, 0);  
    }

    //Use this when you want to check if I am still holding
    //down the space button.
    protected virtual void SpaceButton()
    {
    }

    protected virtual void SpaceButtonUp()
    {
    }

    //Only call jump inside one of the SpaceButton functions.
    //Otherwise the character may not move as intended.
    protected void Jump(float height, float length)
    {
        moveDirection.y = height;
        currentJumpHeight = height; 
        forwardForce = length;
    }

    private void MouseRotate()
    {
        if(!isMouseXLock)
        {
            horizontalRate += turnSpeed * Input.GetAxis("Mouse X");
        }
        transform.localRotation = Quaternion.Euler(0, horizontalRate, 0);
    }

	protected void CharacterMovement()
    {
        
        float v = Input.GetAxis("Vertical");
        if(v > 0)
        {
            v *= vertialForwardSpeed;
        }
        else
        {
            v *= vertialBackwardSpeed;
        }
        float h = horizontalSpeed * Input.GetAxis("Horizontal");

        if (controller.isGrounded) 
        {
            forwardForce = 0;
            moveDirection = new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                SpaceButton();
            }
            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("Button is down");
                SpaceButtonDown();
            }
            if(Input.GetButtonUp("Jump"))
            {
                SpaceButtonUp();
            }
        }
        else
        {
            currentJumpHeight -= gravity * Time.deltaTime;
          //  Debug.Log("Not grounded");
            //forwardForce -= Time.deltaTime;
            //forwardForce = Mathf.Clamp(forwardForce,0,forwardForce);
            //moveDirection = new Vector3(h * speed * airDragSpeed , 0,
            //                             v * speed * airDragSpeed );
            //moveDirection.z += forwardForce;
            moveDirection.y = currentJumpHeight;
        }
        //Debug.Log("Move direction " + moveDirection);
        //moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
        //Vector3 rotationDirection = new Vector3(v * 10,0,-h * 10);
        //characterModel.transform.localEulerAngles = rotationDirection;
        
    }
}
