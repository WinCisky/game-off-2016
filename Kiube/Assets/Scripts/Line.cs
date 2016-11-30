using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Line : MonoBehaviour {
    public Camera myCamera;
    public float posx, posy;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer;
    public float b;
    int frames;
    int lastSec;
    void Start() {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.numCornerVertices = 1;
        lineRenderer.numCapVertices = 1;
        lineRenderer.numPositions = lengthOfLineRenderer;
        lastSec = (int) Time.time;
        myCamera.orthographicSize = 200;
    }
    void Update() {
        Time.timeScale = 0.1f + Mathf.PingPong(Time.time, 1.5f);
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        float t = Time.time;
        b = 0.1759f;
        int i = 1;
        while (i < lengthOfLineRenderer)
        {
            posx = i * b * Mathf.Cos(i + t);
            posy = i * b * Mathf.Sin(Mathf.Cos(i + t)) / Mathf.Sin(i);
            Vector3 pos = new Vector3(i * b * Mathf.Cos(i + t), i * b * Mathf.Sin(Mathf.Cos(i + t)) / Mathf.Sin(i), 0);
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }
}