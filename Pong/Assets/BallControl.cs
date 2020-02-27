using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallControl : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    //besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    //mengembalikan bola ke tengah layar
    void ResetBall()
    {   
        //Reset posisi menjadi 0,0
        transform.position = Vector2.zero;

        //Reset kecepatan menjadi 0
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        //-----code tutorial float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        //tentukan nilai acak antara 0(inklusif) dan 2(ekslusif)
        float randomDirection = Random.Range(0, 6.0f);

        //jika nilainya dibawah 1, bola bergerak ke kiri atas
        //jika nilainya antara 1 samapi 2 , bola bergerak ke kiri
        //jika nilainya antara 2 sampai 3, bola bergerak ke kiri bawah
        //jika nilainya antara 3 sampai 4, bola bergerak ke kanan atas
        //jika nilainya antara 4 sampai 5, bola bergerak ke kanan
        //jika nilainya lebih dari 5, bola bergerak ke kanan bawah
        if (randomDirection < 1.0f)
        {
            //gunakan gaya untuk menggerakan bola ke kiri atas
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yInitialForce));
        }
        if (randomDirection > 1.0f && randomDirection < 2.0f)
        {
            //gaya untuk mendorong bola ke kiri
            rigidBody2D.AddForce(new Vector2(-xInitialForce, 0));
        }
        if (randomDirection > 2.0f && randomDirection < 3.0f)
        {
            //gaya untuk mendorong bola ke kiri bawah
            rigidBody2D.AddForce(new Vector2(-xInitialForce, -yInitialForce));
        }
        if (randomDirection > 3.0f && randomDirection < 4.0f)
        {
            //gaya untuk mendorong bola ke kanan atas
            rigidBody2D.AddForce(new Vector2(xInitialForce, yInitialForce));
        }
        if (randomDirection > 4.0f && randomDirection < 5.0f)
        {
            //gaya untuk mendorong bola ke kanan
            rigidBody2D.AddForce(new Vector2(xInitialForce, 0));
        }
        if (randomDirection > 5.0f)
        {
            //gaya untuk mendorong bola ke kanan bawah
            rigidBody2D.AddForce(new Vector2(xInitialForce, -yInitialForce));
        }
    }

    void RestartGame()
    {
        //kembalikan ke posisi semula
        ResetBall();

        //Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        //mulai game
        RestartGame();

        trajectoryOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
