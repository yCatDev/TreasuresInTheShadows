using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour
{
    private SpriteRenderer sr;

    public Sprite[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = tiles[Random.Range(0, tiles.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
