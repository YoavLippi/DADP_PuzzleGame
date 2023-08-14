using System.Collections;
using Resources.Enums;
using UnityEngine;

public class GearController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer topL, topR, botL, botR;

    [Header("Initial Color Choices")]
    [Header("Ignore once gameplay has started")]

    [SerializeField] private PossibleColor topLeftColor;

    [SerializeField] private PossibleColor topRightColor, bottomRightColor, bottomLeftColor;
    
    [Header("CurrentColors")]
    
    //We say from top left going clockwise will be our notation for this array ie. CW is +ve
    [SerializeField] private Color[] colorTracker;
    
    //array to change the selected choices to avoid confusion, same convention as the one above
    [SerializeField] private PossibleColor[] choiceTracker;

    public SpriteRenderer TopL
    {
        get => topL;
        set => topL = value;
    }

    public SpriteRenderer TopR
    {
        get => topR;
        set => topR = value;
    }

    public SpriteRenderer BotL
    {
        get => botL;
        set => botL = value;
    }

    public SpriteRenderer BotR
    {
        get => botR;
        set => botR = value;
    }

    private void Awake()
    {
        //Setting scale to match the board
        GameObject boardController = GameObject.FindWithTag("BoardController");
        if (boardController.GetComponent<BoardManager>())
        {
            transform.localScale = transform.localScale * boardController.GetComponent<BoardManager>().PrefabScale;
        }
        
        colorTracker = new Color[]
        {
            DecodeColor(topLeftColor), DecodeColor(topRightColor), DecodeColor(bottomRightColor),
            DecodeColor(bottomLeftColor)
        };

        choiceTracker = new PossibleColor[] { topLeftColor, topRightColor, bottomRightColor, bottomLeftColor };

        //put into a separate function to avoid ArgumentNullException
        StartCoroutine(setColors());
    }

    private IEnumerator setColors()
    {
        yield return new WaitForSeconds(0.001f);
        topL.color = colorTracker[0];
        topR.color = colorTracker[1];
        botR.color = colorTracker[2];
        botL.color = colorTracker[3];
    }

    public void RotateGear(bool rotateLeft)
    {
        if (rotateLeft)
        {
            //Delete this next line when the animation is in place
            transform.Rotate(new Vector3(0,0,90));
            //we rotate CCW, so that means everything has to rotate CW to ensure top left is still in the top left position

            Color temp = colorTracker[0];
            PossibleColor temp2 = choiceTracker[0];
            for (int i = 0; i < 3; i++)
            {
                colorTracker[i] = colorTracker[i + 1];
                choiceTracker[i] = choiceTracker[i + 1];
            }

            colorTracker[3] = temp;
            choiceTracker[3] = temp2;
        }
        else
        {
            //Delete this next line when the animation is in place
            transform.Rotate(new Vector3(0,0,-90));
            //this is vice versa, we rotate everything CW, so the array rotates CCW
            Color temp = colorTracker[3];
            PossibleColor temp2 = choiceTracker[3];
            for (int i = 3; i > 0; i--)
            {
                colorTracker[i] = colorTracker[i - 1];
                choiceTracker[i] = choiceTracker[i - 1];
            }

            colorTracker[0] = temp;
            choiceTracker[0] = temp2;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateGear(false);
        }
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
            case PossibleColor.Orange :
                output = new Color32(0xF5,0xb3,0x42, 0xFF);
                break;
            case PossibleColor.Pink :
                output = new Color32(0xDE, 0x50, 0xD7, 0xFF);
                break;
            case PossibleColor.Purple :
                output = new Color32(0x8A, 0x35, 0xB5, 0xFF);
                break;
            case PossibleColor.Yellow :
                output = Color.yellow;
                break;
            case PossibleColor.White :
                output = Color.white;
                break;
        }
        return output;
    }
}
