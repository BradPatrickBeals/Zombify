using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : PlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerClass = PlayerClasses.Medic;
        baseSpeed = 8;
    }
}
