using UnityEngine;
using System.Collections;

public class turnDisplay : MonoBehaviour {

    public GameObject turnCounter;

    public Sprite number0;
    public Sprite number1;
    public Sprite number2;
    public Sprite number3;
    public Sprite number4;
    public Sprite number5;

    private SpriteRenderer turnRenderer;
    public SpriteRenderer okWord;

    void Awake()
    {
        okWord.enabled = false;
    }

	// Use this for initialization
	void Start () {
        turnRenderer = turnCounter.GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeDisplay(int tempNumber)
    {
        switch (tempNumber)
        {
            case 0:
                turnRenderer.sprite = number0;
                okWord.enabled = true;
                Debug.Log("Boom Shack A Lack");
                break;
            case 1:
                turnRenderer.sprite = number1;
                break; 
            case 2:
                turnRenderer.sprite = number2;
                break;
            case 3:
                turnRenderer.sprite = number3;
                break; 
            case 4:
                turnRenderer.sprite = number4;
                break;                 
            case 5:
                turnRenderer.sprite = number5;
                break; 
            default:
                {
                    Debug.Log("Default. Shouldn't get here!");
                break; 
                }
        
        }
    }




}
