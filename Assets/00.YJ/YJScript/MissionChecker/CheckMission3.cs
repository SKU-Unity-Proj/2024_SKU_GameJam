using Unity.VisualScripting;
using UnityEngine;

namespace SojaExiles
{
    public class CheckMission3 : MonoBehaviour
    {
        public opencloseDoor door;
        private bool isOpen;

        private void Start()
        {
            door = FindObjectOfType<opencloseDoor>();
        }

        private void Update()
        {
            if (door != null)
            {
                // ScriptB의 bool 변수를 ScriptA의 bool 변수로 설정합니다.
                door.open = this.isOpen;
            }

            if(isOpen)
            {
                MissionManager.Instance.ChangeMissionStatus(3, false);
            }
            else
            {
                MissionManager.Instance.ChangeMissionStatus(3, true);
            }
        }
    }
}