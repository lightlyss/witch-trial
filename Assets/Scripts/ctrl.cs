using UnityEngine;

public class ctrl : MonoBehaviour {
  private Animator ani;
  private Rigidbody2D rb;

  void Start() {
    ani = this.GetComponent<Animator>();
    rb = this.GetComponent<Rigidbody2D>();
  }

  void Update() {
    var horizontal = Input.GetAxis("Horizontal");
    if (horizontal > 0) {
      rb.velocity = new Vector2(2, rb.velocity.y);
    } else if (horizontal < 0) {
      rb.velocity = new Vector2(-2, rb.velocity.y);
    } else {
      rb.velocity = new Vector2(0, rb.velocity.y);
    }
    ani.SetInteger("xvel", (int) rb.velocity.x);
  }
}
