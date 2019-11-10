using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : PlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerClass = PlayerClasses.Scout;
        baseSpeed = 9;
    }
}
