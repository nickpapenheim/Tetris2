using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPiece : MonoBehaviour
{

    public GameObject[] Pieces;
    // Start is called before the first frame update
    void Start()
    {
        NewPiece();
    }

    public void NewPiece() 
    {
        Instantiate(Pieces[Random.Range(0, Pieces.Length)], transform.position, Quaternion.identity);
    }
}
