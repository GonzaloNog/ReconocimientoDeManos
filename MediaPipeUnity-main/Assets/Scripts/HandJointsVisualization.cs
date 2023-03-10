using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandJointsVisualization : MonoBehaviour
{
    public MediapipeHandsReceiver mediapipeHandsReceiver;
    public GameObject landmark;
    public GameObject leftHandGO;
    public GameObject rightHandGO;
    public float dist;
    public Vector2 imgAspect;
    private float time = 0;
    private bool comand = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 21; i++)
        {
            leftHandGO.transform.GetChild(i).transform.position = new Vector3(mediapipeHandsReceiver.handLeft[i].x* imgAspect.x,
                                                                              mediapipeHandsReceiver.handLeft[i].y* imgAspect.y,
                                                                              mediapipeHandsReceiver.handLeft[i].z);

            rightHandGO.transform.GetChild(i).transform.position = new Vector3(mediapipeHandsReceiver.handRight[i].x * imgAspect.x,
                                                                               mediapipeHandsReceiver.handRight[i].y * imgAspect.y,
                                                                               mediapipeHandsReceiver.handRight[i].z);

        }
//Deteccion de gestos por proximidad de puntos 
        dist = Vector3.Distance(leftHandGO.transform.GetChild(8).transform.position, leftHandGO.transform.GetChild(5).transform.position);
        if (dist < 1 && dist > 0.01f)
        {
            dist = Vector3.Distance(leftHandGO.transform.GetChild(12).transform.position, leftHandGO.transform.GetChild(9).transform.position);
            if(dist < 1 && dist > 0.01f)
            {
                dist = Vector3.Distance(leftHandGO.transform.GetChild(16).transform.position, leftHandGO.transform.GetChild(13).transform.position);
                if(dist < 1 && dist > 0.01f)
                {
                    dist = Vector3.Distance(leftHandGO.transform.GetChild(20).transform.position, leftHandGO.transform.GetChild(17).transform.position);
                    if(dist < 1 && dist > 0.01f)
                    {
                        dist = Vector3.Distance(leftHandGO.transform.GetChild(5).transform.position, leftHandGO.transform.GetChild(0).transform.position);
                        if(dist > 2f) 
                        {
                            if (comand)
                            {
                                Debug.Log("ManoCerrada");
                                GameManager.instance.closedHandLeft();
                                comand = false;
                            }
                        }                        
                    }
                }
            }
        }
        dist = Vector3.Distance(leftHandGO.transform.GetChild(4).transform.position, leftHandGO.transform.GetChild(12).transform.position);
        //Debug.Log(dist);
        if (dist < 0.6f && dist > 0.01f)
        {
            if (comand)
            {
                Debug.Log("grosor");
                GameManager.instance.changeGrosor();
                comand = false;
            }
        }
        dist = Vector3.Distance(leftHandGO.transform.GetChild(4).transform.position, leftHandGO.transform.GetChild(16).transform.position);
        //Debug.Log(dist);
        if (dist < 0.6f && dist > 0.01f)
        {
            if (comand)
            {
                Debug.Log("color");
                GameManager.instance.colorPlus();
                comand = false;
            }
        }
        dist = Vector3.Distance(leftHandGO.transform.GetChild(4).transform.position, leftHandGO.transform.GetChild(20).transform.position);
        //Debug.Log(dist);
        if (dist < 0.6f && dist > 0.01f)
        {
            if (comand)
            {
                Debug.Log("Restart");
                GameManager.instance.ResetDraw();
                comand = false;
            }
        }
        dist = Vector3.Distance(leftHandGO.transform.GetChild(8).transform.position, leftHandGO.transform.GetChild(4).transform.position);
        if (dist < 0.6f && dist > 0.01f)
        {
            if (comand)
            {
                Debug.Log("OK");
                GameManager.instance.okHandLeft();
                comand = false;
            }
        }

        //Timer permisos entre comandos
        if (!comand)
        {
            time += Time.deltaTime;
            if(time >= GameManager.instance.getTimeComand())
            {
                time = 0;
                comand = true;
            }
        }
    }
}
