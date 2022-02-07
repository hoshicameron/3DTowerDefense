using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] private List<GameObject> path;

   private void Start()
   {
      StartCoroutine(MoveRoutine(1.0f));
   }


   private IEnumerator MoveRoutine(float delay)
   {
      foreach (GameObject waypoint in path)
      {
         transform.position = new Vector3(waypoint.transform.position.x, transform.position.y,
            waypoint.transform.position.z);

         yield return new WaitForSeconds(delay);
      }
   }
   private void Awake()
   {

   }
}
