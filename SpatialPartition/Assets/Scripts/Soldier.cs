using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier
{
    public MeshRenderer soldiersMeshRenderer;
    public Transform soldierTrans;
    protected float walkSpeed;
    public Soldier previousSoldier;
    public Soldier nextSoldier;

    public virtual void Move()
    {

    }

    public virtual void Move(Soldier soldier)
    {

    }
}

