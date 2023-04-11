using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public GameObject terrainControls;

    private void Start()
    {
        terrainControls.SetActive(false);
    }
}
