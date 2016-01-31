using Meshadieme;
using Meshadieme.Math;
using UnityEngine;
using System.Collections;

public class stressScript : MonoBehaviour {

    bool start = false;
    int[] dirshuffle = new int[] { 1, 2, 3, 4 };
    shuffleBag shuffle;
    

    void Awake ()
    {
        start = false;
        shuffle = new shuffleBag(1, dirshuffle);
    }
    // Use this for initialization
    public void startStress()
    {
        start = true;
    }

    // Use this for initialization
    public void stopStress()
    {
        start = false;
    }

    // Update is called once per frame
    void Update () {
        Vector2 level = GM.Get().framework.sweetSpot;
        GM.Get().framework.stress += 0.0001f;
        if (GM.Get().framework.stress > 1) GM.Get().framework.stress = 1.0f;
        float stress = GM.Get().framework.stress;

        if (GM.Get().framework.stress > level.x && GM.Get().framework.stress > level.y)
        {
            //start = false;
        } else
        {
            start = true;
        }

	    if (start)
        {
            int dir = shuffle.Next();
            switch (dir)
            {
                case 1:
                    this.transform.Translate(Vector3.up * Mathf.Abs(stress - 0.5f) * 30);
                    break;
                case 2:
                    this.transform.Translate(Vector3.down * Mathf.Abs(stress - 0.5f) * 30);
                    break;
                case 3:
                    this.transform.Translate(Vector3.left * Mathf.Abs(stress - 0.5f) * 30);
                    break;
                case 4:
                    this.transform.Translate(Vector3.right * Mathf.Abs(stress - 0.5f) * 30);
                    break;
            }
        }
	}
}
