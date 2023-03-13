using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandJointsVisualization : MonoBehaviour
{
    public MediapipeHandsReceiver mediapipeHandsReceiver;
    public GameObject landmark;
    public GameObject leftHandGO;
    public GameObject rightHandGO;
    public float dist; //Calcula al distancia entre dos puntos de la mano
    public Vector2 imgAspect;
    private float time = 0; //Contar el tiempo que transcurre desde que se ejecuto un comando
    private bool comand = true;// Comprobar si los comandos pueden usarse

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
// 1 - Deteccion de gestos por proximidad de puntos (puño cerrado)
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
        // 1.End 
        // 2 - Deteccion de gestos por proximidad de puntos (pulgar con idice)
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
        // 2.end
        // 3 - Deteccion de gestos por proximidad de puntos (pulgar con dedo medio)
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
        // 3.end
        // 4 - Deteccion de gestos por proximidad de puntos (pulgar con anular)
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
        //4.end
        //5 - Deteccion de gestos por proximidad de puntos (pulgar con meñique)
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
        // 6 - Deteccion de gestos por proximidad de puntos (marco)
        dist = Vector3.Distance(leftHandGO.transform.GetChild(8).transform.position, rightHandGO.transform.GetChild(4).transform.position);
        if(dist < 0.6f && dist > 0.01f)
        {
            dist = Vector3.Distance(leftHandGO.transform.GetChild(4).transform.position, rightHandGO.transform.GetChild(8).transform.position);
            if (dist < 0.6f && dist > 0.01f)
            {
                if (comand)
                {
                    GameManager.instance.Screen();//apaga la UI para la foto
                    comand = false;
                    leftHandGO.SetActive(false);//apaga los puntos dela mano izquierda
                    rightHandGO.SetActive(false);// apaga los puntos de la mano derecha
                }
            }
        }
        //6.End

        //Timer permisos entre comandos
        if (!comand)
        {
            time += Time.deltaTime;
            if(time >= GameManager.instance.getTimeComand())
            {
                time = 0;
                comand = true;
                GameManager.instance.ScreenOn();//Enciendo de nuevo la UI
                leftHandGO.SetActive(true);//Enciendo mano izquierda
                rightHandGO.SetActive(true);//Enciendo mano derecha 
            }
        }
    }
}
