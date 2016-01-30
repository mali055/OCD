using UnityEngine;
using Meshadieme;
using System.Collections;

public class mouseInput : MonoBehaviour {

    void OnMouseDown()
    {
        //Debug.Log("OnMouseDown = " + Input.mousePosition);
        GM.Get().framework.mouseEvent(0, Input.mousePosition);



    }
    void OnMouseDrag()
    {
        //Debug.Log("OnMouseDown = " + Input.mousePosition);
        GM.Get().framework.mouseEvent(1, Input.mousePosition);



    }
    void OnMouseUp()
    {
        //Debug.Log("OnMouseDown = " + Input.mousePosition);
        GM.Get().framework.mouseEvent(2, Input.mousePosition);



    }
}
