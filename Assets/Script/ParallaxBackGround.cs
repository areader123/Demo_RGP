using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float Length;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        Length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMove = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y); 

        if(distanceMove > xPosition + Length) 
        {
            xPosition = xPosition + Length;
        }else if(distanceMove < xPosition - Length) 
        {
            xPosition = xPosition - Length;
        }

    }

}
