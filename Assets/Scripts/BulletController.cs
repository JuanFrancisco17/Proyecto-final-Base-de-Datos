using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("VARIABLES COMPONENTES")]

    //Rigidbody
    private Rigidbody2D _rb2d;

    [Header("VARIABLES FISICAS")]

    [Tooltip("Fuerza con la que el jugador dispara")]
    [SerializeField]
    private float _force;

    [Header("VARIABLES CONTADOR")]

    //Contador para que la bala desaparezca
    private float counter = 0;
    [Tooltip("Tiempo al que tiene que llegar el contador para que la bala desaparezca")]
    [SerializeField]
    float maxTime;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Añadimos la fuerza y dirección con la que irá la bala
        _rb2d.AddForce(Vector2.up * _force * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Update()
    {
        //El contador empieza a contar
        counter += Time.deltaTime;
        //Si el contador es mayor o igual al del tiempo maximo se resetea la fuerza de la bala y regresa al pool
        if (counter >= maxTime)
        {
            _rb2d.velocity = Vector2.zero;
            BulletPoolManager.Instance.ReturnOjectToPool(this.gameObject);
            counter = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si la bala choca con algo que no sea el jugador...
        if (collision.collider.tag != "Player")
        {
            //Reseteamos las fisicas
            _rb2d.velocity = Vector2.zero;

            //Regresamos la bala al final del pool
            BulletPoolManager.Instance.ReturnOjectToPool(this.gameObject);
        }
    }
}
