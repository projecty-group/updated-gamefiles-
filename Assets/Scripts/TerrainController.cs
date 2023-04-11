using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public GameObject miniGameControls;
    

    private void Start()
    {
        miniGameControls.SetActive(false);
    }
}