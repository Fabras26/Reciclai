using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Residuo : MonoBehaviour
{
    [SerializeField]
    private Vector2 forcaArremesso;

    public bool ativo = false;

    public int tipo;
    public ParticleSystem particulasAcerto;
    public ParticleSystem particulasErro;


    private Rigidbody2D rb;
    private BoxCollider2D residuoCollider;

    private Animator anim;

    private AudioPlayer audioP;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        residuoCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        audioP = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && ativo && GameObject.FindGameObjectWithTag("Controller").GetComponent<GameController>().pausado == false)
        {
            audioP.PlayAudio(audioP.arremesso);
            rb.AddForce(forcaArremesso, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ativo)
            return;
        ativo = false;
        rb.velocity = new Vector2(0, 0);
        rb.bodyType = RigidbodyType2D.Kinematic;
        residuoCollider.offset = new Vector2(residuoCollider.offset.x, 0.4f);

        this.transform.SetParent(collision.collider.transform);

        if (collision.gameObject.GetComponent<Lixeira>() != null)
        {
            if (tipo == collision.gameObject.GetComponent<Lixeira>().tipo)
            {
                Destruir();
                GameController.Instance.quantidadeResiduos--;
                GameController.Instance.pontos++;


                var particulas = Instantiate(particulasAcerto, transform.position, transform.rotation);
                particulas.transform.parent = collision.transform;
                GameController.Instance.GameUI.DiminuirContagemDeResiduos();
                GameController.Instance.AcertoResiduo();

            }
            else
            {
                audioP.PlayAudio(audioP.erro);

                GameController.Instance.fimDeJogo = true;
                Instantiate(particulasErro, transform.position, particulasErro.transform.rotation) ;
                Destruir();

            }
        }
        else
        {
            audioP.PlayAudio(audioP.erro);
            GameController.Instance.fimDeJogo = true;
            Instantiate(particulasErro, transform.position, particulasErro.transform.rotation);
            Destruir();


        }

    }
    public void Destruir()
    {
        anim.SetTrigger("Acerto");
        Destroy(gameObject, 3.5f);
    }
}
