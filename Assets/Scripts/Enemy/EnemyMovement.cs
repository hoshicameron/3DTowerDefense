using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] private GameObject endPathExplosion = null;
   [SerializeField] private float speed;


   private List<Waypoint> path;
   private int index=1;

   private void OnEnable()
   {
      path = FindObjectOfType<PathFinder>().GetPath();
      index = 1;

      transform.position = new Vector3(path[0].transform.position.x, transform.position.y,
         path[0].transform.position.z);
   }

   private void Update()
   {
      if(GameManager.Instance.IsGameEnded)   return;

      Move();
   }

   private void Move()
   {
      if (index == path.Count)
      {
         GameObject endPathVFX = PoolManager.Instance.ReuseObject(endPathExplosion,
            new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity);
         endPathVFX.SetActive(true);
         gameObject.SetActive(false);
      } else
      {
         float step = speed * Time.deltaTime;

         Vector3 destinationPosition = new Vector3(
            path[index].transform.position.x,
            transform.position.y,
            path[index].transform.position.z
         );

         transform.position=Vector3.MoveTowards(transform.position, destinationPosition, step);

         if (Vector3.Distance(transform.position, destinationPosition) < Mathf.Epsilon)
         {
            index++;
         }
      }


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
