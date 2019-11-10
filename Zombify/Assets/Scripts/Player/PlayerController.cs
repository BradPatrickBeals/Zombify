﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public enum PlayerClasses
    {
        Soldier,
        Medic,
        Scout,
        Engineer
    }

    public enum PlayerNumber
    {
        Player1,
        Player2,
        Player3,
        Player4
    }

    public float baseSpeed;
    public int health;
    [HideInInspector]public PlayerClasses playerClass;
    public PlayerNumber playerNumber;

    public int upgradePoints = 0;
    [SerializeField] private Ability[] skillTree;
    private GameObject equippedAbility;

    public GameObject[] Weapons = new GameObject[4];
    //doin
    #region Sprinting Vars
    private float currentStamina = 3.0f;
    private float MaxStamina = 3.0f;
    bool isSprinting = false;
    //---------------------------------------------------------
    private float StaminaRegenTimer = 0.0f;
    //---------------------------------------------------------
    private const float StaminaDecreasePerFrame = 1.0f;
    private const float StaminaIncreasePerFrame = 0.5f;
    private const float StaminaTimeToRegen = 1.5f;
    #endregion

    [SerializeField]private GameObject gunSpawn;

    private bool hasWeaponEquipped;
    private Weapon currentWeapon;
    private float currentSpeed;
    private float sprintSpeed;

    private TMP_Text currentBulletCount;
    private TMP_Text totalAmmo;

    private Vector2 lookAtPoint;

    private Vector2 lookDeadzone
    {
        get
        {
            float x = transform.position.x + 5f;
            float y = transform.position.y + 5f;

            return new Vector2(x, y);
        }
    } 

    private Vector2 GetLookAtPoint()
    {
        lookAtPoint = transform.position + new Vector3(InputManager.inputInstance.lookAtOffset.x * 10f, InputManager.inputInstance.lookAtOffset.y * 10f);

        return lookAtPoint;
    }

    public virtual void Start()
    {
        currentSpeed = baseSpeed;
        currentWeapon = Weapons[0].GetComponent<Weapon>();
        GetUI();
    }

    public virtual void Update()
    {
        LookAtStick();
        Movement();
        Sprint();
        Shoot();

        currentBulletCount.text = currentWeapon.currentBulletCount.ToString();
        totalAmmo.text = currentWeapon.totalAmmo.ToString();

        if (!hasWeaponEquipped)
        {
            GameObject newWeapon = Instantiate(currentWeapon.gameObject, gunSpawn.transform.position, gunSpawn.transform.rotation, gunSpawn.transform);
            currentWeapon = newWeapon.GetComponent<Weapon>();
            hasWeaponEquipped = true;
        }

        if (InputManager.inputInstance.onRBPressed)
        {
            Debug.Log("RB reload");
            currentWeapon.isReloading = true;
            StartCoroutine(currentWeapon.Reload(currentWeapon.reloadSpeed));
        }
        Debug.Log(GetLookAtPoint());
    }

    public void Sprint()
    {
        sprintSpeed = currentSpeed + (currentSpeed / 10) * 2;

        if (InputManager.inputInstance.isLTPressed)
        {
            if (!isSprinting && currentStamina != 0)
            {
                currentSpeed += sprintSpeed;
                isSprinting = true;
            }

            if (currentStamina <= 0)
            {
                currentSpeed = baseSpeed;
                isSprinting = false;
            }

            currentStamina = Mathf.Clamp(currentStamina - (StaminaDecreasePerFrame * Time.deltaTime), 0.0f, MaxStamina);
            StaminaRegenTimer = 0.0f;
        }
        else
        {
            if (currentStamina < MaxStamina)
            {
                if (StaminaRegenTimer >= StaminaTimeToRegen)
                    currentStamina = Mathf.Clamp(currentStamina + (StaminaIncreasePerFrame * Time.deltaTime), 0.0f, MaxStamina);
                else
                    StaminaRegenTimer += Time.deltaTime;
            }

            isSprinting = false;
            currentSpeed = baseSpeed;
        }
    }

    public void Shoot()
    {
        if (Input.GetMouseButton(0) || InputManager.inputInstance.isRTPressed)
        {
            currentWeapon.Shoot();
        }
    }

    public void LookAtStick()
    {
        Vector2 dir = (Vector2)transform.position - GetLookAtPoint();/*Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)*/;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.back);

        WeaponLookAtStick(currentWeapon);
    }

    private void WeaponLookAtStick(Weapon currentWeapon)
    {
        float dist = Vector2.Distance(transform.position, GetLookAtPoint()); //Stops the 

        if (dist >= 5f)
        {
            Vector2 dir = (Vector2)currentWeapon.transform.position - GetLookAtPoint();
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            currentWeapon.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.back);
        }
    }

    public void Movement()
    {
        Vector2 m = new Vector2(InputManager.inputInstance.movementVector.x, InputManager.inputInstance.movementVector.y) * Time.deltaTime * currentSpeed;
        Debug.Log(m);
        //transform.Translate(xMovement * Time.deltaTime * currentSpeed, yMovement * Time.deltaTime * currentSpeed, 0, Space.World);
        transform.Translate(m, Space.World);
    }

    public void ChooseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //Open weapon wheel popup
            //Weapon wheel popup handles the rest
        }
    }

    public void UseAbility(Ability ability)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ability.Use();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(lookAtPoint, .2f);
    }

   private void GetUI()
   {
        if (playerNumber == PlayerNumber.Player1)
        {
            currentBulletCount = GameObject.FindGameObjectWithTag("Player1CBC").GetComponent<TMP_Text>();
            totalAmmo = GameObject.FindGameObjectWithTag("Player1TA").GetComponent<TMP_Text>();
        }
        else if (playerNumber == PlayerNumber.Player2)
        {
            currentBulletCount = GameObject.FindGameObjectWithTag("Player2CBC").GetComponent<TMP_Text>();
            totalAmmo = GameObject.FindGameObjectWithTag("Player2TA").GetComponent<TMP_Text>();
        }
        else if (playerNumber == PlayerNumber.Player3)
        {
            currentBulletCount = GameObject.FindGameObjectWithTag("Player3CBC").GetComponent<TMP_Text>();
            totalAmmo = GameObject.FindGameObjectWithTag("Player3TA").GetComponent<TMP_Text>();
        }
        else if (playerNumber == PlayerNumber.Player4)
        {
            currentBulletCount = GameObject.FindGameObjectWithTag("Player4CBC").GetComponent<TMP_Text>();
            totalAmmo = GameObject.FindGameObjectWithTag("Player4TA").GetComponent<TMP_Text>();
        }

    }
}
