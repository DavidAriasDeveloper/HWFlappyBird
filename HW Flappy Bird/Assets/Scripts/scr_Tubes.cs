using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class scr_Tubes : MonoBehaviour {

    public Vector3 velocity;

    public Vector3 ColDistance;

    public Text score;

    public bool UpdateScore = true;

    public scr_Flappy flappy;

    public AudioClip TubeCrossed;//Sonido al pasar un tubo

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (flappy.GameStart)
        {
            moveTube();
        }
	}

    private void moveTube()
    {
        this.transform.position = this.transform.position + (velocity * Time.deltaTime);
        
        if (this.transform.position.x <= -4)
        {
            Vector3 tempPos = this.transform.position + ColDistance;
            tempPos.y = Random.Range(1f, 18f);
            this.transform.position = tempPos;
            UpdateScore = true;
        }

        if (this.transform.position.x <= 5 & UpdateScore)
        {
            int newScore = int.Parse(score.text) + 1;
            score.text = newScore.ToString();
            UpdateScore = false;

            PlaySound(TubeCrossed);
        }
    }

    private void PlaySound(AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}
