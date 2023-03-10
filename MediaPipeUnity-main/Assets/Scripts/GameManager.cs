using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool drawOn = false;
    public UIManager ui;
    public float timeComand = 1.0f;
    private int color = 0;
    private float grosor = 0.1f;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    private void Start()
    {
        ui.UpdateColor();
    }

    public bool getDraw(){
        return drawOn;
    }
    public UIManager GetUIManager()
    {
        return ui;
    }
    public void ResetDraw()
    {
        Application.LoadLevel("Hands");
    }

    //Manos
    public void closedHandLeft()
    {
        ui.changeBackground();
    }
    public void okHandLeft()
    {
        drawOn = !drawOn;
        ui.drawChange(drawOn);
    }

    //Timer
    public float getTimeComand()
    {
        return timeComand;
    }

    public void colorPlus()
    {
        color++;
        if (color > 5)
            color = 0;
        ui.UpdateColor();
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

    public void changeGrosor()
    {
        grosor += 0.1f;
        if(grosor >= 1f)
        {
            grosor = 0.1f;
        }
        ui.UpdateGrosor();
    }
    public float getGrosor()
    {
        return grosor;
    }
}
