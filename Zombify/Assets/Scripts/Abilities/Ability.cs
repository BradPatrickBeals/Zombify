﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Abilities will be an empty game object with a script attached
 * the script will be a child of this class allowing all the functionality
 * When the player uses the ability, it will instantiate itself and the effect plays automatically
 * Once the effect is done, ability gets destroyed/set inactive
 */

public class Ability : MonoBehaviour
{
    bool isUnlocked;
    int cost;
    PlayerController player;
    Ability[] parents;
    float cooldownTime;

    public virtual void Use() //Handles base functionality for using an ability
    {
        //instantiate object that runs it's own shit? Yeah go on then
        Instantiate(gameObject);
    }

    public bool AttemptBuyAbility()
    {
        int parentCheckCount = 0;

        if (parents.Length != 0) //If it has a parent skill, check if that's unlocked
        {
            foreach (Ability a in parents) //Check if parent abilities are unlocked
            {
                if (a.isUnlocked)
                {
                    parentCheckCount++;
                }
            }

            if (parentCheckCount == parents.Length) //if all are unlocked
            {
                if (player.upgradePoints <= cost)
                {
                    isUnlocked = true;
                    player.upgradePoints -= cost; //Buy ability
                    return true;
                }
            }
            else
            {
                OnBuyFailed(); //Fail
                return false;
            }
        }
        else
        {
            if (player.upgradePoints <= cost)
            {
                isUnlocked = true;
                player.upgradePoints -= cost; //Buy ability
                return true;
            }
            else
            {
                OnBuyFailed();
                return false;
            }
        }
        return false;
    }

    private void OnBuyFailed()
    {
        Debug.LogError("This isn't unlocked yet!"); //Replace this with UI element later
    }
}
