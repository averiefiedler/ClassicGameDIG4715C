using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D Player;

    private float horizontal;
    private float vertical;
    public float Speed = 350;

    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {

        movement = new Vector3(horizontal, vertical, 0).normalized;
        Player.velocity = new Vector3(movement.x * Speed, movement.y * Speed, movement.z * Speed) * Time.fixedDeltaTime;




        Debug.Log(movement);
    }
   
}
