# Team22SoloProject
개인 프로젝트

## 필수 기능
- [완료] **기본 이동 및 점프** `Input System`, `Rigidbody ForceMode` (난이도 : ★★☆☆☆)
    - 플레이어의 이동(WASD), 점프(Space) 등을 설정
- [완료] **체력바 UI** `UI` (난이도 : ★★☆☆☆)
    - UI 캔버스에 체력바를 추가하고 플레이어의 체력을 나타내도록 설정. 플레이어의 체력이 변할 때마다 UI 갱신.
- [완료] **동적 환경 조사** `Raycast` `UI` (난이도: ★★★☆☆)
    - Raycast를 통해 플레이어가 조사하는 오브젝트의 정보를 UI에 표시.
    - 예) 플레이어가 바라보는 오브젝트의 이름, 설명 등을 화면에 표시.
- [완료] **점프대** `Rigidbody ForceMode` (난이도 : ★★★☆☆)
    - 캐릭터가 밟을 때 위로 높이 튀어 오르는 점프대 구현
    - **OnCollisionEnter** 트리거를 사용해 캐릭터가 점프대에 닿았을 때 **ForceMode.Impulse**를 사용해 순간적인 힘을 가함.
- [완료] **아이템 데이터** `ScriptableObject` (난이도 : ★★★☆☆)
    - 다양한 아이템 데이터를 `ScriptableObject`로 정의. 각 아이템의 이름, 설명, 속성 등을 `ScriptableObject`로 관리
- [완료] **아이템 사용** `Coroutine` (난이도 : ★★★☆☆)
    - 특정 아이템 사용 후 효과가 일정 시간 동안 지속되는 시스템 구현
    - 예) 아이템 사용 후 일정 시간 동안 스피드 부스트.
 
## 도전 기능
- [완료] **추가 UI** (난이도 : ★★☆☆☆)
    - 점프나 대쉬 등 특정 행동 시 소모되는 스태미나를 표시하는 바 구현
    - 이 외에도 다양한 정보를 표시하는 UI 추가 구현
- [완료] **3인칭 시점** (난이도 : ★★★☆☆)
    - 기존 강의의 1인칭 시점을 3인칭 시점으로 변경하는 연습
    - 3인칭 카메라 시점을 설정하고 플레이어를 따라다니도록 설정
- [완료] **움직이는 플랫폼 구현** (난이도 : ★★★☆☆)
    - 시간에 따라 정해진 구역을 움직이는 발판 구현
    - 플레이어가 발판 위에서 이동할 때 자연스럽게 따라가도록 설정
- [완료] **벽 타기 및 매달리기** (난이도 : ★★★★☆)
    - 캐릭터가 벽에 붙어 타고 오르거나 매달릴 수 있는 시스템 구현.
    - Raycast와 ForceMode를 함께 사용해 벽에 닿았을 때 적절한 물리적 반응을 구현
- [완료] **다양한 아이템 구현** (난이도 : ★★★★☆)
    - 추가적으로 아이템을 구현해봅니다.
    - 예) 스피드 부스트(Speed Boost): 플레이어의 이동 속도를 일정 시간 동안 증가시킴.
    더블 점프(Double Jump): 일정 시간 동안 두 번 점프할 수 있게 함.
    무적(Invincibility): 일정 시간 동안 적의 공격을 받지 않도록 함.
- [완료] **장비 장착** (난이도 : ★★★★☆)
    - 장비를 장착하여 캐릭터의 능력을 강화하는 시스템 구현
    - 예) 속도 증가 장비, 점프력 증가 장비 등
- [완료] **레이저 트랩** (난이도 : ★★★★☆)
    - Raycast를 사용해 특정 구간을 레이저로 감시하고, 플레이어가 레이저를 통과하면 경고 메시지나 트랩 발동
- [완료] **상호작용 가능한 오브젝트 표시** (난이도 : ★★★★★)
    - 상호작용 가능한 오브젝트에 마우스를 올리면 해당 오브젝트에 UI를 표시
    - 예) 문에 마우스를 올리면 'E키를 눌러 열기' 텍스트 표시.
    레버(Lever): 'E키를 눌러 당기기' 텍스트 표시.
    상자(Box): 'E키를 눌러 열기' 텍스트 표시.
    버튼(Button): 'E키를 눌러 누르기' 텍스트 표시.
- [완료] **플랫폼 발사기** (난이도 : ★★★★★)
    - 캐릭터가 플랫폼 위에 서 있을 때 특정 방향으로 힘을 가해 발사하는 시스템 구현특정 키를 누르거나 시간이 경과하면 ForceMode를 사용해 발사
- [완료] **발전된 AI** (난이도 : ★★★★★)
    - AI Navigation 시스템을 활용하여 맵에 다양한 구조물에 대한 계산 가중치를 설정
    - **Navigation 시스템 활용 예시**
        - 예1) 동적 베이크 `NavMeshSurface.BulidNavMesh()`
            - 에디터에서 NavMeshSurface를 베이크하는 것이 아닌 런타임에 동적으로 베이크를 수행
        - 예2) 구조물의 가중치를 설정 `NavMeshObstacle`, `NavMesh Modifier`
            - 특정 구조물에 NavMeshObstacle 컴포넌트를 추가하여 AI 캐릭터가 해당 구조물을 피하게 설정합니다. Carve 옵션을 활성화하여 AI가 구조물을 정확히 인식하도록 합니다.
            - 특정 구조물이나 영역에 대해 가중치를 설정하려면 Navigation 창의 Areas 탭에서 새로운 Area Type을 정의합니다.
            예를 들어, `HighCostArea`라는 새로운 영역을 만들고, 이 영역에 높은 가중치를 설정할 수 있습니다.
            - 구조물에 [NavMesh Modifier 컴포넌트](https://docs.unity3d.com/kr/2020.3/Manual/class-NavMeshModifier.html)를 추가하여 특정 Area Type을 할당합니다. 이를 통해 AI 캐릭터가 특정 구조물을 지나갈 때 더 높은 비용을 지불하게 됩니다.
        - 예3) 가중치에 따른 경로 변경 `NavMeshAgent.CalculatePath()`
            - AI 캐릭터가 이동 중 특정 구조물을 만나면, 가중치에 따라 경로를 변경하거나 피하는 로직을 구현합니다. 이를 통해 AI 캐릭터가 고유한 경로 선택 전략을 가지게 됩니다.

## 트러블 슈팅

1. Cinemachine 3인칭 기능 구현
문제 : FreeLook 으로 3인칭 기능을 구현 했으나 부자연스럽게 캐릭터를 따라오고 덜덜거림 현상이 생김.
해결 : 현 시점에서 우선 Camera의 rotation을 직접 변경하는 방식으로 수정함.

2. 사다리에 매달리는 기능 구현
문제 : 캐릭터가 사다리를 정면으로 바라보고 매달리기 시도할 때 바로 움직이지 않는 문제가 생김.
해결 : 방향 키 입력시 지속적으로 값을 업데이트 해줘야 하는데, Move 함수를 한번만 호출하는 문제가 생겨서 Update 시에 moveDirection 값을 지속적으로 전달 하도록 함.

3. NavMesh를 동적으로 Bake 하는 기능 구현
문제 : 동적으로 NavMeshSurface를 BuildNavMesh 하도록 했지만, 반복되는 에러 발생으로 제대로 Bake 되지 않음.
해결 : Unity Editor는 Bake하는데 권한에 대해서는 상관없지만 Runtime에 NavMeshSurface를 BuildNavMesh 하면 읽기/쓰기 권한이 없어서 Bake가 불가능 한데, 이 문제는 Mesh가 포함된 Model 에 Read/Write 권한을 부여 해주면, 동적으로 Bake 가능 하다.
그리고 비슷 문제로 Scene의 Root 에서 권한이 없다는 에러가 발생하면 ProjectSettings > Player > Other Settings > Static Batching 을 체크 해제 해주면 해결 된다.

