using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

[RequireComponent(typeof(Rigidbody2D))]
public class LaserMan : MonoBehaviour
{
    public GameObject controllrer;
    public GameObject movePlate;
    private LaserWebSocketClient client;
    

    private int xBoard = -1;
    private int yBoard = -1;

    private string player;

    private string pieceId;
    

    public Sprite black_king, black_tri_1, black_tri_2, black_tri_3, black_squre, black_splate;
    public Sprite white_king, white_tri_1, white_tri_2, white_tri_3, white_squre, white_splate;

    private void Start()
    {
        client = FindObjectOfType<LaserWebSocketClient>();
    }

    public void Activate()
    {
        controllrer = GameObject.FindGameObjectWithTag("GameController");

        //ü�� ���� ��ǥ�� ����� 
        SetCoords();

        //�� ���� �ݶ��̴� ����
        AddColliderBasedOnPiece();
        
        gameObject.tag = this.name;
        switch (this.name)
        {
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_tri_0": this.GetComponent<SpriteRenderer>().sprite = black_tri_3; player = "black"; break;
            case "black_tri_1": this.GetComponent<SpriteRenderer>().sprite = black_tri_1; player = "black"; break;
            case "black_tri_2": this.GetComponent<SpriteRenderer>().sprite = black_tri_2; player = "black"; break;
            case "black_tri_3": this.GetComponent<SpriteRenderer>().sprite = black_tri_3; player = "black"; break;
            case "black_tri_4": this.GetComponent<SpriteRenderer>().sprite = black_tri_2; player = "black"; break;
            case "black_squre": this.GetComponent<SpriteRenderer>().sprite = black_squre; player = "black"; break;
            case "black_squre_1": this.GetComponent<SpriteRenderer>().sprite = black_squre; player = "black"; break;
            case "black_splate":
                this.GetComponent<SpriteRenderer>().sprite = black_splate;
                player = "black";
                RotatePiece(180f); // black_splate�� 180�� ȸ���Ͽ� ����
                break;
            case "black_laser":
                player = "black";
                break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_tri_0": this.GetComponent<SpriteRenderer>().sprite = white_tri_1; player = "white"; break;
            case "white_tri_1": this.GetComponent<SpriteRenderer>().sprite = white_tri_1; player = "white"; break;
            case "white_tri_2": this.GetComponent<SpriteRenderer>().sprite = white_tri_2; player = "white"; break;
            case "white_tri_3": this.GetComponent<SpriteRenderer>().sprite = white_tri_3; player = "white"; break;
            case "white_tri_4": this.GetComponent<SpriteRenderer>().sprite = white_tri_2; player = "white"; break;
            case "white_squre": this.GetComponent<SpriteRenderer>().sprite = white_squre; player = "white"; break;
            case "white_squre_1": this.GetComponent<SpriteRenderer>().sprite = white_squre; player = "white"; break;
            case "white_splate": this.GetComponent<SpriteRenderer>().sprite = white_splate; player = "white"; break;
            case "white_laser":
                player = "white";
                break;
            
        }
    }
    private void RotatePiece(float rotationAngle)
    {
        // ���� ��ü�� SpriteRenderer ������Ʈ�� ������
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // ȸ������ Quaternion.Euler�� ����Ͽ� ����
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        // ȸ������ �����Ͽ� ��ü�� ȸ��
        transform.rotation = rotation;
    }
    private void AddColliderBasedOnPiece()
    {
        // �̸��� ���� �� ���� ���� Collider �߰�
        switch (this.name)
        {
            //�ݶ��̴� ����
            case "black_king":
            case "white_king":
                killCollider();
                break;
            case "black_squre":
            case "black_squre_1":
                killCollider();
                Bsqure();
                break;
            case "white_squre":
            case "white_squre_1":
                killCollider();
                Wsqure();
                break;
            case "white_tri_2":
            case "white_tri_4":
                triKill4();
                Wtri2();
                break;
            case "black_tri_2":
            case "black_tri_4":
                triKill2();
                Btri2();
                break;
            case "black_tri_1":
            case "white_tri_1":
            case "white_tri_0":
                triKill1();
                tri1();
                break;
            case "black_tri_3": 
            case "white_tri_3":
            case "black_tri_0":
                triKill3();
                tri3();
                break;
            case "black_splate":
            case "white_splate":
                SenserCollider();
                splate();
                break;
                // Add other cases based on your piece types
        }
    }

    
    private void killCollider()
    {
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        // ���ϴ� ũ�� �� ��ġ�� ����
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(1.0f, 1.0f);
        boxCollider.offset = new Vector2(0.0f, 0.0f);
        
    }
    private void triKill1()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(0.4234532f, 0.4234442f),
            new Vector2(0.2583792f, -0.2512011f),
            new Vector2(-0.423439f, -0.4279732f),
            new Vector2(0.4157887f, -0.4310998f)
            
        };
    }
    private void triKill2()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(0.4010151f, 0.4258659f),
            new Vector2(-0.4114053f, 0.4254578f),
            new Vector2(0.2372711f, 0.2500994f),
            new Vector2(0.4315685f, -0.4030554f)
            
        };
    }
    private void triKill3()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(0.4280567f, 0.4321354f),
            new Vector2(-0.3113918f, 0.2986816f),
            new Vector2(-0.4194252f, -0.4257804f),
            new Vector2(-0.4130705f, 0.4257804f)
            
        };
    }
    private void triKill4()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(-0.2612004f, -0.2319725f),
            new Vector2(-0.4155735f, 0.4245908f),
            new Vector2(-0.4371636f, -0.4259564f),
            new Vector2(0.3961739f, -0.4273219f)
            
        };
    }
    private void SenserCollider()
    {
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        // ���ϴ� ũ�� �� ��ġ�� ����
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(0.8438679f, 0.8528936f);
        boxCollider.offset = new Vector2(0.002113894f, 0.003028989f);
    }
    private void splate()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(-0.5f, 0.5f),
            new Vector2(-0.4f, 0.3f),
            new Vector2(0.3f, -0.4f),
            new Vector2(0.5f, -0.5f),
            new Vector2(0.4f, -0.3f),
            new Vector2(-0.3f, 0.4f)
        };
        
    }
    private void Wsqure()
    {
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        // ���ϴ� ũ�� �� ��ġ�� ����
        boxCollider.size = new Vector2(0.2588511f, 0.8703787f);
        boxCollider.offset = new Vector2(0.4288336f, -0.0006781816f);
    }
    private void Bsqure()
    {
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        // ���ϴ� ũ�� �� ��ġ�� ����
        boxCollider.size = new Vector2(0.2511032f, 0.8472852f);
        boxCollider.offset = new Vector2(-0.4218045f, 0.01086859f);
    }

    private void Wtri2()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(-0.2315775f, -0.2162662f),
            new Vector2(0.4f, -0.4f),
            new Vector2(-0.4f, 0.4f)
        };
    }
    private void Btri2()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(-0.4f, 0.4f),
            new Vector2(0.2009552f, 0.2009552f),
            new Vector2(0.4f, -0.4f)
        };
    }
    private void tri1()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(0.4f, 0.4f),
            new Vector2(-0.4f, -0.4f),
            new Vector2(0.2190786f, -0.2148711f)
        };
    }
    private void tri3()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        // ���ϴ� ��ġ�� ���� ������ ����
        polygonCollider.points = new Vector2[]
        {
            new Vector2(-0.2817873f, 0.2817873f),
            new Vector2(-0.4f, -0.4f),
            new Vector2(0.4f, 0.4f)
        };
    }


    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard()
    {
        return yBoard;
    }
    public void SetXBoard(int x)
    {
        xBoard = x;

    }
    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private bool isFirstClick = true;

    public void RotatePiece() {
        this.DestoryMove();
        if (gameObject.CompareTag("white_laser") || gameObject.CompareTag("black_laser"))
        {
            float currentAngle = transform.rotation.eulerAngles.z;
            float newAngle = (currentAngle == 0f) ? 90f : 0f;
            
            // �Լ� ȣ��� ȸ��
            RotateObject(newAngle);
            Game gameScript = FindObjectOfType<Game>();
            if (gameObject.CompareTag("white_laser"))
            {
                gameScript.SetWhiteLaserAng(newAngle);
            }
            else if(gameObject.CompareTag("black_laser"))
            {
                gameScript.SetBlackLaserAng(newAngle);
            }
        }
        else
        {
            // �� ��° Ŭ��: ���� ȸ��
            Rotate();
            isFirstClick = true;
        }
    }

    private void sendRotateData() {
        var vector = new {
            type = "rotate-piece",
            name = this.name
        };
        string vectorJson = JsonConvert.SerializeObject(vector);

        var data = new {
            type = "roomMsg",
            roomType = "laser",
            roomCode = client.getRoomCode(),
            msg = vectorJson
        };

        client.sendMessage(data);
    }


    private void OnMouseUp()
    {
        if (!controllrer.GetComponent<Game>().getEnablePlayer()) return;
        if (!controllrer.GetComponent<Game>().IsGameOver() && controllrer.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            if (isFirstClick)
            {
                DestoryMove();
                
                // ù ��° Ŭ��: ���� ����
                InitiateMove();
                
                isFirstClick = false;
            }
            else
            {
                this.sendRotateData();
            }
        }
    }



    void RotateObject(float targetAngle)
    {
        // ���� ����
        float currentAngle = transform.rotation.eulerAngles.z;

        // ȸ���� ���� ���
        float rotationAmount = targetAngle - currentAngle;

        // ȸ�� ����
        transform.Rotate(Vector3.forward, rotationAmount);
    }
    void Rotate()
    {
        // ���� ������Ʈ�� ȸ�� ���� ������
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // ���ο� ȸ�� ���� ���
        float newRotation = currentRotation.z + 90f; // 90���� ȸ��

        // ���ο� ȸ�� ���� ����
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newRotation);
    }
    
    public void DestoryMove()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }

    }
    public void InitiateMove()
    {
        switch (this.name)
        {
            case "black_king":
            case "white_king":
            case "black_tri_0":
            case "black_tri_1":
            case "black_tri_2":
            case "black_tri_3":
            case "black_tri_4":
            case "black_squre":
            case "black_squre_1":
            case "black_splate":
            case "white_tri_0":
            case "white_tri_1":
            case "white_tri_2":
            case "white_tri_3":
            case "white_tri_4":
            case "white_squre":
            case "white_squre_1":
            case "white_splate":
                SurroundMovePlate();
                break;
            case "black_laser":
            case "white_laser":
                break;

        }
    }
    

    public void SurroundMovePlate()
    {
        int[] xAxix = { 0, 0, -1, -1, -1, 1, 1, 1 };
        int[] yAxix = { 1, -1, 0, 1, -1, 0, 1, -1 };

        for (int i = 0; i < 8; i++)
        {
            PointMove(xBoard + xAxix[i], yBoard + yAxix[i]);
        }
    }

    public void PointMove(int x, int y)
    {
        Game sc = controllrer.GetComponent<Game>();

        if (sc.PositoinOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MoveSpawn(x, y);
            }
        }
    }
    
    
    public bool hasReflected = false; // �ݻ� ���θ� ��Ÿ���� �÷���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Laser") && !hasReflected)
        {
            if (gameObject.CompareTag("wall"))
            {
                Destroy(collision.gameObject);
            }
            else if (gameObject.CompareTag("white_splate") || gameObject.CompareTag("black_splate"))
            {
                // �浹�� ������Ʈ�� white_splate�� ���
                ReflectLaser(collision.gameObject);
            }
            else if (gameObject.CompareTag("white_king") || gameObject.CompareTag("black_king"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);

                Game gameController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<Game>();
                if (gameController != null)
                {
                    if (gameObject.CompareTag("white_king"))
                        gameController.EndGame("Black Wins!"); // ��� ŷ�� ����� -> ������ �¸�
                    else if (gameObject.CompareTag("black_king"))
                        gameController.EndGame("White Wins!"); // ������ ŷ�� ����� -> ��� �¸�
                }
            }
            else
            {
                // �� ���� ��쿡�� �������� ���� ������Ʈ�� �ı�
                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
            
            StartCoroutine(ResetReflectedFlag());
        }
    }
    
    private IEnumerator ResetReflectedFlag()
    {
        // 1�� ������
        yield return new WaitForSeconds(1.0f);

        // hasReflected�� false�� ����
        hasReflected = false;
    }

    private void ReflectLaser(GameObject originalLaser)
    {

        // �ݻ� ������ �����մϴ�.
        // ���÷� ���� �������� ������ �ݴ�� �����߽��ϴ�.
        Vector3 currentDirection = originalLaser.transform.up;
        Vector3 reflectedDirection = -currentDirection;

        // ���ο� ������ �ν��Ͻ� ����
        GameObject newLaser = Instantiate(originalLaser, transform.position, Quaternion.identity);

        // ���ο� �������� ���� �������� ����� �ӷ� ���� ����
        Laser newLaserScript = newLaser.GetComponent<Laser>();
        newLaserScript.SetDirection(reflectedDirection);
        newLaserScript.hitObject = originalLaser.GetComponent<Laser>().GetHitObject(); // Getter�� ���� ����

        // ���⿡ �߰����� �ݻ� ������ ������ �� �ֽ��ϴ�.

        // ���� �������� Rigidbody2D ������Ʈ�� �ִٸ�,
        Rigidbody2D newLaserRigidbody = newLaser.GetComponent<Rigidbody2D>();
        if (newLaserRigidbody != null)
        {
            // ���� �������� �ӵ��� �����Ͽ� ���ο� �������� ����
            newLaserRigidbody.velocity = originalLaser.GetComponent<Rigidbody2D>().velocity;
        }

        hasReflected = true; // �ݻ簡 �̷�������� ǥ��
    }


   
    private move MoveSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;


        x += -2.3f;
        y += -2.3f;


        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        move mpScript = mp.GetComponent<move>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);

        return mpScript;
    }

}