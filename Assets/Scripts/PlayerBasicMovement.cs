using UnityEngine;
using UnityEngine.InputSystem;

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
  Animator animator;

  private Vector2 inputVector;
  private bool jumpPressed = false;
  private bool flipped = false;

  public movementType LastMovement { get; private set; }

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = spriteObject.GetComponent<Animator>();
  }

  void Update()
  {
    CheckHorizontalMovement();
    CheckJump();
    CheckMirrored(GetComponent<PlayerController>().Opponent.gameObject);
  }

  public void CheckHorizontalMovement()
  {
    Vector2 movementVector = horizontalSpeed * inputVector.x * Vector2.right;

    if (movementVector.magnitude > 0)
    {
      LastMovement = movementVector.x > 0 ? movementType.Right : movementType.Left;
    }

    transform.Translate(movementVector * Time.deltaTime);
    animator.SetFloat("xmove", movementVector.x);
  }

  private void CheckJump()
  {
    bool grounded = IsOnGround();

    if (jumpPressed && grounded)
    {
      rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    jumpPressed = false;
    animator.SetBool("onground", grounded);
  }

  public void CheckMirrored(GameObject target)
  {
    bool isOnTheRight = target.transform.position.x < transform.position.x;

    if (isOnTheRight != flipped)
    {
      // Flip entire object (because we also want to flip sub-objects)
      Vector3 workingScale = spriteObject.transform.localScale;
      workingScale.x *= -1;
      spriteObject.transform.localScale = workingScale;

      // Flip the x-offsets of the base fighter object's colliders, just in case
      Collider2D[] colliders = GetComponents<Collider2D>();
      foreach (Collider2D collider in colliders)
      {
        Vector2 offset = collider.offset;
        offset.x *= -1;
        collider.offset = offset;
      }

      flipped = !flipped;
    }

  }

  private bool IsOnGround()
  {
    (Vector2 checkerPosition, Vector2 checkerSize) = GetGroundCheckerValues();

    bool isOnGround = Physics2D.OverlapBox(checkerPosition, checkerSize, 0, groundLayer);

    return isOnGround;
  }

  private (Vector2 position, Vector2 size) GetGroundCheckerValues()
  {
    Collider2D collider = GetComponent<Collider2D>();

    Vector2 size = new(
      collider.bounds.size.x,
      0.4f
    );

    Vector2 position = 
    new(
      collider.bounds.center.x,
      collider.bounds.min.y
    );

    return (position, size);
  }

  private void OnMove(InputValue value) => inputVector = value.Get<Vector2>();
  private void OnJump(InputValue value) => jumpPressed = true;

  private void OnDrawGizmosSelected()
  {
    (Vector2 checkerPosition, Vector2 checkerSize) = GetGroundCheckerValues();
    Gizmos.DrawWireCube(checkerPosition, checkerSize);
  }

}
