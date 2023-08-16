using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMovement : MonoBehaviour
{
    [SerializeField] private bool movable, rotatable;
    [SerializeField] private GearController attachedController;
    [SerializeField] private LayerMask boardLayer;

    private float timer;

    private Rigidbody2D rb;

    private Vector2 offset;

    private Vector2 initialPos;

    private BoardManager bm;

    public bool Movable
    {
        get => movable;
        set => movable = value;
    }

    public bool Rotatable
    {
        get => rotatable;
        set => rotatable = value;
    }

    private void Awake()
    {
        GetComponent<CompositeCollider2D>().layerOverridePriority = 10;
        rb = GetComponent<Rigidbody2D>();
        bm = GameObject.FindWithTag("BoardController").GetComponent<BoardManager>();
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
            Mathf.Infinity, boardLayer);
        if (hit)
        {
            hit.transform.GetComponent<TileHoldChecker>().occupied = true;
            rb.position = hit.transform.position;
        }
        
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            if (child.transform.name == "Circle")
            {
                child.color = rotatable ? Color.white : Color.black;
            }
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log($"{transform} was clicked");
        initialPos = rb.position;
        if (movable)
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
                Mathf.Infinity, boardLayer);
            if (hit)
            {
                hit.transform.GetComponent<TileHoldChecker>().occupied = false;
            }
            offset = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
        }

        if (rotatable)
        {
            timer = Time.time;
        }
    }

    private void OnMouseDrag()
    {
        if (movable)
        {
            rb.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
        }
    }

    private void OnMouseUp()
    {
        if (movable)
        {
            //check if the gear is out of the bounds of the map
            if (rb.position.y is >= 5f or <= -5f || rb.position.x is >= 8.8f or <= -8.8f)
            {
                rb.position = initialPos;
            }
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.zero,
                Mathf.Infinity, boardLayer);
            if (hit)
            {
                if (!hit.transform.GetComponent<TileHoldChecker>().occupied)
                {
                    rb.position = hit.collider.transform.position;
                    hit.transform.GetComponent<TileHoldChecker>().occupied = true;
                }
                else
                {
                    rb.position = initialPos;
                    RaycastHit2D returnHit = Physics2D.Raycast(initialPos, Vector2.zero,
                        Mathf.Infinity, boardLayer);
                    if (returnHit)
                    {
                        returnHit.transform.GetComponent<TileHoldChecker>().occupied = true;
                    }
                }
            }
        }

        if (rotatable)
        {
            if (Time.time - timer <= 0.3f && Vector2.Distance(rb.position, initialPos) < 0.01f)
            {
                attachedController.RotateGear(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < rb.position.x); 
                bm.IncrementCounter();
            }
        }

        if (movable || rotatable)
        {
            if (attachedController.ThisBoardManager.CheckAllOccupied())
            {
                attachedController.ThisBoardManager.EndLevel();
            }
        }
    }
}
