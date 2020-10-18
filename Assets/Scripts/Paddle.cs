using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] private float screenWidthInUnits = 16f;
    [SerializeField] private float minX = 1f;
    [SerializeField] private float maxX = 15f;

    private GameSession theGameSession;
    private Ball theBall;
    
    private float xMin;
    private float xMax;

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
        SetUpMoveBoundaries();
    }
    
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 1f;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (theGameSession.Controls)
        {
            case 0:
                var paddlePos = new Vector2(transform.position.x, transform.position.y);
                
                paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);

                transform.position = paddlePos;
                break;
            case 1:
                Move();
                break;
        }
    }
    
    private void Move()
    {
        
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
