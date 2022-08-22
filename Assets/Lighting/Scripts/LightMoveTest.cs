using UnityEngine;

public class LightMoveTest : MonoBehaviour
{
    Rigidbody2D rigidbody;
    GameObject player;
    bool isPast = true;

    void Start()
    {
        player = GameObject.Find("Player");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    
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
    }

}
