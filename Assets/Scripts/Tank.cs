using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    GameObject body;

    [SerializeField]
    GameObject turret;

    public int health = 3;

    public int damage = 1;

    [SerializeField]
    GameObject arrow;
    bool arrowActive = false;

    [SerializeField]
    float timeToRotationTurret = 0.25f;

    [SerializeField]
    float timeToRotateBody = 0.25f;

    [SerializeField]
    float speed = 2.0f;

    [SerializeField]
    GameObject projectiles;

    [SerializeField]
    Projectile projectile;

    [SerializeField]
    GameManager gameManager;
    PathFindingGrid pathFindingGrid;
    Animator bodyAnimator;
    Animator turretAnimator;

    BoxCollider2D boxCollider;

    bool isMoving = false;
    bool isRotating = false;
    bool hasFired = false;
    bool isAlive = true;

    Vector2 DirectionBodyIsFacing = Vector2.up;
    Vector2 DirectionTurretIsFacing = Vector2.up;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        pathFindingGrid = FindObjectOfType<PathFindingGrid>();
        bodyAnimator = body.GetComponent<Animator>();
        turretAnimator = turret.GetComponent<Animator>();
        body.GetComponent<TankBody>().TankHasDied += KillTank;
        EventBroker.PlayerDamaged += DamagePlayer;
    }

    void Update()
    {
        if(isAlive)
        {
            if(!isMoving && !isRotating && !hasFired)
            {
                MoveBody();
                RotateBody();
                RotateTurret();
                Attack();
            }
            EnableDirectionArrow();
        }
    }

    private void EnableDirectionArrow()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            arrowActive = !arrowActive;
            arrow.SetActive(arrowActive);
        }
    }

    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space) && hasFired == false)
        {
            pathFindingGrid.UpdateAllPaths();
            gameManager.PlayerActionTaken();
            hasFired = true;
            StartCoroutine(TimeSinceLastShotCounter());
            projectile.enabled = true;
            projectile.GetComponent<Projectile>().Direction = DirectionTurretIsFacing;
            Instantiate(projectile, transform.position + (Vector3)(DirectionTurretIsFacing / 2), Quaternion.identity, projectiles.transform);
        }
    }

    /// <summary>
    /// Used to ensure player can't shoot more than once per turn
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeSinceLastShotCounter()
    {
        float timeSinceLastShot = 0f;

        while (timeSinceLastShot <= GameManager.timePerTurn)
        {
            timeSinceLastShot += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        hasFired = false;
    }

    private void RotateTurret()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(RotateObjectCoroutine(turret, Vector3.forward * -90, timeToRotationTurret, false));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(RotateObjectCoroutine(turret, Vector3.forward * 90, timeToRotationTurret, false));
        }
    }

    public IEnumerator RotateObjectCoroutine(GameObject objToRotate, Vector3 changeRotation, float duration,
        bool isBody)
    {
        isRotating = true;

        if(objToRotate.tag == "tank body")
        {
            bodyAnimator.SetBool("Driving", true);
        }

        Vector3 currentRoation = objToRotate.transform.rotation.eulerAngles;
        Vector3 newRotation = changeRotation + currentRoation;

        if(isBody)
        {
            DirectionBodyIsFacing = UpdateDirectionFacing(newRotation);
        }
        else
        {
            DirectionTurretIsFacing = UpdateDirectionFacing(newRotation);
        }

        float timeCount = 0f;
        while (timeCount < duration)
        {
            timeCount += Time.deltaTime;
            objToRotate.transform.eulerAngles = Vector3.Lerp(currentRoation, newRotation, timeCount / duration);
            yield return new WaitForEndOfFrame();
        }

        bodyAnimator.SetBool("Driving", false);

        isRotating = false;
    }

    public void TriggerDeathAnimation()
    {
        bodyAnimator.SetTrigger("Death");
        turretAnimator.SetTrigger("Death");
    }

    public void KillTank()
    {
        isAlive = false;
    }

    private Vector2 UpdateDirectionFacing(Vector3 newRotation)
    {
        switch (Mathf.Floor(newRotation.z))
        {
            case (0):
            case (360):
                return Vector2.up;
            case (90):
            case (-270):
                return Vector2.left;
            case (180):
            case (-180):
                return Vector2.down;
            case (270):
            case (-90):
                return Vector2.right;
            default:
                return Vector2.up;
        }
    }

    private void MoveBody()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(MoveBodyCoroutine(DirectionBodyIsFacing));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(MoveBodyCoroutine(-DirectionBodyIsFacing));
        }
    }

    private void RotateBody()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(RotateObjectCoroutine(body, Vector3.forward * -90, timeToRotateBody, true));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(RotateObjectCoroutine(body, Vector3.forward * 90, timeToRotateBody, true));
        }
    }

    public IEnumerator MoveBodyCoroutine(Vector2 direction)
    {
        isMoving = true;
        bodyAnimator.SetBool("Driving", true);
        gameManager.PlayerActionTaken();

        Vector2 startPosition = transform.position;
        Vector2 newPosition = startPosition + direction;
        bool wasCollision = false;
        float duration = GameManager.timePerTurn;

        float timeCount = 0f;
        while (timeCount < duration)
        {
            wasCollision = wasCollision == true ? true : boxCollider.IsTouchingLayers(LayerMask.GetMask("Obstacles", "Walls"));
            timeCount += Time.deltaTime;

            if (wasCollision == false)
            {
                transform.position = Vector3.Lerp(startPosition, newPosition, timeCount / duration);
            }
            else
            {
                transform.position = startPosition;
            }
            yield return new WaitForEndOfFrame();
        }

        if (wasCollision == false)
        {
            transform.position = newPosition;
        }

        isMoving = false;
        bodyAnimator.SetBool("Driving", false);
        pathFindingGrid.UpdateAllPaths();
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;

        if (health <= 0 && isAlive)
        {
            StopAllCoroutines();
            TriggerDeathAnimation();
            KillTank();
        }
    }
}
