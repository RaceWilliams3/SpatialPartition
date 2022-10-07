using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    int cellSize;

    Soldier[,] cells;

    public Grid(int mapWidth, int cellSize)
    {
        this.cellSize = cellSize;
        int numberOfCells = mapWidth / cellSize;

        cells = new Soldier[numberOfCells, numberOfCells];
    }

    public void Add(Soldier soldier)
    {
        int cellx = (int)(soldier.soldierTrans.position.x / cellSize);
        int cellz = (int)(soldier.soldierTrans.position.y / cellSize);

        soldier.previousSoldier = null;
        soldier.nextSoldier = cells[cellx,cellz];

        cells[cellx,cellz] = soldier;

        if (soldier.nextSoldier != null)
        {
            soldier.nextSoldier.previousSoldier = soldier;
        }
    }

    public Soldier FindClosestEnemy(Soldier friendlySoldier)
    {
        int cellx = (int)(friendlySoldier.soldierTrans.position.x / cellSize);
        int cellz = (int)(friendlySoldier.soldierTrans.position.y / cellSize);

        Soldier enemy = cells[cellx, cellz];

        Soldier closestSoldier = null;

        float bestDistSqr = Mathf.Infinity;

        while(enemy != null)
        {
            float distSqr = (enemy.soldierTrans.position - friendlySoldier.soldierTrans.position).sqrMagnitude;

            if (distSqr < bestDistSqr)
            {
                bestDistSqr = distSqr;

                closestSoldier = enemy;
            }

            enemy = enemy.nextSoldier;
        }

        return closestSoldier;
    }

    public void Move(Soldier soldier, Vector3 oldPos)
    {
        int oldCellx = (int)(oldPos.x / cellSize);
        int oldCellz = (int)(oldPos.z / cellSize);

        int cellx = (int)(soldier.soldierTrans.position.x / cellSize);
        int cellz = (int)(soldier.soldierTrans.position.y / cellSize);

        if (oldCellx == cellx && oldCellz == cellz)
        {
            return;
        }
        if (soldier.previousSoldier != null)
        {
            soldier.previousSoldier.nextSoldier = soldier.nextSoldier;
        }
        if (soldier.nextSoldier != null)
        {
            soldier.nextSoldier.previousSoldier = soldier.previousSoldier;
        }

        if (cells[oldCellx, oldCellz] == soldier)
        {
            cells[oldCellx, oldCellz] = soldier.nextSoldier;
        }

        Add(soldier);
    }
}
