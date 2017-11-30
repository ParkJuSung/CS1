using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public GameObject cube;
    private static int N = 10;
    private  bool[] wall = new bool[100];
    
    // Use this for initialization
    void Start () {
        for (int i = 0; i < 11; i++)
        {
            Debug.DrawLine(new Vector3(i, 0, 0), new Vector3(i, 0, 10), new Color(255, 0, 0), Mathf.Infinity);
            Debug.DrawLine(new Vector3(0, 0, i), new Vector3(10, 0, i), new Color(255, 0, 0), Mathf.Infinity);
        }
        for (int i = 0; i < 100; i++)
        {
            if (Random.Range(1, 10) > 7)
            {
                wall[i] = true;
                Instantiate(cube, GetVector(i), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        

    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            GetNeighborNumber(hit.point);
            int[] neighobrNumber =  GetNeighborNumber(hit.point);
            Debug.Log("찍은 곳" + GetGridNumber(hit.point));
            foreach (int c in neighobrNumber)
            {
                Debug.Log(GetGridNumber(hit.point) + c);
            }
        }
    }

    static int GetGridNumber(Vector3 point)
    {
        
        return (int)point.x + (int)point.z * N;
    }

    static Vector3 GetVector(int GridNumber)
    {
        return new Vector3(GridNumber % N+0.5f, 0.5f, GridNumber /N+0.5f);
    }

    static int[] GetNeighborNumber(Vector3 point)
    {
        int tempGridNumber = GetGridNumber(point);
        List<int> neighbor = new List<int>() { N,N+1,1,-N+1,-N,-N-1,-1,N-1};

        if(tempGridNumber % N ==0 && tempGridNumber / N ==0)
            neighbor.RemoveAll(c => c == -1 || c == N - 1 || c == -N - 1 || c == -N || c == -N + 1); // 왼쪽 끝아래
        else if (tempGridNumber % N == N - 1 && tempGridNumber / N == 0)
            neighbor.RemoveAll(c => c == 1 || c == -N + 1 || c == N + 1 || c == -N || c == -N - 1); // 오른쪽 끝아래
        else if (tempGridNumber % N == 0 && tempGridNumber / N == N-1)
            neighbor.RemoveAll(c => c == -1 || c == N - 1 || c == N || c == N + 1 || c == -N - 1); //왼쪽 끝위
        else if(tempGridNumber %  N == N-1 && tempGridNumber / N == N-1)
            neighbor.RemoveAll(c => c == 1 || c == N - 1 || c == N || c == N + 1 || c == -N + 1); //오른쪽 끝위
        else if (tempGridNumber % N == 0)
            neighbor.RemoveAll(c => c == -1 || c == N - 1 || c == -N - 1); //왼쪽
        else if (tempGridNumber / N == 0)
            neighbor.RemoveAll(c => c == -N - 1 || c == -N || c == -N + 1); // 맨아래
        else if (tempGridNumber % N == N - 1)
            neighbor.RemoveAll(c => c == 1 || c == N + 1 || c == -N + 1); // 오른쪽
        else if (tempGridNumber / N == N - 1)
            neighbor.RemoveAll(c => c == N - 1 || c == N || c == c + 1); // 맨위

        return neighbor.ToArray();
    }
}
