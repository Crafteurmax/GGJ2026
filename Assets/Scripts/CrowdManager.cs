using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    [SerializeField] List<NpcMovement> npcs = new List<NpcMovement>();

    private float timer;
    [SerializeField] private float movingCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= movingCooldown)
        {
            timer = 0;
            npcs[Random.Range(0,npcs.Count)].CallMoveOtherSide();
        }
    }
}
