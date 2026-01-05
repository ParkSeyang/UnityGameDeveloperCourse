using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerDataManager
{
    void SaveData(string path);
    void LoadData(string path);
}

/* 유니티에서 자주 사용되는 경로(Editor, Build(Window, Android))
 *
 * 1. Application.dataPath(강사님은 잘 사용 안함.)
 * - 프로젝트의 핵심 데이터가 위치한 경로
 * - Editor : 프로젝트의 "Assets" 폴더를 가리킵니다. ({Project Path}/Assets)
 * - Window Build : 실행 파일의 데이터 폴더({Product Name}_Data 폴더)
 * - Android Build : 앱 패키지 내부의 프라이빗 영역(.apk, ReadOnly)
 * Android처럼 읽기 전용으로 되어있는 경우가 있음.
 * 데이터를 저장하기에는 부적합하다.
 *
 * 2. Application.streamingAssetsPath(유저들의 아이템이나 기본정보는 이걸로 사용한다.)
 * - 빌드 시 "Assets/StreamingAssets" 폴더의 내용이 그대로 복사되는 경로
 * - Editor : "Assets/StreamingAssets"
 * - Window Build : 실행 파일의 StreamingAssets 폴더 ({Product Name}/StreamingAssets)
 * - Android Build : jar:file:///data/app/com.Company.Game.apk/!/assets (URL 형태)
 * - System.IO.File 클래스로 접근이 불가능하며, 반드시 UnityWebRequest를 통해 데이터를 읽어야 합니다.
 * Better Streaming Assets 라이브러리(에셋 스토어) 사용하길 권장
 *
 * 3. Application.persistentDataPath(유저의 정보를 저장하거나 불러올때 Json과같이 사용)
 * 런타임중에 데이터를 저장하고 유지할 수 있는 샌드박스 경로입니다.
 * 프로그램의 정보에 따라 동적으로 변경되는 경로 입니다.
 * User Base Path(사용자 / App Data / LocalLow 경로) / Company Name / Product Name
 * - Editor : {사용자} / App Data / LocalLow / {Company Name} / {Product Name}
 * - Window Build : {사용자} / App Data / LocalLow / {Company Name} / {Product Name}
 * - Android Build : /storage/emulated/0/Android/data/[PackageName]/files
 */
