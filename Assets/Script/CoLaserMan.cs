using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class CoLaserMan : MonoBehaviour
{
    public GameObject controllrer;
    public GameObject movePlate;
    

    private int xBoard = -1;
    private int yBoard = -1;

    private string player;
    

    public Sprite black_king, black_tri_1, black_tri_2, black_tri_3, black_squre, black_splate;
    public Sprite white_king, white_tri_1, white_tri_2, white_tri_3, white_squre, white_splate;


    public void Activate()
    {
        controllrer = GameObject.FindGameObjectWithTag("GameController");

        //체스 판을 좌표로 만들기 
        SetCoords();

        //각 말의 콜라이더 생성
        AddColliderBasedOnPiece();
        
        gameObject.tag = this.name;
        switch (this.name)
        {
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_tri_1": this.GetComponent<SpriteRenderer>().sprite = black_tri_1; player = "black"; break;
            case "black_tri_2": this.GetComponent<SpriteRenderer>().sprite = black_tri_2; player = "black"; break;
            case "black_tri_3": this.GetComponent<SpriteRenderer>().sprite = black_tri_3; player = "black"; break;
            case "black_squre": this.GetComponent<SpriteRenderer>().sprite = black_squre; player = "black"; break;
            case "black_splate":
                this.GetComponent<SpriteRenderer>().sprite = black_splate;
                player = "black";
                RotatePiece(180f); // black_splate는 180도 회전하여 생성
                break;
            case "black_laser":
                player = "black";
                break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_tri_1": this.GetComponent<SpriteRenderer>().sprite = white_tri_1; player = "white"; break;
            case "white_tri_2": this.GetComponent<SpriteRenderer>().sprite = white_tri_2; player = "white"; break;
            case "white_tri_3": this.GetComponent<SpriteRenderer>().sprite = white_tri_3; player = "white"; break;
            case "white_squre": this.GetComponent<SpriteRenderer>().sprite = white_squre; player = "white"; break;
            case "white_splate": this.GetComponent<SpriteRenderer>().sprite = white_splate; player = "white"; break;
            case "white_laser":
                player = "white";
                break;
            
        }
    }
    private void RotatePiece(float rotationAngle)
    {
        // 현재 객체의 SpriteRenderer 컴포넌트를 가져옴
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // 회전값을 Quaternion.Euler를 사용하여 설정
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        // 회전값을 적용하여 객체를 회전
        transform.rotation = rotation;
    }
    private void AddColliderBasedOnPiece()
    {
        // 이름에 따라 각 말에 대한 Collider 추가
        switch (this.name)
        {
            //콜라이더 생성
            case "black_king":
            case "white_king":
                killCollider();
                break;
            case "black_squre":
                killCollider();
                Bsqure();
                break;
            case "white_squre":
                killCollider();
                Wsqure();
                break;
            case "white_tri_2":                
                triKill4();
                Wtri2();
                break;
            case "black_tri_2":               
                triKill2();
                Btri2();
                break;
            case "black_tri_1":
            case "white_tri_1":
                triKill1();
                tri1();
                break;
            case "black_tri_3": 
            case "white_tri_3":
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
        // 원하는 크기 및 위치로 조절
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(1.0f, 1.0f);
        boxCollider.offset = new Vector2(0.0f, 0.0f);
        
    }
    private void triKill1()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;
        // 원하는 위치에 따라 정점을 설정
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
        // 원하는 위치에 따라 정점을 설정
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
        // 원하는 위치에 따라 정점을 설정
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
        // 원하는 위치에 따라 정점을 설정
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
        // 원하는 크기 및 위치로 조절
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(0.8438679f, 0.8528936f);
        boxCollider.offset = new Vector2(0.002113894f, 0.003028989f);
    }
    private void splate()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        
        // 원하는 위치에 따라 정점을 설정
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
        // 원하는 크기 및 위치로 조절
        boxCollider.size = new Vector2(0.2588511f, 0.8703787f);
        boxCollider.offset = new Vector2(0.4288336f, -0.0006781816f);
    }
    private void Bsqure()
    {
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        // 원하는 크기 및 위치로 조절
        boxCollider.size = new Vector2(0.2511032f, 0.8472852f);
        boxCollider.offset = new Vector2(-0.4218045f, 0.01086859f);
    }

    private void Wtri2()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        // 원하는 위치에 따라 정점을 설정
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
        // 원하는 위치에 따라 정점을 설정
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
        // 원하는 위치에 따라 정점을 설정
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
        // 원하는 위치에 따라 정점을 설정
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


    private void OnMouseUp()
    {
        if (!controllrer.GetComponent<CoGame>().IsGameOver() && controllrer.GetComponent<CoGame>().GetCurrentPlayer() == player)
        {
            if (isFirstClick)
            {
                DestoryMove();
                
                // 첫 번째 클릭: 말을 선택
                InitiateMove();
                
                isFirstClick = false;
            }
            else
            {
                DestoryMove();
                if (gameObject.CompareTag("white_laser") || gameObject.CompareTag("black_laser"))
                {
                    float currentAngle = transform.rotation.eulerAngles.z;
                    float newAngle = (currentAngle == 0f) ? 90f : 0f;
                    
                    // 함수 호출로 회전
                    RotateObject(newAngle);
                    CoGame gameScript = FindObjectOfType<CoGame>();
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
                    
                    // 두 번째 클릭: 말을 회전
                    Rotate();
                    isFirstClick = true;
                    
                }
            }
        }
    }



    void RotateObject(float targetAngle)
    {
        // 현재 각도
        float currentAngle = transform.rotation.eulerAngles.z;

        // 회전할 각도 계산
        float rotationAmount = targetAngle - currentAngle;

        // 회전 실행
        transform.Rotate(Vector3.forward, rotationAmount);
    }
    void Rotate()
    {
        // 현재 오브젝트의 회전 값을 가져옴
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // 새로운 회전 값을 계산
        float newRotation = currentRotation.z + 90f; // 90도씩 회전

        // 새로운 회전 값을 적용
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
            case "black_tri_1":
            case "black_tri_2":
            case "black_tri_3":
            case "black_squre":
            case "black_splate":
            case "white_tri_1":
            case "white_tri_2":
            case "white_tri_3":
            case "white_squre":
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
        PointMove(xBoard, yBoard + 1);
        PointMove(xBoard, yBoard - 1);
        PointMove(xBoard - 1, yBoard + 1);
        PointMove(xBoard - 1, yBoard - 1);
        PointMove(xBoard - 1, yBoard);
        PointMove(xBoard + 1, yBoard + 1);
        PointMove(xBoard + 1, yBoard - 1);
        PointMove(xBoard + 1, yBoard);
    }

    public void PointMove(int x, int y)
    {
        CoGame sc = controllrer.GetComponent<CoGame>();

        if (sc.PositoinOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MoveSpawn(x, y);
            }
        }
    }
    
    

    
     // 반사 여부를 나타내는 플래그
     public bool hasReflected = false;
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
                // 충돌한 오브젝트가 white_splate인 경우
                ReflectLaser(collision.gameObject);
            }
            else if (gameObject.CompareTag("white_king") || gameObject.CompareTag("black_king"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);

                CoGame gameController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<CoGame>();
                if (gameController != null)
                {
                    if (gameObject.CompareTag("white_king"))
                        gameController.EndGame("Black Wins!"); // 흰색 킹이 사라짐 -> 검은색 승리
                    else if (gameObject.CompareTag("black_king"))
                        gameController.EndGame("White Wins!"); // 검은색 킹이 사라짐 -> 흰색 승리
                }
            }
            else if(gameObject.CompareTag("white_king") || gameObject.CompareTag("black_king"))
            {
                Destroy(collision.gameObject);
            }
            else
            {
                // 그 외의 경우에는 레이저와 현재 오브젝트를 파괴
                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
            
            StartCoroutine(ResetReflectedFlag());
        }
    }
    
    private IEnumerator ResetReflectedFlag()
    {
        // 1초 딜레이
        yield return new WaitForSeconds(1.0f);

        // hasReflected를 false로 설정
        hasReflected = false;
    }

    private void ReflectLaser(GameObject originalLaser)
    {

        // 반사 로직을 구현합니다.
        // 예시로 현재 레이저의 방향을 반대로 설정했습니다.
        Vector3 currentDirection = originalLaser.transform.up;
        Vector3 reflectedDirection = -currentDirection;

        // 새로운 레이저 인스턴스 생성
        GameObject newLaser = Instantiate(originalLaser, transform.position, Quaternion.identity);

        // 새로운 레이저에 이전 레이저의 방향과 속력 등을 복사
        Laser newLaserScript = newLaser.GetComponent<Laser>();
        newLaserScript.SetDirection(reflectedDirection);
        newLaserScript.hitObject = originalLaser.GetComponent<Laser>().GetHitObject(); // Getter를 통해 접근

        // 여기에 추가적인 반사 로직을 구현할 수 있습니다.

        // 기존 레이저의 Rigidbody2D 컴포넌트가 있다면,
        Rigidbody2D newLaserRigidbody = newLaser.GetComponent<Rigidbody2D>();
        if (newLaserRigidbody != null)
        {
            // 기존 레이저의 속도를 복사하여 새로운 레이저에 적용
            newLaserRigidbody.velocity = originalLaser.GetComponent<Rigidbody2D>().velocity;
        }

        hasReflected = true; // 반사가 이루어졌음을 표시
    }


   
    public void MoveSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;


        x += -2.3f;
        y += -2.3f;


        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        Comove mpScript = mp.GetComponent<Comove>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

}