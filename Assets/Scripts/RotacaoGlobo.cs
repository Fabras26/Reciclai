using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoGlobo : MonoBehaviour
{

    [System.Serializable]
    private class PadrãoRotação
    {
#pragma warning disable 0649
        public float velocidade;
        public float duracao;
#pragma warning restore 0649
    }

    [SerializeField]
    private PadrãoRotação[] padraoDeRotacao;

    public float velocidadeAtual;
    public float velocidadeAnterior;
    // Start is called before the first frame update
    void Start()
    {
        velocidadeAtual = padraoDeRotacao[0].velocidade;

        StartCoroutine(Rotacionar());
    }
    IEnumerator Rotacionar()
    {
        int rotationIndex = 0;
        while (true)
        {
            velocidadeAnterior = velocidadeAtual;
            rotationIndex = Random.Range(0, padraoDeRotacao.Length);
            yield return new WaitForFixedUpdate();
            velocidadeAtual = padraoDeRotacao[rotationIndex].velocidade;
            yield return new WaitForSecondsRealtime(padraoDeRotacao[rotationIndex].duracao);
         
            Debug.Log(PlayerPrefs.GetFloat("ModificadorVelocidadeGlobo") + "Mudou de velocidade");
        }
    }

    // Update is called once per frame
    void Update()
    {
        velocidadeAnterior = Mathf.Lerp(velocidadeAnterior, velocidadeAtual, 0.005f);
        transform.Rotate(Vector3.forward * velocidadeAnterior * Time.deltaTime * PlayerPrefs.GetFloat("ModificadorVelocidadeGlobo"));
    }
}
