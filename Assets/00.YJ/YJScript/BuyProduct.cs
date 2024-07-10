using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Playables;

public class BuyProduct : MonoBehaviour
{
    public int buyId;

    public GameObject carpet;
    public GameObject corner;
    public GameObject jokjoke;

    public TextMeshProUGUI numberText; // UI 텍스트 컴포넌트

    public PlayableDirector playableDirector;

    private void Awake()
    {
        if(carpet != null)
            carpet.SetActive(false); corner.SetActive(false);
    }

    public void BuyProductBtn()
    {
        if(buyId != 0)
        {
            int price = 0;
            if(buyId == 1)
            {
                playableDirector.Play();

                price = 70000;
                SubtractFromNumber(price);

                Invoke("BuyId1", 1f);
                return;
            }

            if (buyId == 2)
            {
                playableDirector.Play();

                price = 20000;
                SubtractFromNumber(price);

                Invoke("BuyId2", 1f);
                return;
            }

            if (buyId == 3)
            {
                price = 8000;
                jokjoke.SetActive(true);
                SubtractFromNumber(price);
                return;
            }
        }
    }

    public void ChangeSelectProduct(int i)
    {
        buyId = i;
    }

    public void AddToNumber(int amount)
    {
        int currentNumber = int.Parse(numberText.text);
        currentNumber += amount;
        numberText.text = currentNumber.ToString();
    }

    // 텍스트에 적힌 숫자에서 빼기
    public void SubtractFromNumber(int amount)
    {
        int currentNumber = int.Parse(numberText.text);
        currentNumber -= amount;
        numberText.text = currentNumber.ToString();
    }

    void BuyId1()
    {
        carpet.SetActive(true);
        MissionManager.Instance.ChangeMissionStatus(6, true);
    }

    void BuyId2()
    {
        corner.SetActive(true);
        MissionManager.Instance.ChangeMissionStatus(7, true);
    }
}
