using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenPlatformController : MonoBehaviour
{
    private Vector2 origin;
    private EdgeCollider2D collider;
    public Vector2 dir;
    public float dist;
    

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<EdgeCollider2D>();
        origin = new Vector2(transform.position.x + 0.06f, transform.position.y+0.08f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir,dist);
        if (hit.transform!=null)
        {
            if (hit.transform.gameObject.tag == "cam_target")
                collider.isTrigger = false;
            else
            {
                collider.isTrigger = true;
            }
        }else collider.isTrigger = true;

    }
}
