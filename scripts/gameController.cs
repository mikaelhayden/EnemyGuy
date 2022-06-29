using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Game controler é o script que vai controlar o jogo, estou usando ele para controlar o 
//nível de vida do personagem, tanto visual, quanto não visual

public class gameController : MonoBehaviour
{
    public int score;
    public int totalScore;
    

    public Text scoreText;
    public Text healthText;
    public Text flechasText;
    public Text pontuacao;

    public GameObject pauseObj;
    public GameObject gameOverObj;
    public GameObject end;

    public static gameController instance;


    private bool isPause;

    void Awake()    //é inicializado antes de todos os scripts
    {
        instance = this;
    }

    void Start()
    {
        score = PlayerPrefs.GetInt("scre");  
        scoreText.text = score.ToString();
        flechasText.text = 10.ToString();
    }

    // Update is called once per frame 
    // Update é chamado por frame "Um frame é uma atualização de tela. OnGUI() é chamado toda vez que a tela é atualizada."

    void Update()
    {
        pauseGame();
    }

    public void UpdateScore(int value)
    {
        score += value;
        PlayerPrefs.SetInt("scre", score);
        scoreText.text = score.ToString();
    }

    public void UpdateLives(int value)
    {
        PlayerPrefs.SetInt("lifes", value);
        value = PlayerPrefs.GetInt("lifes");
        healthText.text = "x "+value.ToString(); //Texto do canvas, as vidas
    }

    public void UpdateBow(int value)
    {
        flechasText.text = value.ToString();
    }

    public void pauseGame() //função para salvar o jogo
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            pauseObj.SetActive(isPause);
        }

        if(isPause)
        {
            Time.timeScale = 0f;    //o que realmente faz pausar o jogo
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void resume()
    {
        isPause = !isPause;
        pauseObj.SetActive(isPause);
    }
    public void menuPause()
    {
        isPause = !isPause;
        pauseObj.SetActive(isPause);
        PlayerPrefs.SetInt("scre", 0);
        PlayerPrefs.SetInt("lifes", 1);
        SceneManager.LoadScene(0);
    }

    public void endScreen()
    {
        int valor;
        valor = PlayerPrefs.GetInt("scre");
        end.SetActive(true);
        pontuacao.text = "Pontuação: " + valor.ToString();
    }

    public void gameOver()
    {
        gameOverObj.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }

}
