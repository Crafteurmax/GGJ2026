using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    [SerializeField] private float walkingDistance;
    [SerializeField] private float walkingSpeed;
    
    [SerializeField] private float shakingForce;
    [SerializeField] private float shakingSpeed;
    
    private float baseYPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        baseYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("CallMoveOtherSide")]
    public void CallMoveOtherSide()
    {
        StartCoroutine(MoveToTheOtherSide());
    }
    
    
    private IEnumerator MoveToTheOtherSide()
    {
        if(LeanTween.isTweening(gameObject)) yield break; // if already moving escape
        float destination = transform.position.x + (isLeft ? -walkingDistance : walkingDistance); // move to the other side of the screen
        
        int tweenXId = LeanTween.moveX(gameObject, destination, walkingDistance / walkingSpeed).id;
        int tweenYId = LeanTween.moveY(gameObject, transform.position.y + shakingForce, shakingForce / shakingSpeed).setEaseInBounce().setLoopPingPong().id;
        
        yield return new WaitUntil(() => !LeanTween.isTweening(tweenXId)); //wait until the npc is off screen
        LeanTween.cancel(tweenYId);
        Vector3 resetPosition = transform.position;
        resetPosition.y = baseYPosition;
        transform.position = resetPosition;
        isLeft = !isLeft;
    }
}
