
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class LevelManager : MonoBehaviour
{

    public Level[] Levels;
    public LevelObject[] LevelObjects;
    private int w, h;
    public int testRun;
    public WallSpawner wallSpawner;
    public CameraController camCtrl;

    // Start is called before the first frame update
    void Start()
    {
        BuildLevel(testRun);
        camCtrl = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildLevel(int index)
    {
        w = Levels[index].Width;
        h = Levels[index].Height;
        wallSpawner.CreateWall(w,h);
        
        try
        {
            for (int i = 0; i < h; i++)
            {
                var l = Levels[index].GetLevelArr().Split('\n')[i].Split(',');
                //Debug.Log(l[l.Length - 1]);
                for (int j = 0; j < w; j++)
                {
                    foreach (var obj in LevelObjects)
                    {
                        if (obj.TiledIndex == int.Parse(l[j]))
                            Instantiate(obj.tiles[Random.Range(0, obj.tiles.Length-1)],
                                new Vector3(0.01f * 16 * j, 0.01f * 16 * (h-i-1), obj.tiles[0].transform.position.z), Quaternion.identity);
                    }
                }
            }
        }
        catch
        {
            ;
        }
        camCtrl.SetUpCamera(w, h, 20, 15);
    }

    void SpawnObject(GameObject obj, float x, float y)
    {

    }

}


[CreateAssetMenu(fileName = "NewLevel", menuName = "Create level")]
public class Level: ScriptableObject
{
    public string LevelName;
    [SerializeField]
    private Object File;
    public int Width = 0;
    public int Height = 0;

    public string GetLevelArr()
    {
        return File.ToString();
    }
}

[CreateAssetMenu(fileName = "NewLevelObject", menuName = "Create level object")]
public class LevelObject : ScriptableObject
{
    public int TiledIndex = 0;
    public GameObject[] tiles;
}