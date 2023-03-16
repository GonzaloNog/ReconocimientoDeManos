using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // instancia del gamemanager
    public bool drawOn = false; // para controlar si esta habilitado el dibujo
    public bool changeColorDraw = true;// comprobar si se cambia de color mientras se dibuja
    public UIManager ui;// referencia al UIManager
    public float timeComand = 1.0f;//Tiempo entre un comando y otro
    private int color = 0;// index del color
    private float grosor = 0.1f;// grosor d ela linea



    private void Awake()//seteo la isntancia de mi variable estatica GameManager
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    private void Start()//Updateo la UI
    {
        ui.UpdateColor();
    }

    public bool getChangeColorDraw()
    {
        return changeColorDraw;
    }
    public void setChangeColorDraw(bool draw)
    {
        changeColorDraw = draw;
    }

    public bool getDraw(){
        return drawOn;
    }
    public UIManager GetUIManager()
    {
        return ui;
    }
    public void ResetDraw()// recargo el nivel 
    {
        Application.LoadLevel("Hands");
    }

    //Manos
    public void closedHandLeft()//le digo a la UI se cambie de fondo
    {
        ui.changeBackground();
    }
    public void okHandLeft()//habilito el dibujado
    {
        drawOn = !drawOn;
        ui.drawChange(drawOn);
    }

    //Timer
    public float getTimeComand()
    {
        return timeComand;
    }

    public void colorPlus()// cambio de color
    {
        color++;
        if (color > 5)
            color = 0;
        ui.UpdateColor();
        if (drawOn)
        {
            changeColorDraw = false;
        }
    }
   /* public void colorMiniun()
    {
        color--;
        if (color < 0)
            color = 5;
    }*/
    
    public int getColorIndex()
    {
        return color;
    }

    public void changeGrosor()// cambio el grosor
    {
        grosor += 0.1f;
        if(grosor >= 1f)
        {
            grosor = 0.1f;
        }
        ui.UpdateGrosor();
        if (drawOn)
        {
            changeColorDraw = false;
        }
    }
    public float getGrosor()
    {
        return grosor;
    }

    public Color newColor()//paso un color segun el index de color
    {
        switch (color)
        {
            case 0:
                return Color.blue;
                break;
            case 1:
                return Color.red;
                break;
            case 2:
                return Color.green;
                break;
            case 3:
                return Color.yellow;
                break;
            case 4:
                return Color.black;
                break;
            case 5:
                return new Color(0.8f, 0.5f, 0.2f);
                break;
        }
        return Color.white;
    }

    public void Screen()//saco una foto de la pantalla se guarda en la carpeta raiz
    {
        ui.setVision(false);
        ScreenCapture.CaptureScreenshot("../myDraw.jpg");
        Debug.Log("A screenshot was taken!");
    }
    public void ScreenOn()
    {
        ui.setVision(true);
    }
}
