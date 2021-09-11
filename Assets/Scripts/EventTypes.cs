using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTypes
{
    public class EventTile
    {
        public int ID;
        public string name;
        public PrimitiveType shape;
        public Vector3 scale;
        public Color color;
        public float height;
        public float playerHeight;
        public int walkable;
        public string dialogue;

        public EventTile(int id, string nam, PrimitiveType sh, Vector3 sc, Color col, float he, string text)
        {
            ID = id;
            name = nam;
            shape = sh;
            scale = sc;
            color = col;
            height = he;
            dialogue = text;
        }
    }
}

