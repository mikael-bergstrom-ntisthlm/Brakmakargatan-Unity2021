using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerBasicMovement : MonoBehaviour
{

    public enum movementType { Right, Left, None };

    [SerializeField]
    float horizontalSpeed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    GameObject spriteObject;

    Rigidbody2D rb;
    PlayerController playerController;
    Animator animator;

    private string verticalAxis;
    private string horizontalAxis;

    

    private bool flipped = false;

    public movementType LastMovement
    {
        get; private set;
    }

    bool hasReleasedJumpButton = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();

        animator = spriteObject.GetComponent<Animator>();

        horizontalAxis = playerController.HorizontalAxis;
        verticalAxis = playerController.VerticalAxis;
    }

    void Update()
    {

        float horizontalAxisValue = Input.GetAxisRaw(horizontalAxis);
        float verticalAxisValue = Input.GetAxisRaw(verticalAxis);

        CheckHorizontalMovement(horizontalAxisValue);
        CheckJump(verticalAxisValue);
        CheckMirrored(GetComponent<PlayerController>().Opponent.gameObject);
    }


    public void CheckHorizontalMovement(float horizontalAxisValue)
    {
        Vector2 movementVector = Vector2.zero;

        if (horizontalAxisValue > 0)
        {
            LastMovement = movementType.Right;
            movementVector += Vector2.right * horizontalAxisValue * horizontalSpeed * Time.deltaTime;
        }
        else if (horizontalAxisValue < 0)
        {
            LastMovement = movementType.Left;
            movementVector += Vector2.right * horizontalAxisValue * horizontalSpeed * Time.deltaTime;
        }

        transform.Translate(movementVector);

        animator.SetFloat("xmove", horizontalAxisValue);
    }

    private void CheckJump(float verticalAxisValue)
    {

        bool grounded = IsOnGround();

        if (verticalAxisValue > 0 && hasReleasedJumpButton == true && grounded)
        {
            DoJump();
            hasReleasedJumpButton = false;
        }
        else if (verticalAxisValue <= 0)
        {
            hasReleasedJumpButton = true;
        }

        animator.SetBool("onground", grounded);
    }

    public void CheckMirrored(GameObject target)
    {
        bool isOnTheRight = target.transform.position.x < transform.position.x;

        if (isOnTheRight != flipped)
        {
            Vector3 workingScale = spriteObject.transform.localScale;
            workingScale.x *= -1;
            spriteObject.transform.localScale = workingScale;

            Collider2D[] colliders = GetComponents<Collider2D>();
            foreach(Collider2D collider in colliders)
            {
                Vector2 offset = collider.offset;
                offset.x *= -1;
                collider.offset = offset;
            }

            flipped = !flipped;
        }

    }



    public void DoJump()
    {
        Vector3 newVelocity = rb.velocity;

        newVelocity.y += jumpForce;

        rb.velocity = newVelocity;
    }

    private bool IsOnGround()
    {

        Vector2 sizeOfGroundChecker = Vector2.up * 0.4f;

        Collider2D collider = GetComponent<Collider2D>();

        sizeOfGroundChecker.x = collider.bounds.size.x;

        Vector2 positionOfGroundChecker = (Vector2)transform.position + Vector2.down * collider.bounds.extents.y;

        bool isOnGround = Physics2D.OverlapBox(positionOfGroundChecker, sizeOfGroundChecker, 0, groundLayer);

        return isOnGround;
    }

}
