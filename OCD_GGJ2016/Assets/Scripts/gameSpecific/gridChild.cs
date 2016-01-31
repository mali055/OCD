using UnityEngine;
using System.Collections;

public class gridChild : MonoBehaviour {
    public int gridHeight = 0;
    public int gridWidth = 0;
    
	void Awake ()
    {
        gridHeight = (int)Mathf.Ceil(this.GetComponent<RectTransform>().localScale.y / 64);
        gridWidth = (int)Mathf.Ceil(this.GetComponent<RectTransform>().localScale.x / 64);

    }
	
}
