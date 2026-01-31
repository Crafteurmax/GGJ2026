using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : MonoBehaviour
{
    public static Simon instance;
    public bool isReseting;
    
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeBetweenSounds = 0.2f;
    
    public int simonSize;
    
    [SerializeField] List<Movements> WantedMovements = new List<Movements>();
    [SerializeField] List<Movements> PerformedMovements = new List<Movements>();

    public void Awake()
    {
        if (instance == null) instance = this;
        StartCoroutine(ResetSimon());
    }
    
    public IEnumerator ResetSimon()
    {
        isReseting = true;
        WantedMovements.Clear();

        for (int i = 0; i < simonSize; i++)
        {
            Movements nextMove = (Movements)Random.Range(0, 4);
            WantedMovements.Add(nextMove);
            audioSource.PlayOneShot(audioClips[(int)nextMove]);
            yield return new WaitUntil(() => !audioSource.isPlaying);
            yield return new WaitForSeconds(timeBetweenSounds);
        }
        isReseting = false;
        PerformedMovements.Clear();
        yield return null;
    }
    
    public void AddMovement(Movements movement)
    {
        PerformedMovements.Add(movement);
    }
    
    public SimonStatus CompareMovements()
    {
        if (isReseting) return SimonStatus.Pending;
        if (PerformedMovements.Count > WantedMovements.Count) return SimonStatus.Wrong; // shouldn't be possible
        if (PerformedMovements[^1] != WantedMovements[PerformedMovements.Count - 1]) return SimonStatus.Wrong; // see if the last action is correct
        if (PerformedMovements.Count < WantedMovements.Count) return SimonStatus.Pending; // the last action is correct, but we are still waiting for all the inputs
        
        // if we are here it means that both lists are the same size and have the same inputs, but we do a last check just to be sure
        for(int i = 0; i < PerformedMovements.Count;i++) if(PerformedMovements[i] != WantedMovements[i]) return SimonStatus.Wrong;
        return SimonStatus.Correct;
    }
    
    public enum Movements
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum SimonStatus
    {
        Wrong,
        Correct,
        Pending
    }
}
