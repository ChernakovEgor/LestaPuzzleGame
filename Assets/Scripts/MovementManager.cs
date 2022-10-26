using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager
{
    const int block = 1;
    const int red = 2;
    const int orange = 3;
    const int yellow = 4;
    //bool isMovingBlock = false;
    private int[,] grid = {
        {block, block,  block, block,  block, block,  block},
        {block, orange, block, yellow, block, orange, block},
        {block, red,    0,     yellow, 0,     orange, block},
        {block, yellow, block, red,    block, red,    block},
        {block, orange, 0,     orange, 0,     yellow, block},
        {block, red,    block, red,    block, yellow, block,},
        {block, block,  block, block,  block, block,  block}};

    private (int, int) currentPosition = (3, 3);
    
    public bool CanMove(bool isMovingBlock, (int, int) direction) {
        if (!isMovingBlock && grid[currentPosition.Item1 + direction.Item1, currentPosition.Item2 + direction.Item2] != block) {
            // currentPosition.Item1 += direction.Item1;
            // currentPosition.Item2 += direction.Item2;
            return true;
        }

        return false;
    }

    public void Move((int, int) direction) {
        currentPosition.Item1 += direction.Item1;
        currentPosition.Item2 += direction.Item2;
    }
}
