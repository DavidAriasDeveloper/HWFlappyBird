using UnityEngine; // Esto son librerias
using System.Collections; // Esto son librerias

public class scr_Flappy : MonoBehaviour {

	Vector3 velocity = Vector3.zero;//Velocidad

    public float mass;

    private Vector3 weight;

	public float velFlutter;//Velocidad Aleteo

	bool flutter = false;
     
	public float velRotationMax;//Velocidad maxima
    //Importamos referencias de los demas GameObjects que interactuan con el pajaro
    public scr_Tubes tube1;
    public scr_Tubes tube2;
    public scr_Background bg;

    private Animator anim;//Animacion del pajaro
    //Variables de Inicio y Fin del juego
    public bool GameOver = false;
    public bool GameStart;

    public GameObject buttons;//Botones

    // Use this for initialization
    void Start () {
        anim = this.gameObject.GetComponent<Animator>();
        weight = new Vector3(0, mass * -9.8f, 0);
    }

    // Update is called once per frame
    void Update() {
        int numTaps = 0;
        foreach (Touch touch in Input.touches)
        {
            if(touch.phase != TouchPhase.Ended)
            {
                numTaps++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) | numTaps > 0) {
            GameStart = true;
            if (!GameOver)
            {
                flutter = true;//Va a aletear
            }
		}
	}

	void FixedUpdate(){
        if (GameStart)//Hasta que no se inicie el juego (No se clickee) no se va a aplicar la gravedad
        {
            velocity += weight * Time.deltaTime;

            if (flutter == true)
            {
                flutter = false;
                velocity.y = velFlutter;
            }

            float angle = 0;

            if (velocity.y >= 0)
            {
                angle = Mathf.Lerp(0, 25, velocity.y / velRotationMax);//Direccion hacia arriba
            }
            else
            {
                angle = Mathf.Lerp(0, -75, -velocity.y / velRotationMax);//Direccion hacia abajo
            }

            transform.position += velocity * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }  
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tube_up" | collision.gameObject.name == "Tube_down")
        {
            tube1.velocity = new Vector3(0, 0, 0);
            tube2.velocity = new Vector3(0, 0, 0);
            anim.SetTrigger("GameOver");
            bg.velocity = new Vector3(0,0,0);//El escenario dejara de moverse
            GameOver = true;
        }

        if (collision.gameObject.name == "Tube_down")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (collision.gameObject.name == "Floor_1" | collision.gameObject.name == "Floor_2")
        {
            weight = new Vector3(0, 0, 0);
        }
    }

    private void OnGUI()
    {
        if (!GameOver)
        {
            //Ocultamos los botones mientras no se termine el juego
            buttons.gameObject.SetActive(false);
        }
        else
        {
            //Mostramos los botones cuando se termine el juego
            buttons.gameObject.SetActive(true);
        }
    }
}