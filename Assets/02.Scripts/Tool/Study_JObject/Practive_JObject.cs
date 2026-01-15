using System;
using System.Collections.Generic;
using System.IO;
using FileIO.JsonDataModule;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Random = UnityEngine.Random;

public class Practive_JObject : MonoBehaviour
{
   
}

/* JObject란?
 * 구체적인 데이터 클래스(DTO, Data Transfer Object)를 정의 하지 않고도
 * JSON 데이터를 다루기 위한 도구.
 * Unity에서 JSON을 다루기 위해 JsonUtility와 JObject(Newtonsoft.Json)
 * 두가지 중 Newtonsoft.Json에서만 사용 가능한 도구.
 * JsonUtility가 정적인 데이터 구조를 처리하는데에 최적화 되어있다면,
 * JObject는 비정형 데이터나 복잡한 중첩 구조를 유연하게 처리하는데 특화 되어 있습니다.
 *
 * JObject의 특징 3가지
 * - 동적 접근이 가능하다.
 * - 유연성 : 런타임에 추가, 삭제, 수정이 가능하다.
 * - Linq 지원 : JToken, JArray와 함께 사용되며, 복잡한 JSON 쿼리가 가능하다.
 *
 * JObject 사용시 주의사항
 * Garbage Collection(GC) 이슈
 * - JObject 파싱 과정에서 많은 참조 타입 객체를 생성 합니다. Update Loop에서는
 * 쓰지마세요. 초기화에 한번만 사용하는거는 괜찮음. 런타임시에 가끔 사용하는것도 OK
 * - 기본적으로 C# 리플렉션 기능을 사용합니다.
 * 느릴 수 있다는것(성능을 잡아먹는다.)을 기억해두십시오.
 */   