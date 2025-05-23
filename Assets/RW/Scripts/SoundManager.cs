using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip shootClip;
    public AudioClip sheepHitClip;
    public AudioClip sheepDroppedClip;

    private Vector3 cameraPosition;

    void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, cameraPosition);
    }
    public void PlayShootClip()
    {
        PlaySound(shootClip);
    }

    public void PlaySheepHitClip()
    {
        PlaySound(sheepHitClip);
    }

    public void PlaySheepDroppedClip()
    {
        PlaySound(sheepDroppedClip);
    }
}
