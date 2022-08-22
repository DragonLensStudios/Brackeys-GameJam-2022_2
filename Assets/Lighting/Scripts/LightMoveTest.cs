using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMoveTest : MonoBehaviour
{
    Rigidbody2D rigidbody;
    BoxCollider2D collider2D;
    GameObject player;
    float borderEdge = 4.5f;
    float startPositionGap = 14f;
    bool isPast = true;

    void Start()
    {
        player = GameObject.Find("Player");
        collider2D = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentX = player.transform.position.x;
        float currentY = player.transform.position.y;
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidbody.MovePosition(new Vector2(currentX, currentY + 1));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rigidbody.MovePosition(new Vector2(currentX, currentY - 1));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rigidbody.MovePosition(new Vector2(currentX - 1, currentY));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidbody.MovePosition(new Vector2(currentX + 1, currentY));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isPast)
            {
                isPast = false;
                Camera.main.transform.Translate(startPositionGap, 0, 0);
                player.transform.Translate(startPositionGap, 0, 0);
            }
            else
            {
                isPast = true;
                Camera.main.transform.Translate(-startPositionGap, 0, 0);
                player.transform.Translate(-startPositionGap, 0, 0);
            }

        }
    }

}
