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
            // �������� �̵��ϸ鼭
            Vector3 movePosition = Vector3.MoveTowards(transform.position, destinations[currentDestinationIndex], speed * Time.fixedDeltaTime);
            transform.position = movePosition;
            // ���� �̵��� ��ġ�� ���� ��ġ�� ���� �̵����� ���ϰ�
            Vector3 movement = movePosition - lastMovePosition;
            if (player != null) // �÷��̾ �г� ���� ������ �÷��̾ �Ա� �����̰� �Ѵ�.
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
            // ĳ������ �浹 ���� �����ͼ�
            ContactPoint contact = collision.contacts[0];
            // ���� ���� ���� ĳ������ �Ʒ��� �������� ������ ���ؼ� 
            if (Vector3.Dot(contact.normal, Vector3.down) > 0.7f)
            {
                // ĳ���Ͱ� ���� ���� �ö� ������ �Ǵ��Ѵ�.
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
