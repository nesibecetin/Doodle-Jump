using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.Android;

public class Karakter_Contoller : MonoBehaviour {

    public Text score_panel;
    public float sag_sol_hiz;
    public float ziplama_gucu;
    //Thread
    float tempY = -999f;
    float colliderY;
    bool colliderBlock = false;
    bool DegdiMi = false;
    
    float timer;
    float score_temp = 0;
    float start_y;
    int random;
    int plt_no = 0;
    int score = 0;
    public GameObject Arkaplan, Temizleyici;
    public Camera main_camera;
    Vector3 ArkaplanKonum;
    Vector2 platform_konumu;
    Vector2 camera_konum;
    public List<GameObject> Platformlar;
    public List<GameObject> Nesneler;
    public List<GameObject> Arkaplanlar;


    
	// Use this for initialization
	void Start () {
        score_temp = this.gameObject.transform.position.y;
        start_y = this.gameObject.transform.position.y;
        ArkaplanKonum = new Vector3(2.24f, 0, 0);
       // StartCoroutine(temizle());

    }

    
	// Update is called once per frame
	void Update () {
        
        random = Random.Range(0, 100);
        if (random<15)
        {
            plt_no = 1;
        }
        else if (random>80)
        {
            plt_no = 2;
        }

        camera_konum = new Vector2 (main_camera.transform.position.x, main_camera.transform.position.y-10f);
       
        if(score_temp<this.gameObject.transform.position.y)
        {
            score_temp = this.gameObject.transform.position.y;
            score_panel.text = "Score : "+Mathf.Ceil((score_temp - start_y)).ToString();
            score = (int) Mathf.Ceil((score_temp - start_y));
            PlayerPrefs.SetInt("Score", score);

            
            for (int i = 0; i< Nesneler.Count;i++)
            {
                if ( Nesneler[i] != null && Nesneler[i].gameObject.transform.position.y < camera_konum.y)
                {
                    Destroy(Nesneler[i]);
                }
            }
            for (int i = 0; i < Arkaplanlar.Count; i++)
            {
                if (Arkaplanlar[i] != null && Arkaplanlar[i].gameObject.transform.position.y < camera_konum.y)
                {
                    Destroy(Arkaplanlar[i]);
                }
            }
        }

       if(DegdiMi)
        {
            if(tempY < this.gameObject.transform.position.y)
            {
                tempY = this.gameObject.transform.position.y;
            }
            else if(tempY>this.gameObject.transform.position.y)
            {
    
                tempY = -999;
                DegdiMi = false;
                colliderBlock = false;
            }


        }
        Temizleyici.transform.position = new Vector3 (main_camera.transform.position.x, main_camera.transform .position.y - 7f);
        //if(colliderBlock)
        //{
        //    timer += Time.deltaTime;
        //    if(timer >= 1.5f)
        //    {
        //        colliderBlock = false;
        //        timer = 0;
        //    }
        //}


        if (Input.GetKey(KeyCode.A))
        {
            Teleport();
            Sol_Kay();
        }
        if (Input.GetKey(KeyCode.D))
        {
            Teleport();
            Sag_Kay();
        }

    }
    public void Sag_Kay()
    {
        this.gameObject.transform.Translate(new Vector2(sag_sol_hiz*Time.deltaTime, 0));
    }
    public void Sol_Kay()
    {
        this.gameObject.transform.Translate(new Vector2(-sag_sol_hiz* Time.deltaTime, 0));
    }


    public void Ziplama()
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, ziplama_gucu));
        
    }

    public void Teleport()
    {
        if(this.gameObject.transform.position.x <= -3.6f)
        {
            this.gameObject.transform.position = new Vector2(3.6f, this.gameObject.transform.position.y);
        }else if(this.gameObject.transform.position.x >= 3.6f)
        {
            this.gameObject.transform.position = new Vector2(-3.6f, this.gameObject.transform.position.y);
        }
    }

    //OnCollisionEnter2D //Obje Collider ile temas ettiği zaman
    //OnCollisionExit2D //Obje Collider'dan teması bittiği zaman
    //OnCollisionStay2D //Obje Collider ile temasta kaldığı zaman
    void OnCollisionEnter2D(Collision2D col)
    {
        if((col.gameObject.tag == "platform" || col.gameObject.tag =="kirilan_platform")&& !colliderBlock)
        {


            Ziplama();
            DegdiMi = true;
            colliderBlock = true;
            tempY = -999;

            if (col.gameObject.tag == "kirilan_platform")
            {
                Destroy(col.gameObject, 1.3f);
                bool isReverse = true;
                float randomSayi = 1;
                for (int i = 0; i < col.gameObject.transform.childCount; i++)
                {
                    randomSayi = Random.Range(1f, 3f);

                    if (!isReverse)
                    {
                        randomSayi *= -1;
                        
                    }
                    isReverse = false;


                    col.gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    col.gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomSayi * 50, 0));
                }
            }
            
        }

        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "arkaplan")
        {
            ArkaplanKonum.y = ArkaplanKonum.y+  10.2f;
            //Debug.Log("arkaplana carpti");
            GameObject ark_temp;
            ark_temp = Instantiate(Arkaplan, ArkaplanKonum, Quaternion.identity);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Arkaplanlar.Add(ark_temp);


            for (int i = 0; i<=10;i++)
            {
                if (score_temp <= 9999)
                {
                    platform_konumu.x = Random.Range(-3.2f, 3.2f);
                    platform_konumu.y += 2.5f;
                    GameObject temp_GameObject;
                    temp_GameObject = Instantiate(Platformlar[plt_no], platform_konumu, Quaternion.identity);
                    Nesneler.Add(temp_GameObject);
                    plt_no = 0;
                }
                //else if (random<15)
                //{
                //    platform_konumu.x = Random.Range(-3.2f, 3.2f);
                //    platform_konumu.y += 1f;
                //    Instantiate(Platformlar[1], platform_konumu, Quaternion.identity);
                //}
                
                
            }
        }
    }






}
