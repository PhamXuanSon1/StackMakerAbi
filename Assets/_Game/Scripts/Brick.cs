using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Transform player;   
    [SerializeField] private Transform playerBrick; 
    [SerializeField] private GameObject brickPrefab;

    private List<GameObject> brickList = new List<GameObject>();


    public void AddBrick(int bricksCollected)
    {
        float brickHeight = 0.2f; // Độ dày viên gạch (bạn hãy chỉnh số này cho khít)

        for (int i = 0; i < bricksCollected; i++)
        {
            GameObject newBrick = Instantiate(brickPrefab, playerBrick); // tạo gạch mới làm con của playerBrick để dễ quản lý

            // xét vị trí và xoay của gạch mới
            float yPos = brickList.Count * brickHeight;
            newBrick.transform.localPosition = new Vector3(0, yPos, 0);
            newBrick.transform.localRotation = Quaternion.identity;
            //Debug.Log(yPos);
            // 3. Thêm vào list để quản lý
            brickList.Add(newBrick);
        }

        if (player != null)
        {
            float totalHeight = brickList.Count * brickHeight;
            //Debug.Log("Total bricks: " + brickList.Count + ", Total height: " + totalHeight);
            player.localPosition = new Vector3(0, totalHeight, 0);
        }
    }
}