using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTypes
{
    public class Tile
    {
        public int ID;
        public string name;
        public PrimitiveType shape;
        public Vector3 scale;
        public Color color;
        public float height;
        public float playerHeight;
        public int walkable;

        public Tile(int id, string nam, PrimitiveType sh, Vector3 sc, Color col, float he, float playerHe, int walk)
        {
            ID = id;
            name = nam;
            shape = sh;
            scale = sc;
            color = col;
            height = he;
            playerHeight = playerHe;
            walkable = walk;
        }

    }


}

