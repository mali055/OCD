using Meshadieme;
using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
    
	void setPlayer () {
        GM.Get().framework.player = this.gameObject;
	}
}
