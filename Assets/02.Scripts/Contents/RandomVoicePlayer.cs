using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVoicePlayer : MonoBehaviour
{
    public SoundList[] sounds;  // ����� �Ҹ� ���
    private int index;  // ���� ����� �Ҹ��� �ε���

    private void Start()
    {
        StartCoroutine(PlayRandomSound());
    }

    private IEnumerator PlayRandomSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);  // 10�� ���

            int oldIndex = index;
            while (oldIndex == index)
            {
                index = Random.Range(0, sounds.Length);
            }

            SoundManager.Instance.PlayOneShotEffect((int)sounds[index], transform.position, 0.2f);
        }
    }
}
