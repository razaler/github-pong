using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerControl : MonoBehaviour
{
    //tombol ke atas
    public KeyCode upButton = KeyCode.W;

    //tombol ke bawah
    public KeyCode downButton = KeyCode.S;

    //kecepatan gerak
    public float speed = 10.0f;

    //batas atas dan bawah game scene (batas bawah menggunakan minus (-)_)
    public float yBoundary = 9.0f;

    //Rigidbody 2D raket ini
    private Rigidbody2D rigidBody2D;

    //skor pemain
    private int score;

    //waktu melempar chance apakah terjadi power  up atau tidak
    private int timeOn;

    //waktu power up mati
    private int timeOff;

    //menyalakan time power up
    private bool powerUp = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //nilai kecepatan raket realtime
        Vector2 velocity = rigidBody2D.velocity;

        //jika pemain menekan tombol atas, raket bergerak ke sumbu y positif
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        
        //jika pemain menekan tombol bawah, raket bergerak ke sumbu y negatif
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        //jika pemain tidak menekan tombol apa-apa , kecepatannya nol
        else
        {
            velocity.y = 0.0f;
        }

        // masukkkan kembali kecepatannya ke rigidBody2D
        rigidBody2D.velocity = velocity;

        //dapatkan posisi raket sekarang
        Vector3 position = transform.position;

        //jika posisi raket melewat batas atas (yBoundary), kembalikan kebatas atas tersebut
        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }

        //jika posisi raket melewati batas bawah, kembalikan ke batas bawah tersebut
        if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        //masukkan kembali posisinya ke transform
        transform.position = position;

        //timeOn terus bertambah sepanjang frame berjalan
        timeOn++;
        if (timeOn == 1000)
        {
            //saat timeOn mencapai nilai 2000, melempar chance apakah player mendapat powerUp atau tidak
            float chancePower = Random.Range(0, 3.0f);
            //jika nilai chance diantara 1.0 sampai dengan 2.0 , player akan mendapat power up
            if(chancePower>=1.0f && chancePower <= 2.0f)
            {
                //power up menyala
                powerUp = true;

                //perbesar ukuran raket
                rigidBody2D.transform.localScale = new Vector3(1, 2, 1);
            }
            //mereset nilai variabel timeOn
            timeOn = 0;
        }


        //jika nilai powerUp true
        if (powerUp == true)
        {
            timeOff++;
            if (timeOff == 500)
            {
                //kembali ke ukuran normal
                rigidBody2D.transform.localScale = new Vector3(1, 1, 1);
                //power up dimatikan
                powerUp = false;
                //mereset nilai timeOff
                timeOff = 0;
            }
        }
    }

    //menaikkan skor sebanyak 1 poin
    public void IncrementScore()
    {
        score++;
    }

    //mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    //mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }

    
    //--------------------------------debug
    // Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;

    // Untuk mengakses informasi titik kontak dari kelas lain
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    // Ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
}
