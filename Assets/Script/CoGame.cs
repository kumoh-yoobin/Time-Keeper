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
    // ü������ ��ǥ�� ��ȯ �� ���� ����
    private GameObject[,] position = new GameObject[8, 8]; 
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private float whitelaserAng, blacklaserAng;


    //�ϴ� �÷��̾� ������� ����
    private string currentPlayer = "white";
    
    
    //���ӿ��� ������ �Ǹ� true�� ��ȯ
    private bool gameOver = false;
    
    public float m_DoubleClickSecond = 0.25f;
    

    // Start is called before the first frame update
    void Start()
    {
       
        //������ ��ȯ -> 2d���������� Vector3 ���
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
        // ��ġ ����
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }


    public GameObject Create(string name, int x, int y)
    {
        // LaserMan �������� �����ϰ� �ʱ�ȭ
        GameObject laserManObject = Instantiate(piece);

        if (laserManObject != null) // �߰��� �κ�: null üũ
        {
            CoLaserMan laserManScript = laserManObject.GetComponent<CoLaserMan>();
            // ������ �κ�: �������� ȸ�� ���� ����
            

            // ������ �κ�: ������ �����հ� ȸ�� ������ LaserMan ��ũ��Ʈ�� ����
            
            if (laserManScript != null) // �߰��� �κ�: null üũ
            {
                laserManScript.name = name;

                // �������� LaserMan ��ũ��Ʈ�� ��ǥ ����
                laserManScript.SetXBoard(x);
                laserManScript.SetYBoard(y);
                laserManScript.Activate();

                // ���� ���忡 ��ġ ����
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
        // ��ǥ�� ���� ������Ʈ�� ��ġ ����
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
        // ���� �÷��̾� ��ü
        currentPlayer = (currentPlayer == "white") ? "black" : "white";

        Shot();
    }
    public void AngleBasedShot(Transform laserPos, Transform laserNpos, float angle, string playerColor)
    {
        // �������� �߻��� ��ġ ��� (�� �κ�)
        Vector3 laserStartPosition = (Mathf.Approximately(angle, 90f)) ? laserNpos.position : laserPos.position;

        // ������ �������� �����ϰ� �߻�
        GameObject instantLaser = Instantiate(Raser, laserStartPosition, Quaternion.identity);  // ȸ���� identity(�⺻��)�� ����
        Rigidbody2D laserRigidbody = instantLaser.GetComponent<Rigidbody2D>();

        // ������ �̿��Ͽ� ���� ���� ����
        Vector2 direction;

        if (Mathf.Approximately(angle, 90f))
        {
            // angle�� 90�� ���� laserNpos���� �߻��ϹǷ� direction�� �������� ����
            direction = Vector2.up;
        }
        else
        {
            // �� ���� ��� ������ ���� ���� ���� ���
            direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        // �÷��̾� ���� ���� �ٸ� ���� ���� ����
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
        // �߰����� ���� ������ �̰��� �߰��� �� �ֽ��ϴ�.
        if (messageText != null)
        {
            messageText.text = winnerMessage;
            messageText.gameObject.SetActive(true);
            mainbutton.gameObject.SetActive(true);// Text UI�� Ȱ��ȭ
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

