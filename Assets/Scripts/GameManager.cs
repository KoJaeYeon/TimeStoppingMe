using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 정적 변수
    private static GameManager instance;

    // 인스턴스에 접근할 수 있는 정적 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없으면 새로 생성
            if (instance == null)
            {
                // 새로운 GameObject를 생성하고 GameManager 컴포넌트를 추가
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<GameManager>();
                singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";

                // 씬이 바뀌어도 파괴되지 않도록 설정
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    // 다른 스크립트가 이 클래스를 직접 생성하지 못하도록 private 생성자
    protected GameManager() { }

    // 필요한 초기화 코드
    private void Awake()
    {
        // 이미 인스턴스가 존재하는 경우 새로운 인스턴스를 파괴
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // 여기에 GameManager의 다른 메서드와 기능을 추가
}
