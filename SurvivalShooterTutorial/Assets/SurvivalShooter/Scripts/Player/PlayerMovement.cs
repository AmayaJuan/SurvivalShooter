using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    int floorMask;
    float camRayLength = 100f;
    Vector3 movemnt;
    Animator anim;
    Rigidbody rb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movemnt.Set(h, 0f, v);
        movemnt = movemnt.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movemnt);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = !((v == 0) && (h == 0));
        anim.SetBool("IsWalking", walking);
    }
}
