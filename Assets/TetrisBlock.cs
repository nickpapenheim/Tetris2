using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// programming for every indiviual tetrisBlock, that is used as a composite pattern in a trtris piece.
public class TetrisBlock : MonoBehaviour

{
public Vector3 rotationPoint;
private float previousTime;
public float fallTime = 0.8f;
public static int height = 20;
public static int width = 10;
private static Transform[,] grid = new Transform[width, height];
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    // move left
    {
    // deletes Tetris Block from list, if entirely cleared
       if (transform.hierarchyCount == 0) Destroy(this.gameObject);

       if(Input.GetKeyDown(KeyCode.LeftArrow)) 
       {
        transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove()) {
                transform.position += new Vector3(1, 0, 0);
            }
       }
    // move right
    //inherits GetKeyDown from MonoBehaviour
       if(Input.GetKeyDown(KeyCode.RightArrow)) 
       {
        transform.position += new Vector3(1, 0, 0);
            if(!ValidMove()) {
                transform.position += new Vector3(-1, 0, 0);
            }
       }
    // rotate
       if(Input.GetKeyDown(KeyCode.UpArrow)) {
        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        if (!ValidMove()) {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }
       }
    // move downwards; automatically and by pressing the down arrow
       if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime)) 
       {
        transform.position += new Vector3(0, -1, 0);
        if(!ValidMove()) {
                transform.position += new Vector3(0, 1, 0);
                AddtoGrid();
                CheckForLines();
                this.enabled=false;
                FindObjectOfType<SpawnPiece>().NewPiece();
            }
        previousTime = Time.time;
       }

    }
    // Checks, if Line is completed and deletes completed Lines
    void CheckForLines()
    {
        for (int i = height-1; i >= 0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }
    
    // actually deletes Line, also incerases the score by 1000 every time it deletes the line
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
            FindObjectOfType<Score>().increaseScore(1000);
    }
    // pushes every block above deleted rows down according to the amount of rows delted
    void RowDown(int i)
    {
        for (int y = i; y < height; y++)        
        {
            for (int j = 0; j < width; j++)
            {
                if(grid[j, y] != null)
                {
                grid[j, y - 1] = grid[j, y];
                grid[j, y] = null;
                grid[j, y - 1].transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }
    // adds dropped down blocks to the grid, so they block the other blocks; 
    void AddtoGrid()
     {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
     }
    // Bool value, if line is complete
    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null) {
            return false;
            }
        }

        return true;
    }
    // check if move is valid; if it does not collide with grid
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height) 
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }
}
