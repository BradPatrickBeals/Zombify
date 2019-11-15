using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : PlayerController
{
<<<<<<< HEAD
    public override void Start()
    {
        base.Start();
        playerClass = PlayerClasses.Soldier;
=======
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerClass = PlayerClasses.Engineer;
>>>>>>> refs/remotes/origin/KDCheapCheap/Development
        baseSpeed = 5;
    }
}
