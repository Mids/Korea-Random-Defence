using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	    transform.position += move * speed * Time.deltaTime;
    }
}
