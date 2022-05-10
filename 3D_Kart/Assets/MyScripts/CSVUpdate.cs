using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ReadLineFile 빼고는 거의 다 필수적으로 사용함
public class CSVUpdate : MonoBehaviour
{
    private static CSVUpdate instance;        // 싱글톤?? 스태틱 변수 생성

    // 주로 사용
    private void Awake()                      // 어웨이크에다가
    {
        if (instance != null)                 // 인스턴스가 널이 아니면 
        {
            Destroy(instance);                // 생성되면 안되므로 파괴한다
        }
        instance = this;                      // 인스턴스는 이것
    }

    // 주로 사용
    public static CSVUpdate GetInstance()     // 싱글톤 인스턴스를 반환해 주는 멤버 함수
    {
        // ex) 24행에서 리턴되는 거랑 27행에서 리턴되는 경우가 각각 어떻게 다른지 이런 느낌
        if (instance == null)                 // 인스턴스가 널인경우
        {
            Debug.LogError("CSV 인스턴스가 존재하지 않습니다.");  // 에러 프린트
            return null;                                          // 널 리턴
        }
        // 아니면 인스턴스 반환해줌
        return instance;
    }

    // 불러오기? 덮어쓰기?? 
    public void ReadLineFile(string file) // 아주 가끔 사용  <= 요기 매개값은 파일경로
    {
        if (System.IO.File.Exists(file))   // 매개값 경로에 파일 이름, 확장자 가 존재하면 돌린다
        {
            //List<Dictionary<string, object>> data = CSVReader.Read(file);       // 파일을 모두 읽어서 데이터들을 딕셔너리 리스트에 넣어줌

            //for (var i = 0; i < data.Count; i++)                                //  데이터 개수만큼 돌린다
            //{
            //    //    KartGame_Data Data = new KartGame_Data();                   // 클래스 객체 생성
            //    //    Data.Accelerometer = Device_Data_Load.instance.Accelerometer;
            //    //    Data.Input_gyro_attitude_eulerAngles = Device_Data_Load.instance.Input_gyro_attitude_eulerAngles;
            //    //    Data.Input_gyro_rotationRateUnbiased = Device_Data_Load.instance.Input_gyro_rotationRateUnbiased;
            //    //    Data.playtime = Device_Data_Load.instance.playtime;                 
            //    //    CSVData.IMU_Data.Add(Data);                                     //     데이터 애드 해줌
            //}
        }
    }

    // 데이터 쓰기
    public void UpdateLineFile() // 주로 사용
    {
        string filePath = CSVData.getPath();                          // 경로 스트링 변수 저장
        StreamWriter outStream = System.IO.File.CreateText(filePath); // 스트림라이터 클래스 객체로 파일 경로에다가 텍스트생성
        string str;                                                   // 값을 받기 위해 스트링 변수 선언

        // 
        outStream.WriteLine("PlayTime," +
                            "carspeed," +
                            "isbooster"
                            );
        for(int i = 0; i < CSVData.Data.Count; i++)
        {
            str = CSVData.Data[i].playtime.ToString("N1") + ","
                + CSVData.Data[i].carspeed + ","
                + CSVData.Data[i].isbooster;
            outStream.WriteLine(str);
        }

        //// 
        //for (int i = 0; i < CSVData.IMU_Data.Count; i++)
        //{
        //    str = CSVData.IMU_Data[i].playtime.ToString("N1") + ","
        //        + CSVData.IMU_Data[i].Accelerometer.x + ","
        //        + CSVData.IMU_Data[i].Accelerometer.y + ","
        //        + CSVData.IMU_Data[i].Accelerometer.z + ","
        //        + CSVData.IMU_Data[i].Input_gyro_attitude_eulerAngles.x + ","
        //        + CSVData.IMU_Data[i].Input_gyro_attitude_eulerAngles.y + ","
        //        + CSVData.IMU_Data[i].Input_gyro_attitude_eulerAngles.z + ","
        //        + CSVData.IMU_Data[i].Input_gyro_rotationRateUnbiased.x + ","
        //        + CSVData.IMU_Data[i].Input_gyro_rotationRateUnbiased.y + ","
        //        + CSVData.IMU_Data[i].Input_gyro_rotationRateUnbiased.z;
        //    outStream.WriteLine(str); // 써주기
        //}
        outStream.Close();            // 파일 스트림 닫기 ( 안해주면 저장이 안될 수 있음 -by NON)
    }

    // 5초 이상 실행하지 않으면 저장하지 않는다.
    public void OnApplicationQuit() // 주로 사용
    {
        //if(Device_Data_Load.instance.playtime > 5.0f)
        //{
        UpdateLineFile();         // 위에 만든 함수 실행
        //    print("csv 만들어짐");
        //}
        //else
        //{
        //    print("5초 이내에 종료하면 csv 만들지 않게 설정해둠");
        //}    
    }
}
