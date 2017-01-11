using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Background : MonoBehaviour {

    public Vector3 velocity;
    private Vector3 StartDistance = new Vector3(306, 0, 0);

    public scr_Flappy flappy;

    public scr_Background BackgroundReference;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!flappy.GameOver)
        {
            transform.position += velocity * Time.deltaTime;
            if (transform.position.x <= -310)//Si el anterior fondo ya no es visible
            {
                realign_bg();
            }
        }
	}

    void realign_bg()//Para hacer que el fondo sea infinito y vuelva a su posicion
    {
        float[] Scales = new float[] { -1f, 1f };
        this.transform.position = BackgroundReference.transform.position + StartDistance;//Se mueve detras del fondo que esta en camara
        this.transform.localScale.Set(Scales[(int)Random.Range(0, 1)], 1, 1);//Se invierte el fondo para hacerlo mas dinamico (Solo en eje x)
    }
}
