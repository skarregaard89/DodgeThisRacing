﻿using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {


    private UnityEngine.Camera camera;
	// Use this for initialization
	void Start () {
        camera = UnityEngine.Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(camera.transform.forward, camera.transform.up);
    }
}