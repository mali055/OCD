using Meshadieme;
using UnityEngine;
using System.Collections;

public class buttonScript : MonoBehaviour {


	void OnMouseDown () {
        Debug.Log("buttonScript ");
        GM.Get().scene.inputProcessing(this.gameObject);
	}

}
