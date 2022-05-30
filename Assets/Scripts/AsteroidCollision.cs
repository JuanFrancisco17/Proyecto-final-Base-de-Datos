using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si collisiona con una bala este se destruye y asigna 100 puntos
        if (collision.collider.tag == "Bullet")
        {
            PointsController.Instance.points += 100;
        }

        //si choca con el jugador pierde la partida
        if (collision.collider.tag == "Player")
        {
            GameOverManager.Instance.GameOver();
        }

        //El asteroide se destruye
        Destroy(this.gameObject);
    }
}
