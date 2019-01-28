using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{

    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public float speed = 8f;
    public LayerMask borderMask;

    private Rigidbody2D body;
    float horizontal = 0f;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = 0f;
#if UNITY_ANDROID
        TouchUpdate();
#else
        KeyboardUpdate();
#endif
    }

    private void TouchUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                float touchX = touch.position.x;
                horizontal = touchX < Screen.width / 2 ? -1f : 1f;
            }
        }
    }

    private void KeyboardUpdate()
    {
        if (Input.GetKey(rightKey))
        {
            horizontal = 1f;
            Debug.Log("Moviendo");
        }

        if (Input.GetKey(leftKey))
        {
            horizontal = -1f;
        }
    }

    void FixedUpdate()
    {
        Vector2 newPos = body.position;
        newPos.x += horizontal * speed * Time.fixedDeltaTime;

        Vector2 collisionPointCheck = newPos;
        body.MovePosition(newPos);
            horizontal = 0;
        
    }
}
