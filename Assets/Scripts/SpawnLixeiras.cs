using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLixeiras : MonoBehaviour
{
    [System.Serializable]
    public class NumeroSpawns
    {
        public float rotacão;
        public bool Ocupado;
    }

    [SerializeField]
    private NumeroSpawns[] spawnsPos;
    public GameObject[] Lixeiras;
   // public GameObject[] spawnPositions;

    public GameObject globo;
    void Start()
    {
        for (int i =0; i < spawnsPos.Length; i++)
        {
            spawnsPos[i].rotacão = ((360f/spawnsPos.Length) * i);
        }
        GameObject lixeira;
        for(int i= 0; i<Lixeiras.Length; i++)
        {
            int a = Random.Range(0, spawnsPos.Length);

            while(spawnsPos[a].Ocupado == true)
            {
               
                a = Random.Range(0, spawnsPos.Length);
                Debug.Log("Estava ocupado");
            }
            lixeira = Instantiate(Lixeiras[i], transform.position, Quaternion.Euler(0,0,spawnsPos[a].rotacão));
            spawnsPos[a].Ocupado = true;
            lixeira.transform.parent = transform;
        }
        for (int i = 0; i< spawnsPos.Length; i++)
        {
            if (spawnsPos[i].Ocupado == false)
            {
                spawnsPos[i].Ocupado = true;
                lixeira = Instantiate(Lixeiras[Random.Range(0, Lixeiras.Length)], transform.position, Quaternion.Euler(0, 0, spawnsPos[i].rotacão));
                lixeira.transform.parent = transform;
            }
        }
       
    }
}
