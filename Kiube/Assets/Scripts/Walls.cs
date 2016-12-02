using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Walls : MonoBehaviour
{

    public GameObject Char, Light, wallT, wallB, wallR, wallL, gLight, cLight;
    public Text score;
    public int points = 0, counter = 0, reset_counter = 3;
    float t, speed = 3, actualScale, waitTime = 3;
    Vector3 scalePos;
    bool moveT, moveB, moveR, moveL, scale;

    public Material ch, bkg;

    public struct Pos
    {
        public float N, S, E, W;
    }

    Pos free;

    void Awake()
    {
        actualScale = Camera.main.orthographicSize;

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

        free.E = 7;
        free.N = 7;
        free.W = -7;
        free.S = -7;
    }

    //to check
    float FilledArea(Pos free)
    {
        return ((100 / ((Camera.main.aspect * 20) * (20))) * ((Mathf.Abs(free.W) + free.E) * (free.N + Mathf.Abs(free.S))));
    }

    void Start()
    {
        t = Time.time + 3;

        Debug.Log(FilledArea(free));

        ch.color = HexToColor("009688");
        bkg.color = HexToColor("B2DFDB");
    }

    void FixedUpdate()
    {
        if (Time.time > t)
        {
            StartCoroutine(MoveWall());
            t += waitTime;
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
            wallT.transform.position = Vector3.Lerp(wallT.transform.position, new Vector3(wallT.transform.position.x, free.N, 0), Time.deltaTime / speed);
        }
        if (moveB)
        {
            wallB.transform.position = Vector3.Lerp(wallB.transform.position, new Vector3(wallB.transform.position.x, free.S, 0), Time.deltaTime / speed);
        }
        if (moveR)
        {
            wallR.transform.position = Vector3.Lerp(wallR.transform.position, new Vector3(free.E, wallR.transform.position.y, 0), Time.deltaTime / speed);
        }
        if (moveL)
        {
            wallL.transform.position = Vector3.Lerp(wallL.transform.position, new Vector3(free.W, wallL.transform.position.y, 0), Time.deltaTime / speed);
        }
        if (scale)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 15, Time.deltaTime);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, scalePos, Time.deltaTime);
            Char.transform.localScale = Vector3.Lerp(Char.transform.localScale, Vector3.one, Time.deltaTime);
        }
    }

    //find the most close wall
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

    char RandomWall()
    {
        int r = Random.Range(0, 4);
        if (r == 0)
        {
            return 'N';
        }else if (r == 1)
        {
            return 'S';
        }else if (r == 2)
        {
            return 'E';
        }else
        {
            return 'W';
        }
    }

    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 250);
    }

    IEnumerator MoveWall()
    {
        
        //calcolo la distanza dalle 4 mura
        float distN, distS, distE, distW;
        distN = ((((Char.transform.position.x) - ((wallR.transform.position.x) - (wallL.transform.position.x))) * ((Char.transform.position.y) - (wallT.transform.position.y))) / 2);
        distS = ((((Char.transform.position.x) - ((wallR.transform.position.x) - (wallL.transform.position.x))) * ((wallB.transform.position.y) - (Char.transform.position.y))) / 2);
        distE = ((((Char.transform.position.x) - (wallR.transform.position.x)) * ((Char.transform.position.y) - ((wallT.transform.position.y) - (wallB.transform.position.y)))) / 2);
        distW = ((((wallL.transform.position.x) - (Char.transform.position.x)) * ((Char.transform.position.y) - ((wallT.transform.position.y) - (wallB.transform.position.y)))) / 2);
        Debug.Log("N:" + distN + " S:" + distS + " E:" + distE + " W:" + distW);
        Debug.Log("N:" + wallT.transform.position.y + " S:" + wallB.transform.position.y + " E:" + wallR.transform.position.x + " W:" + wallL.transform.position.x);
        char sDist;
        /*
        //valuto la distanza minore
        sDist = shortestDist(distN, distS, distE, distW);
        */
        //scelgo a caso il muro da muovere
        sDist = RandomWall();
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
        points++;
        score.text = points.ToString();
        if (counter == reset_counter)
        {
            //scale up lights
            //gLight.GetComponent<DynamicLight>().LightRadius = gLight.GetComponent<DynamicLight>().LightRadius * 3;
            //cLight.GetComponent<DynamicLight>().LightRadius = cLight.GetComponent<DynamicLight>().LightRadius * 3;
            //allontano le mura
            float center_x, center_y;
            center_x = ((free.E + free.W) / 2);
            center_y = ((free.N + free.S) / 2);
            free.E = center_x + 7f;
            free.W = center_x - 7f;
            free.N = center_y + 7f;
            free.S = center_y - 7f;
            wallT.transform.position = new Vector3(center_x, free.N, 0);
            wallB.transform.position = new Vector3(center_x, free.S, 0);
            wallR.transform.position = new Vector3(free.E, center_y, 0);
            wallL.transform.position = new Vector3(free.W, center_y, 0);
            scale = true;          
            //reset  
            counter = 0;
            //centro la camera
            scalePos = new Vector3(center_x, center_y, -10);
            //scale up 
            //Camera.main.orthographicSize = Camera.main.orthographicSize * 4;
            Char.transform.localScale = Char.transform.localScale * 4;
            //hard++
            if (points > 14 && points < 30)
            {
                //level 2
                speed = 2;
                ch.color = HexToColor("00BCD4");
                bkg.color = HexToColor("B2EBF2");
            }
            else if (points > 29 && points < 50)
            {
                //level 3
                waitTime = 2;
                reset_counter = 4;
                ch.color = HexToColor("03A9F4");
                bkg.color = HexToColor("B3E5FC");
            }
            else if(points >49 && points < 100)
            {
                //level 4
                waitTime = 1.5f;
                speed = 1.5f;
                ch.color = HexToColor("2196F3");
                bkg.color = HexToColor("BBDEFB");
            }
            else if (points > 99)
            {
                //level 5
                waitTime = 1f;
                speed = 1f;
                reset_counter = 5;
                ch.color = HexToColor("3F51B5");
                bkg.color = HexToColor("C5CAE9");
            }
        }
        else
        {
            counter++;
        }
        yield return null;
    }
}