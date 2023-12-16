using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CoGame : MonoBehaviour
{

    public GameObject piece;  
    public GameObject Raser;
    public GameObject black_laser, white_laser;
    public Transform WhiteLaserPos, BlackLaserPos, WhiteLaserNPos, BlackLaserNPos;
    public TMP_Text messageText;
    public Button mainbutton;
    // 체스판을 좌표계 전환 후 세팅 과정
    private GameObject[,] position = new GameObject[8, 8]; 
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private float whitelaserAng, blacklaserAng;


    //일단 플레이어 흰색으로 시작
    private string currentPlayer = "white";
    
    
    //게임오버 조건이 되면 true로 전환
    private bool gameOver = false;
    
    public float m_DoubleClickSecond = 0.25f;
    

    // Start is called before the first frame update
    void Start()
    {
       
        //프리팹 소환 -> 2d게임이지만 Vector3 기용
        playerWhite = new GameObject[] {
        Create("white_tri_1", 4 , 0), Create("white_splate", 7 , 0),Create(white_laser,"white_laser",0,0),
        Create("white_squre", 0 , 2), Create("white_king", 0 , 3), Create("white_squre", 0 , 4), Create("white_tri_2", 0 , 5),
        Create("white_tri_3", 3 , 3), Create("white_tri_2", 3 , 4), Create("white_tri_1", 7 , 6)
        
    };
        playerBlack = new GameObject[] {
        Create("black_tri_3", 3 , 7), Create("black_splate", 0 , 7),Create(black_laser,"black_laser",7,7),
        Create("black_squre", 7 , 3), Create("black_king", 7 , 4), Create("black_squre", 7 , 5), Create("black_tri_2", 7 , 2),
        Create("black_tri_2", 4 , 3), Create("black_tri_1", 4 , 4), Create("black_tri_3", 0 , 1)
    };
        WhiteLaserPos = white_laser.transform.Find("pos");
        BlackLaserPos = black_laser.transform.Find("pos");
        WhiteLaserNPos = white_laser.transform.Find("Npos");
        BlackLaserNPos = black_laser.transform.Find("Npos");
        // 위치 설정
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }


    public GameObject Create(string name, int x, int y)
    {
        // LaserMan 프리팹을 생성하고 초기화
        GameObject laserManObject = Instantiate(piece);

        if (laserManObject != null) // 추가된 부분: null 체크
        {
            CoLaserMan laserManScript = laserManObject.GetComponent<CoLaserMan>();
            // 수정된 부분: 레이저의 회전 각도 설정
            

            // 수정된 부분: 레이저 프리팹과 회전 각도를 LaserMan 스크립트에 전달
            
            if (laserManScript != null) // 추가된 부분: null 체크
            {
                laserManScript.name = name;

                // 프리팹의 LaserMan 스크립트의 좌표 설정
                laserManScript.SetXBoard(x);
                laserManScript.SetYBoard(y);
                laserManScript.Activate();

                // 게임 보드에 위치 설정
                SetPosition(laserManObject, x, y);
            }
            else
            {
                Debug.LogError("LaserMan script not found on instantiated object.");
            }
        }
        else
        {
            Debug.LogError("Failed to instantiate piecePrefab.");
        }

        return laserManObject;
    }
    public GameObject Create(GameObject prefab, string name, int x, int y)
    {
        // Instantiate the provided prefab and initialize
        GameObject laserManObject = Instantiate(prefab);

        if (laserManObject != null)
        {
            CoLaserMan laserManScript = laserManObject.GetComponent<CoLaserMan>();

            if (laserManScript != null)
            {
                laserManScript.name = name;
                laserManScript.SetXBoard(x);
                laserManScript.SetYBoard(y);
                laserManScript.Activate();

                // Set position on the game board
                SetPosition(laserManObject, x, y);
            }
            else
            {
                Debug.LogError("LaserMan script not found on instantiated object.");
            }
        }
        else
        {
            Debug.LogError("Failed to instantiate piecePrefab.");
        }

        return laserManObject;
    }

    private void SetPosition(GameObject obj, int x, int y)
    {
        // 좌표에 따라 오브젝트의 위치 설정
        float xPos = x * 0.66f - 2.3f;
        float yPos = y * 0.66f - 2.3f;
        obj.transform.position = new Vector3(xPos, yPos, -1.0f);
    }

    public void SetPosition(GameObject obj)
    {
        CoLaserMan CLm = obj.GetComponent<CoLaserMan>();

        position[CLm.GetXBoard(), CLm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        position[x, y] = null;

    }

    public GameObject GetPosition(int x, int y)
    {
        return position[x, y];
    }
    

    public bool PositoinOnBoard(int x,int y)
    {
        if (x < 0 || y < 0 || x >= position.GetLength(0) || y >= position.GetLength(1)) return false;
        return true;
    }
    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }
    public bool IsGameOver()
    {
        return gameOver;
    }
    public void Shot()
    {

        if (currentPlayer == "white")
        {
            AngleBasedShot(BlackLaserPos, BlackLaserNPos ,blacklaserAng, "White");
        }
        else if (currentPlayer == "black")
        {
            AngleBasedShot(WhiteLaserPos, WhiteLaserNPos, whitelaserAng, "Black");
        }
    }

    public void NextTurn()
    {
        // 현재 플레이어 교체
        currentPlayer = (currentPlayer == "white") ? "black" : "white";

        Shot();
    }
    public void AngleBasedShot(Transform laserPos, Transform laserNpos, float angle, string playerColor)
    {
        // 레이저를 발사할 위치 계산 (앞 부분)
        Vector3 laserStartPosition = (Mathf.Approximately(angle, 90f)) ? laserNpos.position : laserPos.position;

        // 레이저 프리팹을 생성하고 발사
        GameObject instantLaser = Instantiate(Raser, laserStartPosition, Quaternion.identity);  // 회전을 identity(기본값)로 설정
        Rigidbody2D laserRigidbody = instantLaser.GetComponent<Rigidbody2D>();

        // 각도를 이용하여 방향 벡터 생성
        Vector2 direction;

        if (Mathf.Approximately(angle, 90f))
        {
            // angle이 90일 때는 laserNpos에서 발사하므로 direction을 위쪽으로 설정
            direction = Vector2.up;
        }
        else
        {
            // 그 외의 경우 각도에 따라 방향 벡터 계산
            direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        // 플레이어 색상에 따라 다른 힘의 방향 설정
        float forceMultiplier = (playerColor == "White") ? -3.0f : 3.0f;
        laserRigidbody.AddForce(direction * forceMultiplier, ForceMode2D.Impulse);
    }






    public void SetWhiteLaserAng(float angle)
    {
        whitelaserAng = angle;
    }



    public void SetBlackLaserAng(float angle)
    {
        blacklaserAng = angle;
    }





    public void EndGame(string winnerMessage)
    {
        Debug.Log(winnerMessage);
        // 추가적인 종료 로직을 이곳에 추가할 수 있습니다.
        if (messageText != null)
        {
            messageText.text = winnerMessage;
            messageText.gameObject.SetActive(true);
            mainbutton.gameObject.SetActive(true);// Text UI를 활성화
        }
    }

    public void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }
    }

}

