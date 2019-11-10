using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : PlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerClass = PlayerClasses.Engineer;
        baseSpeed = 5;
    }
}
