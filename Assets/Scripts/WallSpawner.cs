using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallSpawner : MonoBehaviour
{
    public Sprite[] Walls;
    public float w, h;
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
                CreateTile(i * 16, j * 16);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTile(float x, float y)
    {
        GameObject obj = new GameObject("Wall_tile");
        var wallTile = Instantiate(obj,transform);
        Destroy(obj);
        var spr = wallTile.AddComponent<SpriteRenderer>();
        spr.material = mat;
        spr.sprite = Walls[Random.Range(0, Walls.Length - 1)];
        wallTile.transform.position = new Vector3(0.01f*x, 0.01f * y,0.3f);
        
    }

}
