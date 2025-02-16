﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float CurHealth = 50;
    public float MaxHealth = 100;
	public float HealFactor = 2;
	
	public static Vector3 PlayerPosition;

    public ParticleSystem ExplodeParticle;
    private bool _exploding = false;
    public bool IsResponsive = true;

    public static int CurrentScore;

    public GameObject GameOverUi;
	public Image HealthBar;
	public Text ScoreText;
	
	
	private void Update ()
	{

        if (_exploding) return;

		PlayerPosition = transform.position;
		HealthBar.fillAmount = CurHealth / MaxHealth;
		ScoreText.text = CurrentScore.ToString();
        if (CurHealth <= 0) {
	        GameOverUi.GetComponent<GameOverController>().gameover();
            StartCoroutine("Kill");
            _exploding = true;
        }
        RegenHealth();
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        var magnitude = GetComponent<Rigidbody>().velocity.magnitude;
        Damage(magnitude * 5);
    }

    public void Damage(float amount) {
        CurHealth -= amount;
    }

    private void RegenHealth() {
        CurHealth += Time.deltaTime * HealFactor;
        if (CurHealth > MaxHealth)
            CurHealth = MaxHealth;
    }

    IEnumerator Kill() {
        
        //trigger an explosion
        var explosion = Instantiate(ExplodeParticle, transform.position, Quaternion.identity);
        explosion.transform.parent = transform;

        IsResponsive = false;

        var totalDuration = explosion.main.duration;
        yield return new WaitForSeconds(totalDuration);

        //destroy the ship
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        //trigger an explosion
        explosion = Instantiate(ExplodeParticle, transform.position, Quaternion.identity);
        explosion.transform.parent = transform;

        totalDuration = explosion.main.duration;
       
        yield return new WaitForSeconds(totalDuration);


        explosion.Stop();
        DestroyImmediate(explosion);
        explosion = null;

        Destroy(gameObject);
        

    }
	
}
