using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatJumpCollider : MonoBehaviour
{
    // Start is called before the first frame update

    Enemies enemy;
    public List<GameObject> jumpPoints = new List<GameObject>();
    int idx = 0;
    GameObject currentTarget;

    bool activateJump = false;

    private void Awake()
    {
        enemy = FindObjectOfType<Enemies>();    
    }

    void Start()
    {
        currentTarget = jumpPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(activateJump)
        {
            MakeCatJump();
        }

        if(idx >= jumpPoints.Capacity)
        {
            enemy.isMoving = true;
            activateJump = false;
            idx = 0;
            currentTarget = jumpPoints[0];
            enemy.isJumpingIsland = false;
        }
    }

    public void MakeCatJump()
    {
        enemy.isMoving = false;
        if(Mathf.Abs(enemy.transform.position.y - currentTarget.transform.position.y) <= 0.01f)
        {
            idx++;
            if (idx >= jumpPoints.Count) return;
            currentTarget = jumpPoints[idx];

        }
        enemy.ChangeToJump(true, currentTarget.transform.position);
    }

    public void ActivateJump()
    {
        activateJump = true;
        enemy.SetToJumpAnimation();
    }
}
