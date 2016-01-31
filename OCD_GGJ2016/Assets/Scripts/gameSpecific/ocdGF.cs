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

        public List<string> badThoughts;
        float stress = 0.5f;
        Vector2 sweetSpot = new Vector2(0.4f, 0.6f);
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
            //initGame();
            callMiniGame(GameMode.MiniGameB);

        }

        //GM.Get().scene.miscRefs[0] = "Welcome to Paper Slots"; <-- Example how to reference stuff
        // miscRef are in editor in _SM game object along with button ref for buttons.

        void callMiniGame(GameMode gm)
        {
            switch (gm)
            {
                case GameMode.MiniGameA:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[1].SetActive(true);
                    eye1Open = false;
                    eye2Open = false;
                    break;
                case GameMode.MiniGameB:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[2].SetActive(true);
                    GM.Get().scene.miscRefs[1].SetActive(false);
                    GM.Get().scene.buttonRefs[0].SetActive(false);
                    GM.Get().scene.miscRefs[1].SetActive(false);
                    sfx[1].Play();
                    miniGameCounter = 0;
                    StartCoroutine(tapTableGame());
                    break;
                case GameMode.MiniGameC:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[3].SetActive(true);
                    animateWalk = true;
                    break;
                case GameMode.MiniGameD:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[4].SetActive(true);
                    break;
                case GameMode.MiniGameE:
                    GM.Get().framework.canvases[0].SetActive(false);
                    GM.Get().framework.canvases[5].SetActive(true);
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
            GM.Get().framework.canvases[5].SetActive(false);
            GM.Get().framework.canvases[4].SetActive(false);
            GM.Get().framework.canvases[3].SetActive(false);
            GM.Get().framework.canvases[2].SetActive(false);
            GM.Get().framework.canvases[1].SetActive(false);
            GM.Get().framework.canvases[0].SetActive(true);
        }
        void Update()
        {
            if (animateWalk)
            {
                GM.Get().scene.miscRefs[4].transform.Translate(Vector3.left * 0.5f);
            }
        }

        void initGame()
        {

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
                    //swap eye sprites
                    if (!eye1Open)
                    {
                        eye1Open = !eye1Open;
                        if (eye2Open)
                        {
                            //Do something maybe
                            endMiniGame();
                        }
                    }
                    break;
                case 2: //minigame2 button1
                    sfx[2].Play();
                    if (!eye2Open)
                    {
                        eye2Open = !eye2Open;
                        if (eye1Open)
                        {
                            //Do something maybe
                            endMiniGame();
                        }
                    }
                    break;
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
