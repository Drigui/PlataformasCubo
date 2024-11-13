using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] float parallaxSpeed;

    private Transform gameCameraPos;
    private Vector3 lastCameraPos;

    private void Start()
    {
        gameCameraPos = Camera.main.transform;
        lastCameraPos = gameCameraPos.position;
    }

    private void LateUpdate()
    {
        //calculate diference of move
        Vector3 backgroundMove = gameCameraPos.position - lastCameraPos;

        //move with speed
        transform.position += new Vector3(backgroundMove.x * parallaxSpeed, backgroundMove.y * parallaxSpeed, 0);

        //update the lastcamerapos
        lastCameraPos = gameCameraPos.position;
    }
}
