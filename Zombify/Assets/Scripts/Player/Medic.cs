using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : PlayerController
{
<<<<<<< HEAD
    public override void Start()
=======
    // Start is called before the first frame update
    void Start()
>>>>>>> refs/remotes/origin/KDCheapCheap/Development
    {
        base.Start();
        playerClass = PlayerClasses.Medic;
        baseSpeed = 8;
    }
}
