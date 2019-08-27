using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour {

    float xmov;
    float zmov;
    [Header("Velocidade do Player")]
    public float speed;

    [Header("Camera que aponta para o jogador")]
    public Camera cam;

    [Header("GameObject que contém o LineRenderer")]
    public GameObject playerAim;
    public LineRenderer line;

    [Header("GameObject Painel de Game Over")]
    public GameObject panelGameOver;

    [Header("Posicao do mouse de acordo com o Raycast no chao")]
    public Vector3 mousePos;
    Rigidbody rb;
    [Header("Estado do Jogador")]
    public bool defaultState;


    [Header("Velocidade para mirar em direção ao mouse")]
    public float smoothVelocity;

    //[Header("Line Render para fazer o Lazer")]
    //LineRenderer line;

    [Header("GameObject da Mira no Canvas")]
    public GameObject aimObj;
    RectTransform aimRect;
    public Texture2D aimTexture;

    [Header("Vida do player")]
    public float vida;
    [Tooltip("Colocar Slider da barra de vida")] public Slider healthBar;

    //layer do chao
    int floorMask;
    //Raycast que aponta para o chao
    RaycastHit hitInfo;
    //Raycast do Laser do jogador
    RaycastHit hitLaser;
    //rotacao do jogador
    Quaternion rot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        floorMask = LayerMask.GetMask("Floor");
        aimRect = aimObj.GetComponent<RectTransform>();
        vida = 100f;
        line = GetComponentInChildren<LineRenderer>();

        //Desabilitar o mouse.
        Cursor.visible = false;

        //Seta o estado padrao para verdadeiro
        defaultState = true;

        // Muda a sprite do cursor.
        //Cursor.SetCursor(aimTexture, Vector3.zero, CursorMode.Auto);
    }

	void FixedUpdate () {
        healthBar.value = vida / 100f;
        if (defaultState)
        {
            xmov = Input.GetAxis("Horizontal");
            zmov = Input.GetAxis("Vertical");


            Vector3 movement = new Vector3(xmov, 0f, zmov);
            rb.velocity = movement * speed;

            TurnCharacter();
        }

        AimCharacter();
        GameOver();
    }

    /// <summary>
    /// Vira o Jogador para a posição do mouse
    /// </summary>
    void TurnCharacter()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        /*Ray ray = new Ray(cam.transform.position, new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,
                                                              transform.position.y,
                                                              cam.ScreenToWorldPoint(Input.mousePosition).y));
                                                              */

        if (Physics.Raycast(ray, out hitInfo, 1000f, floorMask))
        {
            mousePos = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);

            rot = Quaternion.LookRotation(mousePos);
            
        }

        //transform.rotation = Quaternion.Lerp(transform.rotation, rot, smoothVelocity * Time.smoothDeltaTime);
        transform.LookAt(mousePos, Vector3.up);
    }

    /// <summary>
    /// esta funcao precisa de correcoes
    /// </summary>
    void AimCharacter()
    {
        aimRect.position = Input.mousePosition;

        //Linha para verificar o Raycast
        Debug.DrawLine(playerAim.transform.position, playerAim.transform.position + transform.forward, Color.red);

        Ray ray = new Ray(playerAim.transform.position, playerAim.transform.position + transform.forward);
        if (Physics.Raycast(ray, out hitLaser, 100f, floorMask))
        {
            line.SetPosition(1, line.GetPosition(1) + new Vector3(0f, 0f, hitLaser.distance));
        }
        else
        {
            //seta a posicao final do Line Renderer para 50 no eixo Z
            //line.SetPosition(1, new Vector3(0f, 0f, 50f));
        }

    }

    /// <summary>
    /// Método para tomar dano do inimigo
    /// </summary>
    /// <param name="_damage"></param>
    public void TakeDamage(float _damage)
    {
        vida -= _damage;
    }

    /// <summary>
    /// Método para condição em que perde toda a vida, ativa o painel de GAME OVER
    /// </summary>
    void GameOver()
    {
        if (vida <= 0f)
        {
            panelGameOver.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }
}
