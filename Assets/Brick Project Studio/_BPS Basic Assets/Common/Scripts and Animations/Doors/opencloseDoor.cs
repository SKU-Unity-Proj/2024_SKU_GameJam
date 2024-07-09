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

		void Start()
		{
			open = false;
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                Player = playerObject.transform;
            }
        }

		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 5f)
					{
						if (open == false)
						{
							if (Input.GetMouseButtonDown(0))
							{
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StartCoroutine(closing());
								}
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
			yield return new WaitForSeconds(0.5f);
		}

		IEnumerator closing()
		{
			//print("you are closing the door");
			openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(0.5f);
		}


	}
}