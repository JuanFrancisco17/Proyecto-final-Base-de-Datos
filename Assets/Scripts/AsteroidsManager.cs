using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    public float counter;
    public float maxTime;
    [SerializeField]
    private GameObject _asteroid;

    void Start()
    {
        maxTime = Random.Range(1, 8);
    }
    void Update()
    {
        //Cada cierto tiempo aleatorio se instancia un asteroide
        counter += Time.deltaTime;
        if (counter >= maxTime)
        {
            Instantiate(_asteroid, transform.position, Quaternion.identity);
            counter = 0;
            ChangeNumber();
        }
    }

    //Cambiamos el tiempo de forma aleatoria para la proxima instancia 
    void ChangeNumber()
    {
        maxTime = Random.Range(1, 10);
    }
}
