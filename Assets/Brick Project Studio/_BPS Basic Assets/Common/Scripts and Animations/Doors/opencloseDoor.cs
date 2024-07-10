using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;

		private SoundList openSound, closeSound;
		void Start()
		{
			open = false;
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                Player = playerObject.transform;
            }

			openSound = SoundList.doorOpen;
			closeSound = SoundList.Land;

		}

		void OnMouseDown()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 5f)
					{
						if (open == false)
						{
                            StartCoroutine(opening());
                        }
						else
						{
							if (open == true)
							{
                                StartCoroutine(closing());
                            }

						}

					}
				}

			}

		}

		IEnumerator opening()
		{
			//("you are opening the door");
			openandclose.Play("Opening");
			open = true;

			SoundManager.Instance.PlayOneShotEffect((int)openSound, this.transform.position, 3f);
			yield return new WaitForSeconds(0.5f);
		}

		IEnumerator closing()
		{
			//print("you are closing the door");
			openandclose.Play("Closing");
			open = false;

			SoundManager.Instance.PlayOneShotEffect((int)closeSound, this.transform.position, 3f);
			yield return new WaitForSeconds(0.5f);
		}


	}
}