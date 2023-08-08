using System.Collections;
using System.Collections.Generic;
using Resources.Enums;
using Resources.ScriptableObjects;
using UnityEngine;

public class GearController : MonoBehaviour
{
    [SerializeField] private GameObject topL, topR, botL, botR;

    [Header("Color Choices")] 
    
    [SerializeField] private PossibleColor topLeftColor;

    [SerializeField] private PossibleColor topRightColor, bottomLeftColor, bottomRightColor;

    public GameObject TopL
    {
        get => topL;
        set => topL = value;
    }

    public GameObject TopR
    {
        get => topR;
        set => topR = value;
    }

    public GameObject BotL
    {
        get => botL;
        set => botL = value;
    }

    public GameObject BotR
    {
        get => botR;
        set => botR = value;
    }

    private void RenderGear()
    {
        
    }

    public static Color DecodeColor(PossibleColor inputChoice)
    {
        Color output = Color.clear;
        switch (inputChoice)
        {
            case PossibleColor.Red :
                output = Color.red;
                break;
            case PossibleColor.Blue :
                output = Color.blue;
                break;
            case PossibleColor.Black :
                output = Color.black;
                break;
            case PossibleColor.Green :
                output = Color.green;
                break;
            /*case PossibleColor.Orange :
                output = new Color32(0xF5,0xb3,0x42);*/
        }
        return output;
    }
}
