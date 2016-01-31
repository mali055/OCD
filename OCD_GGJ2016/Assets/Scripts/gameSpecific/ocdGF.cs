//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.1.0
/// </version>
/// <summary>
/// A Parent Class for most of our scripts,
/// To make it easier to manage debugging code and modify debug output as needed.
/// Aswell as avoiding debug messages is a non-development build.
/// </summary>
/// CHANGELOG: 
///	*	0.1.0: C# Game Template Base
/// TODO: Consolidate Debug Toggles in Editor for easiers access (C#)
/// 

using Meshadieme;
using Meshadieme.Math;
using Meshadieme.Pathfinding;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Meshadieme
{
    /*
    Sequence of events - todo list
    */

    public enum GameMode
    {
        Menu = 0,
        Home = 1,
        MiniGameA = 2,
        MiniGameB = 3,
        MiniGameC = 4,
        MiniGameD = 5,
        MiniGameE = 6,
    }

    public class ocdGF : gameFramework
    {

        public GameMode gMode;
        public GameMode defMode = GameMode.Home;
        public int mini = 0;
        public List<string> badThoughts;
        public float stress = 0.5f;
        public Vector2 sweetSpot = new Vector2(0.45f, 0.55f);
        public Vector2 map = new Vector2(20, 12);
        public int tileSize = 64;
        public GameObject player;
        public GameObject currentCanvas;
        public List<GameObject> canvases;
        public Camera cam;
        public AStar pathing = new AStar(pType.TileBased);
        public AudioSource[] sfx;
        int miniGameCounter = 0;
        int miniGameCounter2 = 0;
        bool eye2Open = false;
        bool eye1Open = false;
        bool animateWalk = false;
        bool lamp1 = false;
        bool lamp2 = false;
        int lampCount = 0;
        int lampCount2 = 0;
        GameObject popup;
        List<Vector3> popPos = new List<Vector3>();


        protected override void Awake()
        {
            //Debug.Log("GF_Awake()");
            currentCanvas.BroadcastMessage("setPlayer");
            cam = Camera.main;
            sfx = cam.GetComponents<AudioSource>();
        }
        
        public void loadSelectedGame()
        {
            Debug.Log("GM.framework Loading");
            initGame();
            //callMiniGame(GameMode.MiniGameC);

        }

        //GM.Get().scene.miscRefs[0] = "Welcome to Paper Slots"; <-- Example how to reference stuff
        // miscRef are in editor in _SM game object along with button ref for buttons.

        void callMiniGame(GameMode gm)
        {
            StopCoroutine(popupTime());
            switch (gm)
            {
                case GameMode.MiniGameA:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[1].SetActive(true);
                    eye1Open = false;
                    eye2Open = false;
                    gMode = GameMode.MiniGameA;
                    break;
                case GameMode.MiniGameB:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[2].SetActive(true);
                    GM.Get().scene.miscRefs[1].SetActive(false);
                    GM.Get().scene.buttonRefs[0].SetActive(false);
                    GM.Get().scene.miscRefs[1].SetActive(false);
                    sfx[0].Play();
                    miniGameCounter = 0;
                    StartCoroutine(tapTableGame());
                    gMode = GameMode.MiniGameB;
                    break;
                case GameMode.MiniGameC:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[3].SetActive(true);
                    GM.Get().scene.miscRefs[2].SetActive(false);
                    GM.Get().scene.miscRefs[3].SetActive(false);
                    animateWalk = true;
                    lamp1 = false;
                    lamp2 = false;
                    lampCount = 0;
                    lampCount2 = 0;
                    gMode = GameMode.MiniGameC;
                    break;
                case GameMode.MiniGameD:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[4].SetActive(true);
                    gMode = GameMode.MiniGameD;
                    break;
                case GameMode.MiniGameE:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[5].SetActive(true);
                    gMode = GameMode.MiniGameE;
                    break;
            }
            return;
        }
        
        IEnumerator tapTableGame ()
        {
            yield return new WaitForSeconds(4);
            GM.Get().scene.miscRefs[1].SetActive(true);
            GM.Get().scene.buttonRefs[0].SetActive(true);
            GM.Get().scene.miscRefs[1].SetActive(true);
            //Animate finger here
            yield return null;
        }

        //call this at the end of your mini game to take the results
        public void endMiniGame()
        {
            Debug.Log("End Mini");
            if (gMode == GameMode.MiniGameC)
            {
                SceneManager.LoadScene(1);
                return;
            }
            StartCoroutine(popupTime());
            GM.Get().framework.canvases[5].SetActive(false);
            GM.Get().framework.canvases[4].SetActive(false);
            GM.Get().framework.canvases[3].SetActive(false);
            GM.Get().framework.canvases[2].SetActive(false);
            GM.Get().framework.canvases[1].SetActive(false);
            GM.Get().framework.canvases[0].SetActive(true);
            gMode = GameMode.Home;
        }
        void Update()
        {
            if (animateWalk)
            {
                GM.Get().scene.miscRefs[4].transform.Translate(Vector3.left * 2f);
            }
        }

        void initGame()
        {
            cam.GetComponent<stressScript>().startStress();
            GM.Get().scene.miscRefs[7].SetActive(false);
            GM.Get().scene.miscRefs[8].SetActive(false);
            GM.Get().scene.miscRefs[9].SetActive(false);
            mini = 0;
            StartCoroutine(popupTime());
        }

        IEnumerator popupTime()
        {
            if (gMode == GameMode.Home)
            {
                mini++;
                switch (mini)
                {
                    case 1:
                        yield return new WaitForSeconds(2);
                        GM.Get().scene.miscRefs[7].SetActive(true);
                        yield return new WaitForSeconds(3); //wait for popup
                        GM.Get().scene.miscRefs[7].SetActive(false);
                        stress -= 0.1f;
                        StartCoroutine(popupTime());
                        break;
                    case 2:
                        yield return new WaitForSeconds(2);
                        GM.Get().scene.miscRefs[8].SetActive(true);
                        yield return new WaitForSeconds(3); //wait for popup
                        GM.Get().scene.miscRefs[8].SetActive(false);
                        stress -= 0.1f;
                        StartCoroutine(popupTime());
                        break;
                    case 3:
                        yield return new WaitForSeconds(2);
                        GM.Get().scene.miscRefs[9].SetActive(true);
                        yield return new WaitForSeconds(3); //wait for popup
                        GM.Get().scene.miscRefs[9].SetActive(false);
                        stress -= 0.1f;
                        mini = 0;
                        StartCoroutine(popupTime());
                        break;
                        //case 4:
                        //    yield return new WaitForSeconds(2);
                        //    GM.Get().scene.miscRefs[7].SetActive(true);
                        //    yield return new WaitForSeconds(3); //wait for popup
                        //    GM.Get().scene.miscRefs[7].SetActive(false);
                        //    break;
                }
            }
            yield return null;
        }

        public void procCmds(int buttonIndex)
        {
            Debug.Log(buttonIndex);
            switch (buttonIndex)
            {
                case 0: //minigame1 button
                    miniGameCounter++;
                    if (miniGameCounter < 3)
                    {
                        sfx[0].Play();
                        stress -= 0.05f;
                        //Do something here then
                    }
                    else
                    {
                        miniGameCounter = 0;
                        miniGameCounter2++;
                        GM.Get().scene.miscRefs[1].SetActive(false);
                        GM.Get().scene.buttonRefs[0].SetActive(false);
                        GM.Get().scene.miscRefs[1].SetActive(false);
                        StartCoroutine(tapTableGame());
                        if (miniGameCounter2 == 3)
                        {
                            //Maybe do something here
                            endMiniGame();
                        }
                    }
                    break;
                case 1: //minigame2 button1
                    sfx[2].Play();
                    Debug.Log("Eye");
                    //swap eye sprites
                    if (!eye1Open)
                    {
                        eye1Open = !eye1Open;
                        stress -= 0.05f;
                        if (eye2Open)
                        {
                            //Do something maybe
                            endMiniGame();
                        }
                    }
                    break;
                case 2: //minigame2 button1
                    sfx[2].Play();
                    Debug.Log("Eye");
                    if (!eye2Open)
                    {
                        eye2Open = !eye2Open;
                        stress -= 0.05f;
                        if (eye1Open)
                        {
                            //Do something maybe
                            endMiniGame();
                        }
                    }
                    break;
                case 3: //minigame3 button1
                    sfx[3].Play();
                    if (!lamp1)
                    {
                        Debug.Log("Lamp Tap");
                        lampCount++;
                        stress -= 0.05f;
                        if (lampCount == 3)
                        {
                            lamp1 = true;
                            disableLamp(1);
                        }
                    }
                    break;
                case 4: //minigame3 button1
                    sfx[3].Play();
                    if (!lamp2)
                    {
                        Debug.Log("Lamp Tap");
                        lampCount2++;
                        stress -= 0.05f;
                        if (lampCount2 == 3)
                        {
                            lamp2 = true;
                            disableLamp(2);
                        }
                    }
                    break;
                case 5: //button1
                    callMiniGame(GameMode.MiniGameA);
                    break;
                case 6: //button2
                    callMiniGame(GameMode.MiniGameB);
                    break;
                case 7: //button3
                    callMiniGame(GameMode.MiniGameC);
                    break;
            }

        }

        public void disableLamp(int lamp)
        {
            if ( lamp == 1 )
            {
                GM.Get().scene.miscRefs[2].SetActive(false);
                endMiniGame();
            } else
            {
                GM.Get().scene.miscRefs[3].SetActive(false);
            }
        }

        public void mouseEvent(int type, Vector2 pos)
        {
            switch (type)
            {
                case 0: //Mouse Down
                    Debug.Log(" Go To " + GM.Get().scene.miscRefs[0].GetComponent<gridParent>().pointToTile(pos));
                    player.GetComponent<playerScript>().setPlayer();
                    pathing.goToPos( new Vector2(player.GetComponent<playerScript>().xPos, player.GetComponent<playerScript>().yPos) , GM.Get().scene.miscRefs[0].GetComponent<gridParent>().pointToTile(pos));
                    break;
                case 1: //Mouse Drag
                    break;
                case 2: //Mouse Up
                    break;
            }
        }
    }
}
