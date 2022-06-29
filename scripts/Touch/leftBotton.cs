using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class leftBotton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{  
    public bool isLeft;

    private Player player;
    private rightBotton right;

    public float movement;
    public float speed = 3f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        right = GameObject.FindGameObjectWithTag("right").GetComponent<rightBotton>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0) == true && isLeft == true && right.isRight == false)
        {
            movement = movement - (Time.deltaTime * speed);

            if (movement < -1)
            {
                movement = -1f;
            }

            player.movement = movement;
        }

        if (isLeft == false)
        {
            movement += Time.deltaTime * speed;
      
            if (movement > 0)
            {
                movement = 0f;
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isLeft = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isLeft = false;

    }

}
