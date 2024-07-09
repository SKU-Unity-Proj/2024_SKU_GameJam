using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVoicePlayer : MonoBehaviour
{
    public SoundList[] sounds;  // 재생할 소리 목록
    private int index;  // 현재 재생할 소리의 인덱스

    private void Start()
    {
        StartCoroutine(PlayRandomSound());
    }

    private IEnumerator PlayRandomSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);  // 10초 대기

            int oldIndex = index;
            while (oldIndex == index)
            {
                index = Random.Range(0, sounds.Length);
            }

            SoundManager.Instance.PlayOneShotEffect((int)sounds[index], transform.position, 0.2f);
        }
    }
}
