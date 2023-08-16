using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeValidator : MonoBehaviour
{
    //Serialized for debugging, nothing else
    [SerializeField] private float nodeDistance;
    [SerializeField] private LayerMask gearLayer;

    public float NodeDistance
    {
        get => nodeDistance;
        set => nodeDistance = value;
    }
    public bool ValidateJunction()
    {
        Vector2 nodePos = transform.position;
        Vector2[] rayPosArr = raycastPositions(nodePos);
        RaycastHit2D tLHit = Physics2D.Raycast(rayPosArr[0], Vector2.zero,
            Mathf.Infinity, gearLayer);
        RaycastHit2D tRHit = Physics2D.Raycast(rayPosArr[1], Vector2.zero,
            Mathf.Infinity, gearLayer);
        RaycastHit2D bRHit = Physics2D.Raycast(rayPosArr[2], Vector2.zero,
            Mathf.Infinity, gearLayer);
        RaycastHit2D bLHit = Physics2D.Raycast(rayPosArr[3], Vector2.zero,
            Mathf.Infinity, gearLayer);

        Color[] hitColors = { Color.gray,Color.gray,Color.gray,Color.gray };
        Color checkColor = Color.clear;
        
        if (tLHit)
        {
            hitColors[0] = tLHit.transform.GetComponent<GearController>().ColorTracker[2];
            checkColor = hitColors[0];
        }

        if (tRHit)
        {
            hitColors[1] = tRHit.transform.GetComponent<GearController>().ColorTracker[3];
            checkColor = hitColors[1];
        }

        if (bRHit)
        {
            hitColors[2] = bRHit.transform.GetComponent<GearController>().ColorTracker[0];
            checkColor = hitColors[2];
        }

        if (bLHit)
        {
            hitColors[3] = bLHit.transform.GetComponent<GearController>().ColorTracker[1];
            checkColor = hitColors[3];
        }

        bool validFlag = true;
        if (checkColor != Color.clear)
        {
            for (int i = 0; i < 4; i++)
            {
                if (hitColors[i] != checkColor && hitColors[i] != Color.gray)
                {
                    validFlag = false;
                    break;
                }
            }  
        }
        else
        {
            validFlag = false;
        }

        return validFlag;
    }

    private Vector2[] raycastPositions(Vector2 basePos)
    {
        float baseX = basePos.x, baseY = basePos.y;
        Vector2[] output = new Vector2[4];
        //-+ ++ +- --
        //Dividing so that the raycast doesn't hit a theoretical corner that shouldn't be there
        float divFactor = 2f;
        output[0] = (new Vector2(baseX - nodeDistance, baseY + nodeDistance) - basePos)/divFactor + basePos;
        output[1] = (new Vector2(baseX + nodeDistance, baseY + nodeDistance) - basePos)/divFactor + basePos;
        output[2] = (new Vector2(baseX + nodeDistance, baseY - nodeDistance) - basePos)/divFactor + basePos;
        output[3] = (new Vector2(baseX - nodeDistance, baseY - nodeDistance) - basePos)/divFactor + basePos;
        return output;
    }
}
