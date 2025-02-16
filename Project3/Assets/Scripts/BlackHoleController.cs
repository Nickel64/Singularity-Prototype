﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

    public GameObject player;
    public Player playerScript;

    private void Start()
    {
        playerScript = player.GetComponent<Player>(); 
    }

    // Update is called once per frame
    void Update () {

        if (isPlayerInDistance()) {
            playerScript.Damage(playerScript.CurHealth * 2);
        }
	}

    /// <summary>
    /// If the player gets too close to this object, it will destroy them
    /// </summary>
    /// <returns>If the player is in destroying range or not</returns>
    /// 

    bool isPlayerInDistance() {
        if(gameObject != null && player != null)
            return (Vector3.Distance(gameObject.transform.position, player.transform.position)<= 75);
        return false;
    }
}
