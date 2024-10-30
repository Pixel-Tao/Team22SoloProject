using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovePanel : MoveableObject, ILeverTarget
{
    private Vector3 lastMovePosition;
    private PlayerCondition player;

    protected override void Start()
    {
        lastMovePosition = transform.position;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (destinations.Count > 0)
        {
            // ���� �̵��� ��ġ�� ���� ��ġ�� ���� �̵����� ���ϰ�
            Vector3 movement = currentMovePosition - lastMovePosition;
            if (player != null) // �÷��̾ �г� ���� ������ �÷��̾ �Ա� �����̰� �Ѵ�.
                player.transform.position += movement;
            lastMovePosition = currentMovePosition;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerCondition player))
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
        if (collision.gameObject.TryGetComponent(out PlayerCondition player))
        {
            this.player = null;
        }
    }

    public void LeverOn()
    {
        enabled = true;
    }

    public void LeverOff()
    {
        enabled = false;
    }
}
