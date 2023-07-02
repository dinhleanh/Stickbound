using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";


    string FacingDirection;

    Vector3 baseScale;
    Vector3 baseRotation;

    Rigidbody2D rb;
    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDist = 0.2f;
    private float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        FacingDirection = LEFT;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float x = Random.Range(-2, 2);
        //rb.velocity = new Vector2(x, rb.velocity.y);

        if (IsHittingWall())
        {
            if(FacingDirection == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else if(FacingDirection == RIGHT)
            {
                ChangeFacingDirection(LEFT);
            }
        }
    }

    bool IsHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;

        if (FacingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.green);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    void ChangeFacingDirection(string newDirection)
    {
        

        FacingDirection = newDirection;
    }
}
