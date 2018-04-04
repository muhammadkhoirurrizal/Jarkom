using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Sprite[] SprGambar;
    public Image[] Gambar;
    public GameObject[] BtnMove;

    public bool Turn = false;

    public int NoGambar;

    public int[] Prev;

    public static GameManager Instance;

    public string YourName, EnemyName;

    int Rand,EnemyRand;
    bool Ready,EnemyReady;

    public Text[] Dice;
    public GameObject btnDice;

    public GameObject[] UIGame;

    public int ManyTurn=0;

    public AudioClip Lose;
    public AudioSource Play;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        YourName = PlayerPrefs.GetString("YourName");
        EnemyName = PlayerPrefs.GetString("EnemyName");
        Dice[0].text = YourName + " Dice";
        Dice[1].text = EnemyName + " Dice";
    }

    public void Change(int No)
    {
        if (NoGambar==0)
        {
            Gambar[No].sprite = SprGambar[1];
        }
        else
        {
            Gambar[No].sprite = SprGambar[0];
        }

        BtnMove[No].SetActive(false);
        Turn = true;
        Dice[2].text = YourName+" Turn";
    }

    void CheckWin()
    {
        for (int a =0;a<Prev.Length;a++)
        {
            if (Prev[0]==NoGambar&& Prev[1] == NoGambar && Prev[2] == NoGambar)
            {
                UIGame[1].SetActive(false);
                UIGame[2].SetActive(true);
                Dice[3].text = YourName+" Win";
                NetworkManager.Instance.streamWriter.WriteLine("Hasil");
                NetworkManager.Instance.streamWriter.Flush();
            }
            else if (Prev[3] == NoGambar && Prev[4] == NoGambar && Prev[5] == NoGambar)
            {
                UIGame[1].SetActive(false);
                UIGame[2].SetActive(true);
                Dice[3].text = YourName + " Win";
                NetworkManager.Instance.streamWriter.WriteLine("Hasil");
                NetworkManager.Instance.streamWriter.Flush();
            }
            else if (Prev[6] == NoGambar && Prev[7] == NoGambar && Prev[8] == NoGambar)
            {
                UIGame[1].SetActive(false);
                UIGame[2].SetActive(true);
                Dice[3].text = YourName + " Win";
                NetworkManager.Instance.streamWriter.WriteLine("Hasil");
                NetworkManager.Instance.streamWriter.Flush();
            }
            else if (Prev[0] == NoGambar && Prev[3] == NoGambar && Prev[6] == NoGambar)
            {
                UIGame[1].SetActive(false);
                UIGame[2].SetActive(true);
                Dice[3].text = YourName + " Win";
                NetworkManager.Instance.streamWriter.WriteLine("Hasil");
                NetworkManager.Instance.streamWriter.Flush();
            }
            else if (Prev[1] == NoGambar && Prev[4] == NoGambar && Prev[7] == NoGambar)
            {
                UIGame[1].SetActive(false);
                UIGame[2].SetActive(true);
                Dice[3].text = YourName + " Win";
                NetworkManager.Instance.streamWriter.WriteLine("Hasil");
                NetworkManager.Instance.streamWriter.Flush();
            }
            else if (Prev[2] == NoGambar && Prev[5] == NoGambar && Prev[8] == NoGambar)
            {
                UIGame[1].SetActive(false);
                UIGame[2].SetActive(true);
                Dice[3].text = YourName + " Win";
                NetworkManager.Instance.streamWriter.WriteLine("Hasil");
                NetworkManager.Instance.streamWriter.Flush();
            }
            else if (Prev[0] == NoGambar && Prev[4] == NoGambar && Prev[8] == NoGambar)
            {
                UIGame[1].SetActive(false);
                UIGame[2].SetActive(true);
                Dice[3].text = YourName + " Win";
                NetworkManager.Instance.streamWriter.WriteLine("Hasil");
                NetworkManager.Instance.streamWriter.Flush();
            }
            else if (Prev[2] == NoGambar && Prev[4] == NoGambar && Prev[6] == NoGambar)
            {
                UIGame[1].SetActive(false);
                UIGame[2].SetActive(true);
                Dice[3].text = YourName + " Win";
                NetworkManager.Instance.streamWriter.WriteLine("Hasil");
                NetworkManager.Instance.streamWriter.Flush();
            }
        }
    }

    public void Win()
    {
        UIGame[1].SetActive(false);
        UIGame[2].SetActive(true);
        Dice[3].text = EnemyName + " Win";
        Play.PlayOneShot(Lose);
    }

    public void Draw()
    {
        UIGame[1].SetActive(false);
        UIGame[2].SetActive(true);
        Dice[3].text = "Draw";
    }

    public void RequestRand()
    {
        Rand = Random.RandomRange(0, 20);
        NetworkManager.Instance.streamWriter.WriteLine("Turn" + "." + Rand );
        NetworkManager.Instance.streamWriter.Flush();
        Dice[0].text = YourName +" Dice : " + Rand;
        btnDice.SetActive(false);
        Ready = true;
        if (EnemyReady)
        {
            if (Rand < EnemyRand)
            {
                UIChange();
                Turn = true;
                NoGambar = 0;
                Dice[2].text = YourName+" Turn";
            }
            else if (Rand == EnemyRand)
            {
                btnDice.SetActive(true);
            }
            else
            {
                UIChange();
                Turn = false;
                NoGambar = 1;
                Dice[2].text = EnemyName + " Turn";
            }
        }
    }

    public void UIChange()
    {
        UIGame[0].SetActive(false);
        UIGame[1].SetActive(true);
    }

    public void DependTurn(int RandEnemy)
    {
        Dice[1].text = EnemyName + " Dice : " + RandEnemy;
        EnemyRand = RandEnemy;
        EnemyReady = true;
        if (Ready)
        {
            if (Rand < EnemyRand)
            {
                UIChange();
                Turn = true;
                NoGambar = 0;
                Dice[2].text = YourName +" Turn";
            }
            else if (Rand == EnemyRand)
            {
                btnDice.SetActive(true);
            }
            else
            {
                UIChange();
                Turn = false;
                NoGambar = 1;
                Dice[2].text = EnemyName + " Turn";
            }
        }
    }
    // Use this for initialization
    public void PilihMove(int Pilih)
    {
        if (Turn)
        {
            NetworkManager.Instance.streamWriter.WriteLine("Pilihan" + "." + Pilih);
            NetworkManager.Instance.streamWriter.Flush();
            if (Pilih == 0)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            else if (Pilih == 1)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            else if (Pilih == 2)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            else if (Pilih == 3)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            else if (Pilih == 4)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            else if (Pilih == 5)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            else if (Pilih == 6)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            else if (Pilih == 7)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            else if (Pilih == 8)
            {
                Gambar[Pilih].sprite = SprGambar[NoGambar];
                BtnMove[Pilih].SetActive(false);
                Prev[Pilih] = NoGambar;
                CheckWin();
            }
            Turn = false;
            Dice[2].text = EnemyName +" Turn";
            ManyTurn++;
            if (ManyTurn>=5)
            {
                NetworkManager.Instance.streamWriter.WriteLine("Draw");
                NetworkManager.Instance.streamWriter.Flush();
                Draw();
            }
        }
    }

    public void BtnRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void BtnExit()
    {
        NetworkManager.Instance.streamWriter.WriteLine("Exit");
        NetworkManager.Instance.streamWriter.Flush();
        Application.Quit();
    }
}
