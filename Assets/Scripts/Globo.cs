using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globo : MonoBehaviour
{
    [System.Serializable]
    private class ElementoDeRotacao
    {
        #pragma warning disable 0649
        public float Velociade;
        public float Duracao;
        #pragma warning restore 0649
    }

    [SerializeField]
    private ElementoDeRotacao[] padraoDeRotacao;
    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;

    [SerializeField]
    private GameObject spawns;

    bool cooldown;

    private void Start()
    {
        StartCoroutine(Rotacionar());
    }
    IEnumerator Rotacionar()
    {
        int rotationIndex = 0;
        while (true)
        {

            rotationIndex = Random.Range(0, padraoDeRotacao.Length);
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = (padraoDeRotacao[rotationIndex].Velociade) * PlayerPrefs.GetFloat("ModificadorVelocidadeGlobo");
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(padraoDeRotacao[rotationIndex].Duracao);
            rotationIndex++;
            rotationIndex = rotationIndex < padraoDeRotacao.Length ? rotationIndex : 0;

            Debug.Log(PlayerPrefs.GetFloat("ModificadorVelocidadeGlobo"));
        }
    }
   
      

}
