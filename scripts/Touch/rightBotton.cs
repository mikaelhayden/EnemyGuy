using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rightBotton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isRight;
    private Player player;
    private leftBotton left;

    public float movement;
    public float speed = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        left = GameObject.FindGameObjectWithTag("left").GetComponent<leftBotton>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) == true && isRight == true && left.isLeft == false)
        {
            movement += Time.deltaTime * speed;

            if(movement > 1)
            {
                movement = 1f;
            }

            player.movement = movement;
        }

        if (Input.GetMouseButton(0) == false && isRight== false && left.isLeft == false)
        {
            movement = movement - (Time.deltaTime * speed);

            if (movement < 0)
            {
                movement = 0f;
            }

            player.movement = movement;
        }
    }

    //É chamado quando clicamos(ou tocamos) no elementos de UI
    public void OnPointerDown(PointerEventData eventData)
    {
        isRight = true;
    }

    //É chamado quando soltamos no elementos de UI
    public void OnPointerUp(PointerEventData eventData)
    {
        isRight = false;
    }
}
