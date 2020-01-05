using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    [SerializeField] float healthDecay = .001f;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Canvas gamePauseCanvas;
    public bool pauseBool = false;

    private void Start()
    {
        gamePauseCanvas.enabled = false;
    }

    public void GetDamage(float damage)
    {
        
        healthPoints -= damage;
        
        if (healthPoints <= 0)
        {
            pauseBool = true;
            GetComponent<DeathHandler>().HandleDeath();           
        }
    }


    private void Update()
    {
        if (pauseBool == false)
        {
            Pause();
        }
        healthPoints -= healthDecay * Time.deltaTime;
        DisplayHealth();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1.0)
            {
                Time.timeScale = 0;
                gamePauseCanvas.enabled = true;
                FindObjectOfType<WeaponSwitcher>().enabled = false;
                FindObjectOfType<WeaponScript>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1;
                gamePauseCanvas.enabled = false;
                FindObjectOfType<WeaponSwitcher>().enabled = true;
                FindObjectOfType<WeaponScript>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void DisplayHealth()
    {
        healthText.text = "Health " + Math.Round(healthPoints).ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            pauseBool = true;
        }
    }
}
