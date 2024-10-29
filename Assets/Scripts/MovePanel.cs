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
            // 현재 이동할 위치와 이전 위치를 빼서 이동량을 구하고
            Vector3 movement = currentMovePosition - lastMovePosition;
            if (player != null) // 플레이어가 패널 위에 있으면 플레이어도 함깨 움직이게 한다.
                player.transform.position += movement;
            lastMovePosition = currentMovePosition;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerCondition player))
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
