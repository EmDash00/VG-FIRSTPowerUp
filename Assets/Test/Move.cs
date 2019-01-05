using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    Rigidbody2D body;
    BoxCollider2D boxCollider;

    [SerializeField]
    private LayerMask collisionMask;

    
    public float speed;
    public float restVelocity = 0;
    public float jumpHeight;
    public float standingRatio;

    public float standardGravity;

    public float jumpGravity;

    public bool isJumping = false;

    public Rigidbody2D lastStandingSurface = null;
    public Rigidbody2D standingSurface = null;

    private Vector2 input;

    private float lastInput = 0;

    [SerializeField]
    private float fallingCollisionCheckDistance;

    [SerializeField]
    private float downJumpCheckDistance;

    private bool canJump = true;

    public Moving movementDirection = Moving.Still;

    public bool isBoostActive = false;

    public float boostEffectStrength;

    public Transform checkpoint;





    // Use this for initialization
    void Start ()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        //Time.timeScale = .1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateMovementAnimationInfo();


	}

    private void updateMovementAnimationInfo()
    {
        if (this.body.velocity.x > 0)
        {
            this.movementDirection = Moving.Right;
        }
        else if (this.body.velocity.x == 0)
        {
            this.movementDirection = Moving.Still;
        }
        else if(this.body.velocity.x < 0)
        {
            this.movementDirection = Moving.Left;
        }
    }

    // Fixed update is called once per fixed update frame. (20ms)
    void FixedUpdate()
    {

        Vector2 setter = body.velocity;

        if (InputManager.getHorizontalInput() != 0)
        {
            setter.x += getHoriziontalInput();
        }
        else
        {
            setter.x = getPlatformVelocity().x;
        }

        if (InputManager.getJumpPressed())
        {
            setter.y += getVerticalInput();
        }
        else
        {

                if (getGroundingState("F") == GroundingState.Grounded)
                {
                    setter.y = getPlatformVelocity().y;
                }



                       
        }
        //Debug.Log(setter);

        this.body.velocity = setter;
        setGravityScale();
        this.lastInput = InputManager.getHorizontalInput();


    }




    void OnCollisionEnter2D(Collision2D col) //Grab mechanic?
    {
        if (getGroundingState("C") == GroundingState.Grounded && this.lastStandingSurface != this.standingSurface)
        {
            Vector2 velocityDifference = (this.lastStandingSurface?.velocity ?? Vector2.zero) - this.standingSurface.velocity;

            this.body.velocity += velocityDifference;
        }
    }

    private void setGravityScale()
    {
        if (this.body.velocity.y > 0)
        {
            if (getGroundingState("G") == GroundingState.Airbourne)
            {
                if (InputManager.getJumpHeld() && this.isJumping)
                {
                    this.body.gravityScale = this.jumpGravity;
                }
                else
                {
                    this.body.gravityScale = this.standardGravity;
                }

            }
        }
        else
        {
            this.body.gravityScale = this.standardGravity;
        }

    }

    private Vector2 getInputVelocity()
    {
        return new Vector2(getHoriziontalInput(), getVerticalInput());
    }

    private Vector2 getPlatformVelocity()
    {

            if (getGroundingState("P") == GroundingState.Grounded)
            {
                return this.standingSurface?.velocity ?? Vector2.zero;
            }
            else
            {
                return Vector2.zero;
            }



    }

    private GroundingState getGroundingState(string id)
    {

        Vector2 point = (Vector2)this.boxCollider.bounds.min + Vector2.right;

        float downColcheck = this.body.velocity.y < 0 ? this.downJumpCheckDistance : 0;

        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.down, 1, this.collisionMask);

        if (hit)
        {
            this.lastStandingSurface = this.standingSurface;
            this.standingSurface = hit.rigidbody;

            //if (this.body.velocity.y  - hit.rigidbody.velocity.y <= 0)
            //{
            //    if (hit.distance <= this.fallingCollisionCheckDistance)
            //    {
            //        this.isJumping = false;
            //        this.canJump = true;


            //        return GroundingState.Grounded;
            //    }
            //    else
            //    {
            //        this.isJumping = false;
            //        this.canJump = true;

            //        return GroundingState.Nearly_Grounded;
            //    }
            //}
            //else
            //{
            //    return GroundingState.Airbourne;
            //}


            if (hit.collider.bounds.size.x >= standingRatio * this.boxCollider.bounds.size.x && this.body.velocity.y <= 0)
            {
                if (hit.distance <= this.fallingCollisionCheckDistance)
                {
                    Debug.Log(isJumping ? "Changing " + id : "Not Changing " + id);
                    this.isJumping = false;
                    this.canJump = true;


                    return GroundingState.Grounded;
                }
                else
                {
                    this.isJumping = false;
                    this.canJump = true;

                    return GroundingState.Nearly_Grounded;
                }

            }
            else
            {
                return GroundingState.Unstable;
            }

        }
        else
        {
            this.lastStandingSurface = this.standingSurface;

            this.standingSurface = null;

            return GroundingState.Airbourne;   
        }
    }

    private float getHoriziontalInput()
    {
        float boost = this.isBoostActive ? this.boostEffectStrength : 0;

        if (InputManager.getHorizontalInput() != 0)
        {
            return (this.speed + boost) * (InputManager.getHorizontalInput() - this.lastInput);
        }
        else
        {
            return 0.0f;
        }
    }

    private float getVerticalInput()
    {
        if (InputManager.getJumpPressed() && this.canJump)
        {
            this.isJumping = true;
            this.canJump = false;

            return Mathf.Sqrt(2 * (Physics2D.gravity.magnitude * this.body.gravityScale) * jumpHeight);
        }
        else
        {
            return 0.0f;
        }
    }

    public enum Moving
    {
        Left,
        Right,
        Still
    }

    private enum GroundingState
    {
        Grounded,
        Nearly_Grounded,
        Unstable,
        Airbourne
    };

}



