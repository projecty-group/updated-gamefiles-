using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardController : MonoBehaviour
{
    public UnityEvent OnPiecePlaced;
    
    private int piecesPlaced = 0;
    public int piecesNeeded = 10;

    // Called when a puzzle piece is placed
    public void PiecePlaced()
    {
        piecesPlaced++;

        // Check if enough pieces have been placed
        if (piecesPlaced >= piecesNeeded)
        {
            // Trigger the OnPiecePlaced event
            OnPiecePlaced.Invoke();
        }
    }
}
