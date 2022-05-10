using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전체 다 주로 사용
public class CSVData : MonoBehaviour  
{
    // 스태틱으로 IMU_Data 리스트 생성
    public static List<KartGame_Data> Data = new List<KartGame_Data>();

    // 경로 얻기
    public static string getPath()
    {
        // ex) Application.dataPath이 뭔지 + 뭐하는 코드인지  => 프로젝트디렉토리에 저장할때 사용 
        return Application.dataPath + "/CSV/" + "Saved_data_" + System.DateTime.Now.ToString("yyyy년MM월dd일_HH시mm분ss초") + ".csv";
    }
}

// 저장할 데이터 형식들
public class KartGame_Data
{
    //public Vector3 Accelerometer { get; set; } // 가속도 x, y, z값
    //public Vector3 Input_gyro_attitude_eulerAngles { get; set; } // 자이로_angle x, y, z값
    //public Vector3 Input_gyro_rotationRateUnbiased { get; set; } // 자이로_speed x, y, z값
    public float playtime { get; set; } // 플레이타임
    public float carspeed { get; set; }
    public bool isbooster { get; set; }
}
