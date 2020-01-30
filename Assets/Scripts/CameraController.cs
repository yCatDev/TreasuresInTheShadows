using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float yTopBorder, yBottomBorder, xLeftBorder, xRightBorder;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetUpCamera(int w, int h, int tilesInViewX, int tilesInViewY)
    {
        yTopBorder = (h - tilesInViewY) * 0.16f + transform.position.y;
        xRightBorder = transform.position.x + (w - tilesInViewX) * 0.16f;
        xLeftBorder = transform.position.x;
        yBottomBorder = transform.position.y;
        target = GameObject.FindWithTag("cam_target");
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 next = target.transform.position;
            next.z = -10;

            if (next.x > xRightBorder)
                next.x = xRightBorder;
            if (next.x < xLeftBorder)
                next.x = xLeftBorder;
   

            if (next.y < yBottomBorder)
                next.y = yBottomBorder;
            if (next.y > yTopBorder)
                next.y = yTopBorder;

            transform.position = next;
        }
    }
}
