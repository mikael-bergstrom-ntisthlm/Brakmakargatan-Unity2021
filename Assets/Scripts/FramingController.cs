using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FramingController : MonoBehaviour
{
  [SerializeField]
  PlayerController player1;

  [SerializeField]
  PlayerController player2;

  [Header("Camera position/panning")]

  [SerializeField]
  bool cameraFollowX;
  [SerializeField]
  bool cameraFollowY;

  [Header("Camera size/zooming")]

  [SerializeField]
  Vector2 minViewportSize = new Vector2(12, 15);
  [SerializeField]
  Vector2 maxViewportSize = new Vector2(24, 30);

  [SerializeField]
  float viewportMargin = 1;

  private float verticalOffset;

  private Camera camera;

  void Awake()
  {
    camera = GetComponent<Camera>();
  }

  void Start()
  {
    Vector3 midPoint = CharacterMidpoint();
    verticalOffset = this.transform.position.y - midPoint.y; // how much above the midpoint between the character the camera should be

    KeepCameraCentered();
    KeepCharactersInFrame(true);
  }

  void Update()
  {
    KeepCameraCentered();
    KeepCharactersInFrame();
  }

  void KeepCameraCentered()
  {
    Vector3 midPoint = CharacterMidpoint();

    transform.position = new Vector3(
        (cameraFollowX ? 1 : 0) * midPoint.x,
        (cameraFollowY ? 1 : 0) * (midPoint.y + verticalOffset),
        transform.position.z
        );
  }

  void KeepCharactersInFrame(bool force = false)
  {
    // Use p1 distance from camera position as basis for calculations, regardless of which side it's on
    float p1DistanceFromCamCenter = Mathf.Abs(this.transform.position.x - player1.transform.position.x);

    // Calculate & clamp viewport half-size
    float viewportHalfWidth = Mathf.Clamp(p1DistanceFromCamCenter + viewportMargin, minViewportSize.x/2, maxViewportSize.y/2);
    float viewportHalfHeight = Mathf.Clamp(viewportHalfWidth / camera.aspect, minViewportSize.y / 2, maxViewportSize.x / 2);

    // Apply
    camera.orthographicSize = viewportHalfHeight;
  }

  Vector3 CharacterMidpoint()
  {
    return (player1.gameObject.transform.position + player2.gameObject.transform.position) * 0.5f;
  }

  void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(CharacterMidpoint(), 1f);
  }
}
