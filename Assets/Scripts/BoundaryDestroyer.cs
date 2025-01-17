﻿using UnityEngine;
using System.Collections;

public class BoundaryDestroyer : MonoBehaviour {

    private GameLogic GL;
    private Quaternion OrgRotation;
    private Vector3 OrgPosition;

    [SerializeField]
    private float lifeDecreaseSpeed = 10.0f;

    
    float boostDistMax = 40.0f;
    float boostDistMin = 10.0f;
    public float lifeLossDistMin = 18.0f;

    // Use this for initialization
    void Start () {
        GL = GameObject.Find("GameManager").GetComponent<GameLogic>();
    }
    

    void Awake()
    {
        OrgRotation = transform.rotation;
        OrgPosition = transform.position;
    }

    void LateUpdate()
    {
        transform.rotation = OrgRotation;
        transform.position = OrgPosition + new Vector3(0,0,transform.parent.position.z - UnityEngine.Camera.main.GetComponent<Camera>().getZoomZDiff());
    }


    void OnDrawGizmos()
    {
        var leadingCar = UnityEngine.Camera.main.GetComponent<Camera>().GetLeadingPlayerPosition();
        leadingCar.z -= UnityEngine.Camera.main.GetComponent<Camera>().getZoomZDiff() + 20.0f;
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(leadingCar - new Vector3(20, 0, 0), leadingCar + new Vector3(20, 0, 0));
    }
    
    public float GetPushingWall()
    {
        var leadingCar = UnityEngine.Camera.main.GetComponent<Camera>().GetLeadingPlayerPosition().z;
        leadingCar -= UnityEngine.Camera.main.GetComponent<Camera>().getZoomZDiff() + lifeLossDistMin + 5.0f;
        return leadingCar;
    }

    void Update()
    {
        foreach(var pd in Data.GetPlayerData())
        {
            var car = pd.GetGameObject();
            if (car == null) continue;

            var leadingCar = UnityEngine.Camera.main.GetComponent<Camera>().GetLeadingPlayerPosition();
            leadingCar.z -= UnityEngine.Camera.main.GetComponent<Camera>().getZoomZDiff();

            //If vry far behind wall just kill the car
            if (car.transform.position.z < leadingCar.z - 200.0f)
                GL.DestroyCar(pd, true);
            else if (car.transform.position.z < leadingCar.z - lifeLossDistMin) //If behind wall damage car progressively
                car.GetComponent<CarController>().ReduceLife(Time.deltaTime * lifeDecreaseSpeed);


            //If close to the wall - boost car a bit
            if (car.transform.position.z < leadingCar.z - boostDistMin)
            {
                float boost = 0.0f;
                float dist = leadingCar.z - car.transform.position.z;
                if (dist >= boostDistMax)
                    boost = 1.0f;
                else
                if (dist <= boostDistMin)
                    boost = 0.0f;
                else
                {
                    boost = (dist - boostDistMin) / (boostDistMax - boostDistMin);
                }
                car.GetComponent<CarController>().SetBoost(boost);

            }
            else
                car.GetComponent<CarController>().SetBoost(-1);
        }
    }

    IEnumerator RespawnCar(PlayerData car)
    {
        yield return new WaitForSeconds(1);
        GL.SpawnCar(car);
    }
}
