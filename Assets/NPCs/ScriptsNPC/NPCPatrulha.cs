
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCPatrulha : MonoBehaviour
{
    public float distanciaAndar = 10f;
    public float tempoParado = 2f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Patrulhar());
    }

    IEnumerator Patrulhar()
    {
        while (true)
        {
            Vector3 ponto = transform.position +
                Random.insideUnitSphere * distanciaAndar;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(
                ponto, out hit, distanciaAndar, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);

                while (agent.pathPending ||
                       (agent.hasPath &&
                        agent.remainingDistance >
                        agent.stoppingDistance))
                {
                    yield return null;
                }
            }

            yield return new WaitForSeconds(tempoParado);
        }
    }
}

