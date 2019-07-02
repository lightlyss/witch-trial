using UnityEngine;
using System.Collections;

public class CameraLock : MonoBehaviour {
  public float dampTime = 0.15f;
  public Transform target;
  public GameObject world;

  private Vector3 velocity = Vector3.zero;
  private Camera cm;

  public void Start() {
    cm = this.GetComponent<Camera>();
  }

  public void FixedUpdate() {
    if (!target) return;
    Vector3 point = cm.WorldToViewportPoint(target.position);
    Vector3 delta = target.position - cm.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
    Vector3 destination = transform.position + delta;
    Vector3 step = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    if (world) {
      Vector2 wbounds = world.GetComponent<SpriteRenderer>().size;
      Vector2 wpos = world.GetComponent<Transform>().position;
      float cmheight = 2f * cm.orthographicSize;
      float cmwidth = cmheight * cm.aspect;
      step = new Vector3(
        Mathf.Clamp(step.x, wpos.x - wbounds.x/2f + cmwidth/2f, wpos.x + wbounds.x/2f - cmwidth/2f),
        Mathf.Clamp(step.y, wpos.y - wbounds.y/2f + cmheight/2f, wpos.y + wbounds.y/2f - cmheight/2f),
        step.z
      );
    }
    transform.position = step;
  }
}
