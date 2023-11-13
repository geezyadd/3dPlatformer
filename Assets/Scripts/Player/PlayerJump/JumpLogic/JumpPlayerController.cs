using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerController : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private AnimationCurve _forwardAnimation;

    //private float _expiredTime;
    //private float _duration;
    public void StopCoroutine()
    {
        StopAllCoroutines();
    }

    public void AnimationsPlaying(Transform jumper, float duration, bool isPlayerMove) 
    {
        StartCoroutine(AnimationByTime(jumper, duration, isPlayerMove));
    }
    private IEnumerator AnimationByTime(Transform jumper, float duration, bool isPlayerMove) 
    {
       
        //float expiredSeconds = 0;
        //float progress = 0;
        //
        //Vector3 startPosition = jumper.position;
        //while(progress < 1) 
        //{
        //    expiredSeconds += Time.deltaTime;
        //    progress = expiredSeconds / duration;
        //    jumper.position = startPosition + new Vector3(0, _yAnimation.Evaluate(progress), 0);
        //    
        //
        //    yield return null;
        //}
        float expiredSeconds = 0;
        float progress = 0;

        Vector3 startPosition = jumper.position;
        Vector3 jumperForward = jumper.forward;
        while (progress < 1)
        {
            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / duration;

            
            float yValue = _yAnimation.Evaluate(progress);
            float forwardValue = _forwardAnimation.Evaluate(progress);
            if (!isPlayerMove)
            {
                jumper.position = startPosition + new Vector3(0, _yAnimation.Evaluate(progress), 0);
            }
            else 
            {

                Vector3 newPosition = startPosition + jumperForward * forwardValue * 2 + new Vector3(0, yValue, 0);
                jumper.position = newPosition;
            }
            
            

            yield return null;
        }
    }
    
}
