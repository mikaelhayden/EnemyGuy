using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftRight : MonoBehaviour
{
    //public bool isRight;
    private Player player;

    public float movement;
    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void right()
    {
        movement += Time.deltaTime * speed;

        if (movement > 1)
        {
            movement = 1f;
        }

        player.movement = movement;
    }

    public void stopRight()
    {
        movement -= Time.deltaTime * speed;

        if (movement < 0)
        {
            movement = 0f;
        }

        player.movement = movement;
    }

    public void left()
    {
        movement -= Time.deltaTime * speed;

        if (movement < -1)
        {
            movement = -1f;
        }

        player.movement = movement;
    }

    public void stopLeft()
    {
        movement += Time.deltaTime * speed;

        if (movement > 0)
        {
            movement = 0f;
        }

        player.movement = movement;
    }
}
