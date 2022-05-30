using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PointsController : MonoBehaviour
{
    [Header("SINGLETON")]

    private static PointsController instance;

    public static PointsController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PointsController>();
            }
            return instance;
        }
    }

    public int points;
    [SerializeField]
    private Text pointsText;

    public Text nombreTXT;
    public GameObject rankingGO;

    void Update()
    {
        pointsText.text = "POINTS: " + points;
    }

    public void GuardarPuntosDB()
    {
        //Guardamos los puntos en la base de datos
        rankingGO.GetComponent<RankingManager>().InsertarPuntos(nombreTXT.text, points);
    }

    public JObject Serialize()
    {
        //generamos un jobj que guarde las variables importantes para poder cargar partida
        JObject jobj = new JObject();
        jobj.Add("score", JsonConvert.SerializeObject(points));
        return jobj;
    }

    public void Deserialize(JObject jobj)
    {
        //para cargar la partida, deserializamos y asignamos el valor a cada variable de nuevo
        points = JsonConvert.DeserializeObject<int>(jobj["score"].ToString());
    }
}
