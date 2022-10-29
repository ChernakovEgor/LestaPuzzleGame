using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokensMovementManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject winMessage;
    public bool isInputEnabled = true;
    const int block = -1;
    const int red = 5;
    const int orange = 3;
    const int yellow = 1;

    private GameObject[] tokens;

    public GameObject fruits;
    
    private int[,] grid = {
        {block, block,  block, block,  block, block,  block},
        {block, orange, block, yellow, block, orange, block},
        {block, red,    0,     yellow, 0,     orange, block},
        {block, yellow, block, red,    block, red,    block},
        {block, orange, 0,     orange, 0,     yellow, block},
        {block, red,    block, red,    block, yellow, block,},
        {block, block,  block, block,  block, block,  block}};

    private (int, int) currentPosition = (3, 3);
    private int selectedToken = 0;
    // Start is called before the first frame update
    void Start()
    {
        canvas.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        tokens = new GameObject[15];
        for (int i = 0; i < 15; i++) {
            tokens[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public bool CanMove(bool isMovingBlock, (int, int) direction) {
        if (!isMovingBlock && grid[currentPosition.Item1 + direction.Item1, currentPosition.Item2 + direction.Item2] != block) {
            // currentPosition.Item1 += direction.Item1;
            // currentPosition.Item2 += direction.Item2;
            return true;
        } 
        if (isMovingBlock && grid[currentPosition.Item1 + direction.Item1, currentPosition.Item2 + direction.Item2] == 0) {
            return true;
        }

        return false;
    }

    public void Move((int, int) direction, bool isMovingBlock) {
        currentPosition.Item1 += direction.Item1;
        currentPosition.Item2 += direction.Item2;

        // if (isMovingBlock) {
        //     grid[currentPosition.Item1, currentPosition.Item2] = selectedToken;
            
        // }
        
        // if (checkWinCondition()) {
        //     Debug.Log("Win!");
        // }
    }

    public GameObject SelectToken() {
        float x =  currentPosition.Item2 - 2.5f;
        float y =  -currentPosition.Item1 + 3.5f;

        foreach (GameObject token in tokens) {
            if (token.transform.position.x == x && token.transform.position.y == y) {
                selectedToken = grid[currentPosition.Item1, currentPosition.Item2];
                grid[currentPosition.Item1, currentPosition.Item2] = 0;
                return token;
            }
        }

        return null;
    }

    public void DropToken(GameObject token) {
        grid[currentPosition.Item1, currentPosition.Item2] = selectedToken;

        token.transform.parent = this.transform;
        if (checkWinCondition()) {
            isInputEnabled = false;
            canvas.GetComponent<Image>().CrossFadeAlpha(0.7f, 1.0f, false);
            winMessage.SetActive(true);
            fruits.SetActive(true);
        }
    }

    public void Fade() {
        winMessage.SetActive(true);
        canvas.GetComponent<Image>().CrossFadeAlpha(0.7f, 1.0f, false);
        fruits.SetActive(true);
    }

    bool checkWinCondition() {
        for (int column = 1; column <= 5; column += 2 ) {
            for (int row = 1; row <= 5; row++) {
                if (grid[row, column] != column) return false; 
            }
        }

        return true;
    }
}
