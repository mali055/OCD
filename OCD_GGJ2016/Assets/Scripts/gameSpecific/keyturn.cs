using UnityEngine;
using System.Collections;

public class keyturn : MonoBehaviour {

    //Variables for turning action.
    public bool isdragging;
    public float _sensitivity = 0.4f;
    private Vector3 _mouseStart;
    public Vector3 _mouseOffset;
    public Vector3 _rotation;
    public GameObject keyLock;
    public GameObject key;
    private Vector3 startPosition;
    public float lockRotation;
    public GameObject arrow;
    private Vector3 _arrowRotation;

    //Variables for door locking/unlocking.
    public bool turnRight;
    public int turnCount;
    public int turnsLeft;
    public SpriteRenderer lockWord;
    public SpriteRenderer unlockWord;
    

    private turnDisplay turnDisplayScript;


    void Awake()
    {
        startPosition = transform.position;
        turnDisplayScript = GetComponent<turnDisplay>();

        //Set instructions
        lockWord.enabled = true;
        unlockWord.enabled = false;
    }


	void OnEnable () {

        //Turn on the arrow
        SpriteRenderer arrowSprite = arrow.GetComponent<SpriteRenderer>();
        arrowSprite.enabled = true;

        //Rotate action
        _rotation = Vector3.zero;
        _arrowRotation = Vector3.zero;

        transform.position = startPosition;
        isdragging = false;

        //Turn counting
        turnRight = true;
        turnCount = 0;
        turnsLeft = 5;


	}
	
	// Update is called once per frame
	void Update () {

        lockRotation = transform.localEulerAngles.z;

        //Code to rotate lock after click.
        //If player is clicking and holding and moving...
	    if (isdragging)
        {
            _mouseOffset = (Input.mousePosition - _mouseStart);

            //If player needs to turn lock to the right, only allow movement in that direction.
            if (turnRight && _mouseOffset.x > 0)
            {
                _rotation.z = -_mouseOffset.x * _sensitivity;
            }

            //If player needs to turn lock to the left, only allow movement in that direction.
            if (!turnRight && _mouseOffset.x < 0)
            {
                _rotation.z = -_mouseOffset.x * _sensitivity;
            }

            transform.Rotate(_rotation);

            _mouseStart = Input.mousePosition;

            if (_mouseOffset == Vector3.zero)
                _rotation = Vector3.zero;
        }

        //Unlock/lock code

        if (turnRight)
        {
            if (lockRotation < 40 && lockRotation > 5)
            {
                AddTurnCount();
            }
        }

        if (!turnRight)
        {
            if (lockRotation  > 320)
            {
                AddTurnCount();
            }
        }

        //Give a ceiling to _mouseOffset?


	}

    void OnMouseDown()
    {
      
        _mouseStart = Input.mousePosition;
        _mouseOffset = Vector3.zero;
        isdragging = true;
    }

    void OnMouseUp()
    {
        isdragging = false;
        _rotation.z = 0;
    }

    void AddTurnCount()
    {
        //Stop the mouse turning the lock, change direction, add to turn count.
        isdragging = !isdragging;
        turnCount++;
        turnRight = !turnRight;

        //Swap word instructions.
        lockWord.enabled = !lockWord.enabled;
        unlockWord.enabled = !unlockWord.enabled;

        //Flip the arrow
        _arrowRotation.z = 180;
        arrow.transform.Rotate(_arrowRotation);

        
        if ((turnCount / 2) == 1)
        {
            turnsLeftDisplay();
        }

        //Add audio stuff
    }

    //Update turnsLeft counter and display.
    void turnsLeftDisplay()
    {
        turnsLeft--;
        turnCount = 0;
        turnDisplayScript.changeDisplay(turnsLeft);


    }

    
            

    

}
