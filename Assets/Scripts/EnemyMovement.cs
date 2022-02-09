using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private List<Waypoint> path;

   private void Start()
   {
      path = FindObjectOfType<PathFinder>().Path;
      StartCoroutine(MoveRoutine(1.50f));
   }


   private IEnumerator MoveRoutine(float delay)
   {
      foreach (Waypoint waypoint in path)
      {
         transform.position = new Vector3(waypoint.transform.position.x, transform.position.y,
            waypoint.transform.position.z);

         yield return new WaitForSeconds(delay);
      }
   }

}
