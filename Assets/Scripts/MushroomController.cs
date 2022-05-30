using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float speed;
    public Vector2 currentPosition;
    public Vector2 currentDirection;

    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        currentDirection = new Vector2(1,0);
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 nextPosition = currentPosition + speed * currentDirection.normalized * Time.fixedDeltaTime;
    }

    void FixedUpdate() 
    {
        Vector2 nextPosition = currentPosition + speed * currentDirection.normalized * Time.fixedDeltaTime;
        rigidBody.MovePosition(nextPosition);
    }
}
