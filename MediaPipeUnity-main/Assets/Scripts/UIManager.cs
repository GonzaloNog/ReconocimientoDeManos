using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image drawOn; //imagen para coproar si se puede  pintar o no
    public Renderer background; //fondo
    //materiales para el fondo
    public Material tex1;
    public Material tex2;
    public Material tex3;
    public Material tex4;
    public Material tex5;
    public Material tex6;
    //
    public Image[] img; // imagenes de colores que representan el color en uso
    public TextMeshProUGUI tex; // tesxto para el grosor
    private int a = 0; // index background

    private void Start()
    {
        drawChange(GameManager.instance.getDraw());
        changeBackground();
        UpdateGrosor();
    }
    public void drawChange(bool change){//cambio los colores entre rojo y verde para saber si se puede pintar o no
        if(change){
            drawOn.color = Color.green;
        }
        else
        {
            drawOn.color = Color.red;
        }
    }
    public void changeBackground()// cambio el fondo segun el index 
    {
        a++;
        if (a > 5)
            a = 0;
        switch (a)
        {
            case 0:
                background.material = tex1;
                break;
            case 1:
                background.material = tex2;
                break;
            case 2:
                background.material = tex3;
                break;
            case 3:
                background.material = tex4;
                break;
            case 4:
                background.material = tex5;
                break;
            case 5:
                background.material = tex6;
                break;
        }
        
    }
    public void UpdateColor()//cambio la lista de colores, los colores no seleccionados tienen un alfa bajo y el unico seleccionado tiene al alfa al 100%
    {
        for(int a = 0; a < img.Length; a++){
            if(a == GameManager.instance.getColorIndex())
            {
                img[a].color = new Color(img[a].color.r, img[a].color.g, img[a].color.b, 255);
            }
            else
            {
                img[a].color = new Color(img[a].color.r, img[a].color.g, img[a].color.b, 0.1f);
            }
        }
    }
    public void UpdateGrosor() {//representacion visual del grosor
        tex.text = "" + GameManager.instance.getGrosor();
    }
    public void setVision(bool vision)//controla el prendido y apagado de la interfaz 
    {
        this.gameObject.SetActive(vision);
    }
}
