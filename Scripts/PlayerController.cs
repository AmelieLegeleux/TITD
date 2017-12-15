using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayerController : NetworkBehaviour
{
	float currentRotationX, currentRotationY;
	float xRotationV, yRotationV;
	float xRotation, yRotation;         // Mouvement souris
	float lookSensitivity = 2;     // Sensibilité regard
	float lookSmoothness = 0.1f;
	public float xTranslation, zTranslation;   // Mouvement du joueur
	float vitesseLaterale = 2;       // Vitesse joueur en latéral
	float vitesseMarche;         // Vitesse marche joueur
	public Animator anim;                      // State machine
    Animations animScript;
    public bool speed;
    int mass;
    int force;
    Vector3 vectorTrans;
    bool enableMove = true;
    public Camera maCamera;
    public GameObject corps;
    public GameObject head;
    private bool displayHelp;
    private bool availableChat;
    private bool activeChat;
    private Help help;
    

    public Animator True { get; private set; }

    void OnDisconnectedFromServer()
    {
        afficheSourie(true);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Camera>().depth = 1;
        //masquage et bloquage de la sourie sur instanciation du player
        afficheSourie(false);
        //suppression de l'UI network à l'instantiation du player
        GameObject.Find("Network Manager").GetComponent<NetworkManagerHUD>().showGUI = false;
        corps.SetActive(false);
        anim = GetComponent<Animator>();
        animScript = new Animations();
        vitesseMarche = 2;
        mass = (int)GetComponent<Rigidbody>().mass;
        force = 70;
        vectorTrans.Set(0, 0, 0);
        displayHelp = false;
        availableChat = true;
        activeChat = false;
        help = GetComponent<Help>();
    }

    private void afficheSourie(bool etat)
    {
        Cursor.visible = etat;
        enableMove = !etat;
        Cursor.lockState = etat == true ? CursorLockMode.None : CursorLockMode.Locked;
    }
    void Update()
	{
		if (isLocalPlayer)
		{
            if(!activeChat && Input.GetKeyDown(KeyCode.H))
            {
                if(displayHelp)
                {
                    help.CloseHelp();
                    enableMove = true;
                    availableChat = true;
                }
                else
                {
                    enableMove = false;
                    availableChat = false;
                    help.DisplayHelp();
                }
                displayHelp = !displayHelp;
            }


			if (enableMove)
			{
				// Mouvement translation
				xTranslation = Input.GetAxis("Horizontal");
				zTranslation = Input.GetAxis("Vertical");

                // Mouvement rotation
                yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
                xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;

                // Speed
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = true;
                    //vitesseMarche = 4;
                    force = 240;
                }
                else
                {
                    speed = false;
                    //vitesseMarche = 2;
                    force = 70;
                }
                // Activation animation
                animScript.AnimMove(anim, xTranslation, zTranslation,speed);

                // Mouvement joueur
                //transform.Translate(xTranslation * vitesseLaterale * Time.deltaTime, 0, zTranslation * vitesseMarche * Time.deltaTime);
                if (xTranslation > 0) vectorTrans.x = mass * force;
                else if (xTranslation < 0) vectorTrans.x = mass * (-force);
                else vectorTrans.x = 0;
                if (zTranslation > 0) vectorTrans.z = mass * force;
                else if (zTranslation < 0) vectorTrans.z = mass * (-force);
                else vectorTrans.z = 0;
                GetComponent<Rigidbody>().AddRelativeForce(vectorTrans);



				xRotation = Mathf.Clamp(xRotation, -80, 100);
				currentRotationX = Mathf.SmoothDamp(currentRotationX, xRotation, ref xRotationV, lookSmoothness);
				currentRotationY = Mathf.SmoothDamp(currentRotationY, yRotation, ref yRotationV, lookSmoothness);

				transform.rotation = Quaternion.Euler(0, yRotation, 0);
                head.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
//                maCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
			}

            //sur appuis de escape, on active/desactive la sourie
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                afficheSourie(!Cursor.visible);
            }

            //sur appuis de la touche entrée
            if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && availableChat)
            {
                string textChat = GameObject.Find("InputFieldChat").GetComponent<InputField>().text;
                if (textChat != "")
                {
                    enableMove = true;
                    activeChat = false;
                    GameObject.Find("Chat").GetComponent<Chat>().SendMessage(GameObject.Find("InputFieldChat").GetComponent<InputField>());
                    GameObject.Find("InputFieldChat").GetComponent<InputField>().text = "";
                }
                else
                {
                    enableMove = false;
                    activeChat = true;
                    GameObject.Find("InputFieldChat").GetComponent<InputField>().ActivateInputField();
                }
            }
        }
    }

    private void AnimMove(Animator anim, float xTranslation, float zTranslation)
    {
        anim.SetFloat("Forward", zTranslation);
        anim.SetFloat("Strafe", xTranslation);
        if (xTranslation == 0 && zTranslation == 0)
        {
            anim.SetFloat("Idle", 0);
        }

        if (xTranslation > 0)
        {
            anim.SetFloat("Walk", 0);
        }
    }
}
/*
public class PlayerController : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
	public GameObject head;
	public Camera maCamera;
    public GameObject corps;

    bool enableMove = true;

    float yRotation;
	float xRotation;
	float lookSensitivity = 2;
	float currentXRotation;
	float currentYRotation;
	float yRotationV;
	float xRotationV;
	float lookSmoothnes = 0.1f; 
	
	public float vitesseLaterale = 20.0f;
	public float vitesseMarche = 20.0f;

    public float x;
    public float z;

    void OnDisconnectedFromServer()
    {
        afficheSourie(true);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Camera> ().depth = 1;
        //masquage et bloquage de la sourie sur instanciation du player
        afficheSourie(false);
        //suppression de l'UI network à l'instantiation du player
        GameObject.Find("Network Manager").GetComponent<NetworkManagerHUD>().showGUI = false;
        corps.SetActive(false);
    }

    private void afficheSourie(bool etat)
    {
        Cursor.visible = etat;
        enableMove = !etat;
        Cursor.lockState = etat == true ? CursorLockMode.None  : CursorLockMode.Locked;
    }

    void Update()
	{
        if (isLocalPlayer)
		{
            if (enableMove)
            {
                yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
                xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
                xRotation = Mathf.Clamp(xRotation, -80, 100);
                currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
                currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothnes);
			 
                head.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
                transform.rotation = Quaternion.Euler(0, yRotation, 0);

//                var x = Input.GetAxis("Horizontal") * Time.deltaTime * vitesseLaterale;
  //              var z = Input.GetAxis("Vertical") * Time.deltaTime * vitesseMarche;
  //              var x;
  //              var z;
                x = Input.GetAxis("Horizontal") * vitesseLaterale;
                z = Input.GetAxis("Vertical") *  vitesseMarche;
                //                transform.Translate(x, 0, 0);
                //                transform.Translate(0, 0, z);

//                GetComponent<Rigidbody>().AddForce(x, 0, z, ForceMode.Acceleration);

                GetComponent<Rigidbody>().AddForce(Vector3.forward* x);
            }

            //sur appuis de escape, on active/desactive la sourie
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                afficheSourie(!Cursor.visible);
            }

            //sur appuis de la touche entrée
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                string textChat = GameObject.Find("InputFieldChat").GetComponent<InputField>().text;
                if (textChat != "")
                {
                    enableMove = true;
                    GameObject.Find("Chat").GetComponent<Chat>().SendMessage(GameObject.Find("InputFieldChat").GetComponent<InputField>());
                    GameObject.Find("InputFieldChat").GetComponent<InputField>().text="";
                }
                else
                {
                    enableMove = false;
                    GameObject.Find("InputFieldChat").GetComponent<InputField>().ActivateInputField();
                }
            }

        }
    }
}
*/