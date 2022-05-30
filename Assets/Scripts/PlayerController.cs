using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PlayerController : MonoBehaviour
{
    [Header("SINGLETON")]

    private static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    private Rigidbody2D _rb2d;
    private Vector2 _input;

    [SerializeField]
    private float _speed;
    public GameObject shootSpawnPos;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //Metemos en un Vector2 la pulsación del teclado
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");


        if (Input.GetButtonDown("Fire1"))
        {
            //Si clicamos instanciamos del pool una bala
            BulletPoolManager.Instance.GetObjectFromPool();
        }
    }

    void FixedUpdate()
    {
        //Movimiento del personaje
        _rb2d.velocity = _input * _speed * Time.deltaTime;
    }

    public JObject Serialize()
    {
        //generamos un jobj que guarde las variables importantes, en este caso la posición del jugador
        JObject jobj = new JObject();
        jobj.Add("PlayerShipPos", JsonUtility.ToJson(transform.position));
        return jobj;
    }

    public void Deserialize(JObject jobj)
    {
        //para cargar la partida, deserializamos y asignamos el valor a cada variable de nuevo
        transform.position = JsonUtility.FromJson<Vector3>(jobj["PlayerShipPos"].ToString());
    }

}
