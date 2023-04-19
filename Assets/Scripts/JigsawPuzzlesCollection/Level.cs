﻿//This code file was bought and remained as its original format.

using UnityEngine;

namespace JigsawPuzzlesCollection.Scripts
{
    [CreateAssetMenu(fileName = "New Level", menuName = "JigsawPuzzlesCollection/Level")]
    public class Level : ScriptableObject
    {
        public string Name;
        public Sprite Image;
        public string Caption;
    }
}