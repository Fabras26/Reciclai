using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    public Text nomefase;
    [Header("Contador de Resíduos")]
    [SerializeField]
    private GameObject painel;
    [SerializeField]
    private GameObject iconeResiduo;
    [SerializeField]
    private Sprite[] tipoResiduo;


    private int indexResiduoParaMudar = 0;

    public void SetInicialContadorResiudos(int cont)
    {
        for (int i = 0; i < cont; i++)
        {
            int r = Random.Range(0, tipoResiduo.Length);
            var residuo = Instantiate(iconeResiduo, painel.transform); ;
            residuo.GetComponent<Image>().sprite = tipoResiduo[r];
            residuo.GetComponent<ResiduoInventario>().tipo = r;
        }
        GameController.Instance.tipoResiduo = painel.transform.GetChild(indexResiduoParaMudar).GetComponent<ResiduoInventario>().tipo;

        NumeroFase(nomefase.gameObject);
    }
    public void DiminuirContagemDeResiduos()
    {
        painel.transform.GetChild(indexResiduoParaMudar++).GetComponent<Image>().color = Color.black;

        GameController.Instance.tipoResiduo = painel.transform.GetChild(indexResiduoParaMudar).GetComponent<ResiduoInventario>().tipo;

    }
    public void NumeroFase(GameObject nome)
    {

        nome.GetComponent<Text>().text = ("Fase - " + PlayerPrefs.GetInt("NumeroFase"));
        
    }
    public void Pontuacao(int ponto, GameObject texto)
    {
        texto.GetComponent<Text>().text = (ponto.ToString());
    }
}
