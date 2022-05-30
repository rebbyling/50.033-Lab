using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private float speed = 3;
    private Vector2 currentPosition;
    private Vector2 currentDirection;

    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 nextPosition = currentPosition + speed * currentDirection.normalized * Time.fixedDeltaTime;
        rigidBody.MovePosition(nextPosition);
    }

    void FixedUpdate() 
    {
    
    }
}
