using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Walls : MonoBehaviour
{

    public GameObject Char, Light, wallT, wallB, wallR, wallL, gLight, cLight;
    public Text score;
    public int points = 0, counter = 0;
    float t, speed = 3, actualScale;
    Vector3 scalePos;
    bool moveT, moveB, moveR, moveL, scale;

    public struct Pos
    {
        public float N, S, E, W;
    }

    Pos free;

    void Awake()
    {

        /*
        wallL.SetActive(false);
        wallR.SetActive(false);
        wallT.SetActive(false);
        wallB.SetActive(false);
        */

        Time.timeScale = 1;

        wallL.transform.position = new Vector3(-7 , 0, 0);
        wallR.transform.position = new Vector3(7 , 0, 0);
        wallT.transform.position = new Vector3(0, 7, 0);
        wallB.transform.position = new Vector3(0, -7, 0);

        moveT = false;
        moveR = false;
        moveL = false;
        moveB = false;

        free.E = 10;
        free.N = 10;
        free.W = -10;
        free.S = -10;
    }

    float FilledArea(Pos free)
    {
        return ((100 / ((Camera.main.aspect * 20) * (20))) * ((Mathf.Abs(free.W) + free.E) * (free.N + Mathf.Abs(free.S))));
    }

    void Start()
    {
        t = Time.time + 3;

        Debug.Log(FilledArea(free));
    }

    void FixedUpdate()
    {
        if (Time.time > t)
        {
            StartCoroutine(MoveWall());
            t += 3;
        }
        /*
        if (wallT.transform.position.y == free.N)
        {
            moveT = false;
        }
        if (wallB.transform.position.y == free.S)
        {
            moveB = false;
        }
        if (wallR.transform.position.x == free.E)
        {
            moveR = false;
        }
        if (wallL.transform.position.x == free.W)
        {
            moveL = false;
        }
        */
    }

    void Update()
    {
        if (moveT)
        {
            wallT.transform.position = Vector3.Lerp(wallT.transform.position, new Vector3(0, free.N, 0), Time.deltaTime / speed);
        }
        if (moveB)
        {
            wallB.transform.position = Vector3.Lerp(wallB.transform.position, new Vector3(0, free.S, 0), Time.deltaTime / speed);
        }
        if (moveR)
        {
            wallR.transform.position = Vector3.Lerp(wallR.transform.position, new Vector3(free.E, 0, 0), Time.deltaTime / speed);
        }
        if (moveL)
        {
            wallL.transform.position = Vector3.Lerp(wallL.transform.position, new Vector3(free.W, 0, 0), Time.deltaTime / speed);
        }
        if (scale)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, actualScale, Time.deltaTime);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, scalePos, Time.deltaTime);
        }
    }

    char shortestDist(float distN,float distS,float distE,float distW)
    {
        if (distN < distS)
        {
            if (distN < distE)
            {
                if (distN < distW)
                {
                    return 'N';
                }
                else
                {
                    return 'W';
                }
            }
            else
            {
                if (distE < distW)
                {
                    return 'E';
                }
                else
                {
                    return 'W';
                }
            }
        }
        else
        {
            if (distS < distE)
            {
                if (distS < distW)
                {
                    return 'S';
                }
                else
                {
                    return 'W';
                }
            }
            else
            {
                if (distE < distW)
                {
                    return 'E';
                }
                else
                {
                    return 'W';
                }
            }
        }
    }

    IEnumerator MoveWall()
    {
        //Mathf.Abs(Char.transform.position.x) > Mathf.Abs(Char.transform.position.y)

        /*
        Debug.Log((Mathf.Abs(Char.transform.position.x) + ((free.E + free.W) / 2)) + " " + (Mathf.Abs(Char.transform.position.y) + ((free.N + free.S) / 2)));
        if ((Mathf.Abs(Char.transform.position.x)+((free.E+free.W)/2)) > (Mathf.Abs(Char.transform.position.y)+((free.N+free.S)/2)))
        {
            if (Char.transform.position.x > ((free.E + free.W) / 2))
            {
                free.E = ((free.W + free.E) / 2f);
                moveR = true;
            }
            else
            {
                free.W = ((free.E + free.W) / 2f);
                moveL = true;
            }
        }
        else
        {
            if (Char.transform.position.y > ((free.N + free.S) / 2))
            {
                free.N = ((free.S + free.N) / 2f);
                moveT = true;
            }
            else
            {
                free.S = ((free.N + free.S) / 2f);
                moveB = true;
            }
        }
        */

        //calcolo la distanza dalle 4 mura
        float distN, distS, distE, distW;
        distN = ((((Char.transform.position.x) - ((wallR.transform.position.x) - (wallL.transform.position.x))) * ((Char.transform.position.y) - (wallT.transform.position.y))) / 2);
        distS = ((((Char.transform.position.x) - ((wallR.transform.position.x) - (wallL.transform.position.x))) * ((wallB.transform.position.y) - (Char.transform.position.y))) / 2);
        distE = ((((Char.transform.position.x) - (wallR.transform.position.x)) * ((Char.transform.position.y) - ((wallT.transform.position.y) - (wallB.transform.position.y)))) / 2);
        distW = ((((wallL.transform.position.x) - (Char.transform.position.x)) * ((Char.transform.position.y) - ((wallT.transform.position.y) - (wallB.transform.position.y)))) / 2);
        Debug.Log("N:" + distN + " S:" + distS + " E:" + distE + " W:" + distW);
        Debug.Log("N:" + wallT.transform.position.y + " S:" + wallB.transform.position.y + " E:" + wallR.transform.position.x + " W:" + wallL.transform.position.x);
        //valuto la distanza minore
        char sDist = shortestDist(distN, distS, distE, distW);
        //muovo il muro con la distanza minore
        switch (sDist)
        {
            case 'N':
                free.N = ((free.S + free.N) / 2f);
                moveT = true;
                break;
            case 'S':
                free.S = ((free.N + free.S) / 2f);
                moveB = true;
                break;
            case 'E':
                free.E = ((free.W + free.E) / 2f);
                moveR = true;
                break;
            case 'W':
                free.W = ((free.E + free.W) / 2f);
                moveL = true;
                break;
        }

        Light.transform.position = new Vector3(((free.W + free.E) / 2), ((free.S + free.N) / 2), 0);
        scalePos = new Vector3(((free.W + free.E) / 2), ((free.S + free.N) / 2), -10);
        points++;
        score.text = points.ToString();
        if (counter == 3)
        {
            gLight.GetComponent<DynamicLight>().LightRadius = gLight.GetComponent<DynamicLight>().LightRadius / 3;
            cLight.GetComponent<DynamicLight>().LightRadius = cLight.GetComponent<DynamicLight>().LightRadius / 3;
            actualScale = Camera.main.orthographicSize / 4;
            scale = true;            
            Char.transform.localScale = Char.transform.localScale / 4;

            Vector2 sizeW, sizeH;
            sizeW = wallT.GetComponent<BoxCollider2D>().size;
            sizeW.y = sizeW.y/2;
            wallT.GetComponent<BoxCollider2D>().size = sizeW;
            wallB.GetComponent<BoxCollider2D>().size = sizeW;
            sizeH = wallR.GetComponent<BoxCollider2D>().size;
            sizeH.x = sizeH.x / 2;
            wallR.GetComponent<BoxCollider2D>().size = sizeH;
            wallL.GetComponent<BoxCollider2D>().size = sizeH;
            counter = 0;
            Camera.main.transform.position = scalePos;
        }
        else
        {
            counter++;
        }
        yield return null;
    }
}