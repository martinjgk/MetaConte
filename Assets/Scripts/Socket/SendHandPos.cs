using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class SendHandPos : MonoBehaviour {
	[SerializeField]
	List<Transform> rightHandLandMark;
	[SerializeField]
	List<Transform> leftHandLandMark;

	float[] rightHand = new float[57];  // 0~2: wrist position, 3~ all rotation
	float[] leftHand = new float[57];

	List<float[]> recordedRightHandData = new List<float[]>();
	List<float[]> recordedLeftHandData = new List<float[]>();

	void FixedUpdate() {
		// Right hand data collection
		rightHand[0] = rightHandLandMark[0].localPosition.x;
		rightHand[1] = rightHandLandMark[0].localPosition.y;
		rightHand[2] = rightHandLandMark[0].localPosition.z;
		for (int i = 1; i < 18; i++) {
			rightHand[3 * i] = rightHandLandMark[i - 1].localRotation.x;
			rightHand[3 * i + 1] = rightHandLandMark[i - 1].localRotation.y;
			rightHand[3 * i + 2] = rightHandLandMark[i - 1].localRotation.z;
		}

		// Left hand data collection
		leftHand[0] = leftHandLandMark[0].localPosition.x;
		leftHand[1] = leftHandLandMark[0].localPosition.y;
		leftHand[2] = leftHandLandMark[0].localPosition.z;
		for (int i = 1; i < 18; i++) {
			leftHand[3 * i] = leftHandLandMark[i - 1].localRotation.x;
			leftHand[3 * i + 1] = leftHandLandMark[i - 1].localRotation.y;
			leftHand[3 * i + 2] = leftHandLandMark[i - 1].localRotation.z;
		}

		// Clone and store the data
		recordedRightHandData.Add((float[])rightHand.Clone());
		recordedLeftHandData.Add((float[])leftHand.Clone());


		// JSON 데이터를 전송하는 코루틴을 시작합니다.
		StartCoroutine(SendDataToServer(rightHand, leftHand));
	}

	public class HandData {
		public float[] rightHand;
		public float[] leftHand;
	}

	public IEnumerator SendDataToServer(float[] rightHandData, float[] leftHandData) {

		// 데이터를 JSON 형식으로 변환
		HandData data = new HandData();
		data.rightHand = rightHandData;
		data.leftHand = leftHandData;

		// 데이터를 JSON 형식으로 변환합니다.
		string jsonData = JsonUtility.ToJson(data);

		// 서버 URL 정의
		string serverUrl = "http://3.35.214.173:8501/receive_data";

		// UnityWebRequest를 사용하여 POST 요청 생성
		UnityWebRequest request = new UnityWebRequest(serverUrl, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		// 요청 전송 및 응답 대기
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
			Debug.LogError("Error sending data: " + request.error);
		}
		else {
			Debug.Log("Successfully sent data: " + request.downloadHandler.text);
		}
	}
}
