using Meshadieme;
using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == GM.Get().scene.miscRefs[5])
        {
            GM.Get().scene.miscRefs[2].SetActive(true);
        }
        if (col.gameObject == GM.Get().scene.miscRefs[6])
        {
            GM.Get().scene.miscRefs[3].SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == GM.Get().scene.miscRefs[5])
        {
            GM.Get().framework.disableLamp(1);
        }
        if (col.gameObject == GM.Get().scene.miscRefs[6])
        {
            GM.Get().framework.disableLamp(2);
        }
    }

}
