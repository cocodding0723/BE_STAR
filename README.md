# BE-STAR 기획서

## `0.목차`

---

---

## `1.플레이어`

--- 

### `1.1 플레이어의 기본 행동`

플레이어의 인 게임내의 행동 순서는 다음과 같습니다.

1. 맵에 존재하는 삼각형을 선택합니다.
2. 삼각형을 발사할 방향 반대 방향으로 드래그 하여 발사 위치와 힘을 조절합니다.
3. 손을 놓으면 삼각형이 날라갑니다.

---

### `1.2 플레이어가 발사한 삼각형의 특징`

플레이어가 발사한 삼각형의 특징은 다음과 같으며 맵에 존재하는 다른 도형들은 아래의 특징을 가지지 못합니다.

> 예시 : 플레이어가 발사하지 않은 삼각형이 자연적으로 충돌한 경우 합쳐지거나 폭발하지 않습니다.

1. 플레이어가 발사한 삼각형은 다른 도형과 충돌시 각이 하나씩 증가합니다.
2. 각이 육각형 초과된 도형이 될시 별이 되어서 터집니다.
3. 터진 별은 주변 도형들을 폭발 시킵니다.

> 자세한 사항은 `2.적` 에서 확인해주세요.

---

## `2.적`

---

적은 플레이어가 선택한 도형이 아닌 맵에 배치되있는 다른 도형들을 의미합니다.

### `2.1. 종류`


---

## `3.맵`
1. 적 생성 방식
2. 맵 특성
	- 지형 지물들 특성 정의
		예 ) 우주-행성 : 중력이 있어서 5m 날라가는 오브젝트를 끌어당김
		      하늘-구름 : 플레이어가 닿으면 반사각으로 다시 튕김, 힘이 20% 줄어듬
		      우주 : 무중력 날라갈때 저항값 x
	                   지상 : 날라갈때 저항값 o 

## `4.시스템`
1. 플레이어 날리는 횟수
	
2. 점수
	삼각형 폭발 : 100
	사각형 폭발 : 200
	오각형 폭발 : 300
	육각형 폭발 : 400
	별 폭발 : 1000
3. 시간제한?

## `5.UI`
스토리보드 작성

1. 메인화면
2. 맵 선택화면
3. 인게임 UI
4. 설정

BGM/사운드/효과음

