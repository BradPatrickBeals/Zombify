using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : PlayerController
{
<<<<<<< HEAD
    public override void Start()
    {
        base.Start();
        playerClass = PlayerClasses.Scout;
        baseSpeed = 10;
=======
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerClass = PlayerClasses.Scout;
        baseSpeed = 9;
>>>>>>> refs/remotes/origin/KDCheapCheap/Development
    }
}
