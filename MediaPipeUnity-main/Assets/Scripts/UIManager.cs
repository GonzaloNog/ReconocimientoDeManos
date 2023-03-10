using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image drawOn;
    public Renderer background;
    public Material tex1;
    public Material tex2;
    public Material tex3;
    public Image[] img;
    public TextMeshProUGUI tex;
    private int a = 0;

    private void Start()
    {
        drawChange(GameManager.instance.getDraw());
        changeBackground();
        UpdateGrosor();
    }
    public void drawChange(bool change){
        if(change){
            drawOn.color = Color.green;
        }
        else
        {
            drawOn.color = Color.red;
        }
    }
    public void changeBackground()
    {
        a++;
        if (a > 2)
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
        }
        
    }
    public void UpdateColor()
    {
        for(int a = 0; a < img.Length; a++){
            if(a == GameManager.instance.getColorIndex())
            {
                img[a].color = new Color(img[a].color.r, img[a].color.g, img[a].color.b, 255);
            }
            else
            {
                Debug.Log("HOLA");
                img[a].color = new Color(img[a].color.r, img[a].color.g, img[a].color.b, 0.1f);
            }
        }
    }
    public void UpdateGrosor() {
        tex.text = "" + GameManager.instance.getGrosor();
    }
}
