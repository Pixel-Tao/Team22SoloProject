using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanel : MonoBehaviour
{
    public List<Vector3> destinations;
    public float speed;

    private int currentDestinationIndex = 0;

    public Player player;
    private Vector3 lastMovePosition;

    private void Start()
    {
        lastMovePosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (destinations.Count > 0)
        {
            // 목적지를 이동하면서
            Vector3 movePosition = Vector3.MoveTowards(transform.position, destinations[currentDestinationIndex], speed * Time.fixedDeltaTime);
            transform.position = movePosition;
            // 현재 이동할 위치와 이전 위치를 빼서 이동량을 구하고
            Vector3 movement = movePosition - lastMovePosition;
            if (player != null) // 플레이어가 패널 위에 있으면 플레이어도 함깨 움직이게 한다.
                player.transform.position += movement;
            lastMovePosition = movePosition;
            if (
                Mathf.Approximately(transform.position.x, destinations[currentDestinationIndex].x) &&
                Mathf.Approximately(transform.position.y, destinations[currentDestinationIndex].y) &&
                Mathf.Approximately(transform.position.z, destinations[currentDestinationIndex].z)
                )
            {
                currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Count;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            // 캐릭터의 충돌 면을 가져와서
            ContactPoint contact = collision.contacts[0];
            // 법선 벡터 값을 캐릭터의 아래쪽 방향으로 내적을 구해서 
            if (Vector3.Dot(contact.normal, Vector3.down) > 0.7f)
            {
                // 캐릭터가 발판 위로 올라 갔는지 판단한다.
                this.player = player;
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            this.player = null;
        }
    }
}
