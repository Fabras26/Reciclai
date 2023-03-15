using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private AudioPlayer audioP;
    public int pontos;
    public bool fimDeJogo = false;
    public bool vitoria = false;

    public GameObject canvasFim;
    public GameObject canvasFase;

    public string proximaFase;
    [SerializeField]
    private GameObject particulasAcerto;
    private bool umavez;

    public bool pausado;

    public static GameController Instance { get; private set; }
 
    [SerializeField]
    public int quantidadeResiduos;
    [Header("Spawn Resíduos")]
    [SerializeField]
    private Vector2 ResiduoSpawnPosition;
    [SerializeField]
    private GameObject[] residuos;
    public int tipoResiduo;

    public GameUI GameUI { get; private set; }

    private void Awake()
    {
        Instance = this;
        GameUI = GetComponent<GameUI>();
        umavez = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioP = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
        GameUI.SetInicialContadorResiudos(quantidadeResiduos);
        pontos = PlayerPrefs.GetInt("Pontuacao");
        SpawnResiduo();
        if (GameObject.Find("Explicação") != null)
        {
            if (PlayerPrefs.GetInt("Tutorial") == 0)
            {
                GameObject.Find("Explicação").SetActive(false);
                pausado = false;
            }
            else
            {
                pausado = true;
            }
        }
        else
        {
            pausado = false;
        }
        
    }
    public void AcertoResiduo()
    {
        audioP.PlayAudio(audioP.acerto);
        SpawnResiduo();
    }
    public void SpawnResiduo()
    {
        var residuo = Instantiate(residuos[tipoResiduo], ResiduoSpawnPosition, Quaternion.identity);
        residuo.GetComponent<Residuo>().ativo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (quantidadeResiduos <= 0)
        {
            if (umavez == true)
            {
                StartCoroutine(Vencer());
            }
            umavez = false;
        }
       
        if (fimDeJogo)
        {
            FimDeJogo();
        }
        if(GameObject.Find("Pontuacao")!=null) GameUI.Pontuacao(pontos, GameObject.Find("Pontuacao"));

    }
    public void FimDeJogo()
    {
        canvasFase.SetActive(false);
        canvasFim.SetActive(true);
        if(pontos > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", pontos);
        }


        GameUI.NumeroFase(GameObject.Find("Fase"));
        GameUI.Pontuacao(PlayerPrefs.GetInt("Highscore"), GameObject.Find("NumRecorde"));
        GameUI.Pontuacao(pontos, GameObject.Find("Pontuacao"));
    }
    public void Pausar()
    {
        pausado = true;
        canvasFase.SetActive(false);
    }
    public void Despausar()
    {
        canvasFase.SetActive(true);
        pausado = false;
        audioP.PlayAudio(audioP.clique);
    }
    public void Reinicio()
    {
        audioP.PlayAudio(audioP.clique);
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            DesativarTutorial();
        }
        SceneManager.LoadScene("Fase 1");
        PlayerPrefs.SetFloat("ModificadorVelocidadeGlobo", 1);
        PlayerPrefs.SetInt("NumeroFase", 1);
        PlayerPrefs.SetInt("Pontuacao", 0);
    }
    public void NovaCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }
    public void AvançarFase()
    {
        PlayerPrefs.SetFloat("ModificadorVelocidadeGlobo", PlayerPrefs.GetFloat("ModificadorVelocidadeGlobo") + 0.05f);
        PlayerPrefs.SetInt("NumeroFase", PlayerPrefs.GetInt("NumeroFase") + 1);
        PlayerPrefs.SetInt("Pontuacao", pontos);
        AtivarTutorial();
    }
    IEnumerator Vencer()
    {
        audioP.PlayAudio(audioP.faseCompleta);
        var particulas = Instantiate(particulasAcerto, transform.position, transform.rotation);
        particulas.transform.localScale = new Vector3(3, 3, 3);
        GameObject.Find("Globo").GetComponent<Animator>().SetBool("vitoria", true);
        yield return new WaitForSeconds(1f);

        AvançarFase();
        NovaCena(proximaFase);
    }
    public void AtivarTutorial()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
    }
    public void DesativarTutorial()
    {
        PlayerPrefs.SetInt("Tutorial", 0);
    }
    public void Sair()
    {
        Application.Quit();
    }
}
