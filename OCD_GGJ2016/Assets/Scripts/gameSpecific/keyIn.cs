using UnityEngine;
using System.Collections;

public class keyIn : MonoBehaviour {

    public Transform keyStart;
    public Transform keyFinish;
    public float speed = 1.0f;
    private float _startTime;
    private float _journeyLength;
    public keyturn keyturnScript;
    public Transform keyLock;

	// Use this for initialization
	void Start () {
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(keyStart.position, keyFinish.position);
	
	}
	
	// Update is called once per frame
	void Update () {
        float distCovered = (Time.time - _startTime) * speed;
        float fracJourney = distCovered / _journeyLength;
        transform.position = Vector3.Lerp(keyStart.position, keyFinish.position, fracJourney);

        //When the key gets to its destination, parent it to the lock and parent it to the lock.
        if (transform.position == keyFinish.position)
        {
            transform.parent = keyLock;
            //Adjust position and transform so that it rotates properly.
            transform.localPosition = new Vector3(0, -0.2f, 0);
            keyturnScript.enabled = true;            
        }
	}
}
