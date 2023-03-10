using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Draw : MonoBehaviour
{
    public bool draw = false;
    public GameObject landmark;
    public int render = 0;

    //LineRenderer allow to draw a line using an array of points
    //we will use the position of the hand and we will add a new point to the line
    //check the documentation for exploring the properties of LineRenderer
    //https://docs.unity3d.com/Manual/class-LineRenderer.html
    LineRenderer lineRenderer;

    //The list of points that we will update at each frame
    Queue<Vector3> points = new Queue<Vector3>();
    Queue<Vector3> reset = new Queue<Vector3>();

    void Start()
    {
        //draw = false;
        //Gets the line renderer component that is in the same Game Object
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        //Assigns a material and modifies the properties
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = GameManager.instance.getGrosor();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        SetColor(GameManager.instance.getColorIndex());
        lineRenderer.widthMultiplier = GameManager.instance.getGrosor();
        // We draw in the screen only if the draw variable is true.
        // it can be changed when we want to stop drawing
        if (GameManager.instance.getDraw())
        {
            //Adds the curren position of the landmark assigned in the inspector to the line points list
            //We can choose any landmark to paint
            points.Enqueue(landmark.transform.position);
            lineRenderer.positionCount++;
            lineRenderer.SetPositions(points.ToArray());
            render++;
        }
    }

    private void SetColor(int a)
    {
        switch (a)
        {
            case 0:
                lineRenderer.SetColors(Color.blue, Color.blue);
                break;
            case 1:
                lineRenderer.SetColors(Color.red, Color.red);
                break;
            case 2:
                lineRenderer.SetColors(Color.green, Color.green);
                break;
            case 3:
                lineRenderer.SetColors(Color.yellow, Color.yellow);
                break;
            case 4:
                lineRenderer.SetColors(Color.black, Color.black);
                break;
            case 5:
                lineRenderer.SetColors(Color.gray, Color.grey);
                break;
        }
    }
}
