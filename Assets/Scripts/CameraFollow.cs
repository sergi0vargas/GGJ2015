using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Transform target;


    public float camSpeed = 2;
    public float distancia = -10;
    private Vector3 targetPos;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update () {

        targetPos = new  Vector3(target.position.x, target.position.y,target.position.z + distancia);

        if(Vector3.Distance(transform.position, targetPos)>.2)
            transform.position = Vector3.Lerp(transform.position, targetPos, camSpeed * Time.deltaTime);

	}
}
