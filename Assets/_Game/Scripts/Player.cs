using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    private Vector3 targetPosition;
    private float horizontal;
    private float verticial;
    [SerializeField] private float speed;
    private Vector3 moveDirection;
    private RaycastHit hit; 

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float stepSize = 1f;
    private bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        targetPosition = transform.position;
        //Debug.Log("targetPosition: " + targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        //verticial = Input.GetAxisRaw("Vertical");

        //rb.linearVelocity = new Vector3(horizontal * speed * Time.deltaTime, rb.linearVelocity.y, verticial * speed * Time.deltaTime);


        //lay vi tri khi cham va khi buong 
        if(isMoving)
        {
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouchPosition = Input.mousePosition;
                //Debug.Log("startTouchPosition: " + startTouchPosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                endTouchPosition = Input.mousePosition;
                //Debug.Log("endTouchPosition: " + endTouchPosition);
            }

            Vector2 swipeVector = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
        {
            //if (swipeVector.x > 0)
            //{
            //    //Debug.Log("phai");
            //}
            //else
            //{
            //    //Debug.Log("trai");
            //}
            moveDirection = swipeVector.x > 0 ? Vector3.right : Vector3.left;
            //Debug.Log("moveDirection: " + moveDirection);
        }
        else
        {
            //if (swipeVector.y > 0)
            //{
            //    //Debug.Log("len");
            //}
            //else
            //{
            //    //Debug.Log("xuong");
            //}

            moveDirection = swipeVector.y > 0 ? Vector3.forward : Vector3.back;
            
        }
        //Debug.Log("moveDirection: " + moveDirection);

        MoveToTargetPos(moveDirection);
        }
        

        
    }



    private void MoveToTargetPos(Vector3 direction)
    {
        Vector3 checkPosition = targetPosition;
        Vector3 nextPosition = checkPosition + direction * stepSize;

        int bricksCollected = 0;

        //Debug.Log("moveDirection: " + moveDirection);

        //banws 1 tia raycast theo hướng direction để kiểm tra có chạm vào tag("UnGroundBrick") không 
        // nếu mà chạm thì đi tiếp quãng đường 1f nữa, nếu mà không chạm thì dừng lại và lấy vị trí đó làm targetPosition

        while (Physics.Raycast(checkPosition, direction, out hit, stepSize))
        //giải thích: checkPosition là vị trí hiện tại,
        //direction là hướng di chuyển,
        //hit là biến để lưu thông tin va chạm,
        //stepSize là khoảng cách kiểm tra
        {
            if (hit.collider.CompareTag("UnGroundBrick"))
            {
                //check xem co ton tai child "GroundBrick" trong collider của UnGroundBrick khong
                Transform groundBrickTransform = hit.collider.transform.Find("GroundBrick");

                if (groundBrickTransform != null && groundBrickTransform.gameObject.activeSelf)
                //activeSelf la kiem tra xem GroundBrick co dang duoc kich hoat hay khong, neu co thi se deactive no
                {
                    groundBrickTransform.gameObject.SetActive(false);
                    //Debug.Log("Deactivate GroundBrick: " + groundBrickTransform.gameObject.name);

                    bricksCollected++;
                }

                checkPosition += direction * stepSize;
                nextPosition = checkPosition + direction * stepSize;
                //Debug.Log("checkPosition: " + checkPosition);

            }
            else
            {
                break;
            }
        }

        GetComponent<Brick>().AddBrick(bricksCollected);



        // di chuyển player từ vị trí hiện tại đến targetPosition = moveToWards
        targetPosition = checkPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
    }
}
