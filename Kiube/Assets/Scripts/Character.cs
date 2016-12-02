using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using GooglePlayGames;
//using UnityEngine.SocialPlatforms;

public class Character : MonoBehaviour {
	void Update () {

		#if UNITY_ANDROID
		if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)
		{
			transform.position = Vector3.Lerp (transform.position, Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position), Time.deltaTime);
		}
		#endif
		#if UNITY_EDITOR
		transform.position = Vector3.Lerp (transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition), Time.deltaTime);
		#endif
		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D coll){
		switch (coll.tag) {
		case "wall":
                /*
                Social.ReportScore(Camera.main.GetComponent<Walls>().points, "CgkIzLWV9OYPEAIQAA", (bool success) => {
                    // handle success or failure
                });
                */
                Debug.Log("Dead");
                if(Camera.main.GetComponent<Walls>().points > PlayerPrefs.GetInt("player_highest_score"))
                {
                    PlayerPrefs.SetInt("player_highest_score", Camera.main.GetComponent<Walls>().points);
                }
                PlayerPrefs.SetInt("player_latest_score", Camera.main.GetComponent<Walls>().points);
                SceneManager.LoadScene("Menu");
			break;
		case "scale":

			break;
		case "slow":

			break;
		case "break":

			break;
		case "rotate":

			break;
		case "invert":

			break;
		}
	}
}
