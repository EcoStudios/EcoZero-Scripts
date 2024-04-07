using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class HostilePathfinding : MonoBehaviour
{
    public float radius = 15f;
    public float stoppingDistance = 3f;
    

    private void Update()
    {
        Mob mob = World.FindMobFromGameObject(gameObject);
        if (Physics.CheckSphere(transform.position, radius, LayerMask.GetMask("OBJ")))
        {
            mob.MeshAgent.isStopped = true;
            mob.IsAngry = true;
            ChasePlayer();
        }
        else
        {
            if (mob.IsAngry)
            {
                Search();
            }
            else
            {
                int random = Random.Range(2, 6);
                Invoke("Search", random);
            }
            mob.MeshAgent.isStopped = true;
        }
    }

    private void ChasePlayer()
    {
        Mob mob = World.FindMobFromGameObject(gameObject);
        mob.MeshAgent.isStopped = false;
        mob.MeshAgent.SetDestination(PlayerManager.Player.position);
        mob.MeshAgent.gameObject.transform.LookAt(new Vector3(PlayerManager.Player.position.x, 0, PlayerManager.Player.position.z));
        mob.MeshAgent.stoppingDistance = stoppingDistance;
    }

    private void Search()
    {
        Mob mob = World.FindMobFromGameObject(gameObject);
        mob.MeshAgent.isStopped = false;
        float randomz = Random.Range(-40, 40);
        float randomx = Random.Range(-40, 40);

        Vector3 vector3 = new Vector3(transform.position.x + randomx, transform.position.y,
            transform.position.z + randomz);
        
        

        if (mob.MeshAgent.CalculatePath(vector3, new NavMeshPath()))
        {
            mob.MeshAgent.SetDestination(vector3);
        }

        mob.IsAngry = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
        if (Physics.CheckSphere(transform.position, radius, LayerMask.GetMask("OBJ")))
        {
            Gizmos.color = Color.red;
        }
        else
        {
           Gizmos.color = Color.green;
        }
    }
}
