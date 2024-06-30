using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShurikanSkill : Skill
{
    [Header("Shurikan info")]
    [SerializeField] private GameObject shurikanPrefab;
    [SerializeField] private Vector2 launchDir;
    [SerializeField] private float shurikanGravity;

    private Vector2 finalDir;

    [Header("Aim info")] 
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotParent;
    [SerializeField] private float minAimDir;

    private GameObject[] dots;


    protected override void Start()
    {
        base.Start();
        
        GenerateDots();
    }
    
    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            finalDir = new Vector2(AimDir().normalized.x * launchDir.x, AimDir().normalized.y * launchDir.y);
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }

    public void CreateShurikan()
    {
        GameObject newShurikan = Instantiate(shurikanPrefab, player.transform.position, player.transform.rotation);
        ShurikanSkillController newShurikanSkillController = newShurikan.GetComponent<ShurikanSkillController>();
        
        newShurikanSkillController.SetUpShurikan(finalDir,shurikanGravity,player);
        
        player.ThrowShurikan(newShurikan);
        
        DotsActive(false);
    }

    public Vector2 AimDir()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 aimDir = mousePosition - playerPosition;

        if (aimDir.y < minAimDir)
        {
            aimDir.y = minAimDir;
        }

        return aimDir;
    }

    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);
        }
        
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
                AimDir().normalized.x * launchDir.x,
                AimDir().normalized.y * launchDir.y)
            * t + 0.5f * (Physics2D.gravity * shurikanGravity) * t * t;

        return position;
    }
    
}
