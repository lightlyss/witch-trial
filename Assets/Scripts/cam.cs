using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
  public float dampTime = 0.15f;
  public Transform target;

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
    transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
  }
}
