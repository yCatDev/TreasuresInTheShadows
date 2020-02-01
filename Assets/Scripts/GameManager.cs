using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState {
        Menu, Play, Over
    };

    private GameState _gameState;

    // Start is called before the first frame update
    void Start()
    {
        _gameState = GameState.Menu;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
