using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FramingController : MonoBehaviour
{
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
  private Vector3 midPoint;

  private Camera myCamera;

  private List<GameObject> players;
  private List<GameObject> Players
  {
    get
    {
      if (players == null || players.Count < 2 || players.Any(p => p == null))
      {
        players = GameObject.FindGameObjectsWithTag("Player")?.ToList();
      }
      return players;
    }
  }

  void Awake()
  {
    myCamera = GetComponent<Camera>();
  }

  void Start()
  {
    midPoint = CharacterMidpoint();
    verticalOffset = this.transform.position.y - midPoint.y; // how much above the midpoint between the character the camera should be

    KeepCameraCentered();
    KeepCharactersInFrame(true);
  }

  void Update()
  {
    midPoint = CharacterMidpoint();
    KeepCameraCentered();
    KeepCharactersInFrame();
  }

  void KeepCameraCentered()
  {
    transform.position = new Vector3(
        (cameraFollowX ? 1 : 0) * midPoint.x,
        (cameraFollowY ? 1 : 0) * (midPoint.y + verticalOffset),
        transform.position.z
        );
  }

  void KeepCharactersInFrame(bool force = false)
  {
    // Use p1 distance from camera position as basis for calculations, regardless of which side it's on
    float p1DistanceFromCamCenter = Mathf.Abs(this.transform.position.x - players[0].transform.position.x);

    // Calculate & clamp viewport half-size
    float viewportHalfWidth = Mathf.Clamp(p1DistanceFromCamCenter + viewportMargin, minViewportSize.x / 2, maxViewportSize.y / 2);
    float viewportHalfHeight = Mathf.Clamp(viewportHalfWidth / myCamera.aspect, minViewportSize.y / 2, maxViewportSize.x / 2);

    // Apply
    myCamera.orthographicSize = viewportHalfHeight;
  }

  Vector3 CharacterMidpoint()
  {
    Vector3 total = Vector3.zero;
    Players.ForEach(p => total += p.transform.position);
    return total / Players.Count;
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(CharacterMidpoint(), 1f);
  }
}
