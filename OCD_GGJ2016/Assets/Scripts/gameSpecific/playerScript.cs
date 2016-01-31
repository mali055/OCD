using Meshadieme;
using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    public int xPos = 0;
    public int yPos = 0;

	public void setPlayer () {
        GM.Get().framework.player = this.gameObject;
        Vector2 temp = GM.Get().scene.miscRefs[0].GetComponent<gridParent>().pointToTile(this.GetComponent<RectTransform>().position);
        xPos = (int) temp.x;
        yPos = (int) temp.y;
	}
}
