using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNShoot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float power = 500;
    public Vector2 minPower;
    public Vector2 maxPower;
    public Rigidbody2D playerRigidbody;
    public LineRenderer lr;
    public GameObject ring;
    public GameObject main_ball;

    Camera cam;
    Game balls_movement;

    Vector3 startPos, endPos;
    Vector3 dragLength;
    bool isMoving;

    void Start()
    {
        cam = Camera.main;
        balls_movement = GameObject.Find("UI").GetComponent<Game>();
        ring.SetActive(false);
    }

    void Update()
    {
        isMoving = (playerRigidbody.velocity.magnitude < 0.0001) ? false : true;
        if (Input.GetMouseButton(0) && !isMoving && !balls_movement.are_balls_moving && main_ball.activeSelf)
        {
            RenderLine();
        }
        if (!isMoving && !balls_movement.are_balls_moving) ring.SetActive(true);
        else if (isMoving && ring.activeSelf && balls_movement.are_balls_moving) ring.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.pressPosition;
        startPos.z = 15;
    }

    public void OnDrag(PointerEventData eventData)
    {
        endPos = eventData.position;
        endPos.z = 15;

        dragLength = cam.ScreenToWorldPoint(startPos) - cam.ScreenToWorldPoint(endPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isMoving && !balls_movement.are_balls_moving)
        {
            Push();
            HideLine();
        }

        startPos = Vector2.zero;
        endPos = Vector2.zero;
    }

    void Push()
    {
        Vector2 force = new Vector2(Mathf.Clamp(dragLength.x, minPower.x, maxPower.x), Mathf.Clamp(dragLength.y, minPower.y, maxPower.y));
        playerRigidbody.AddForce(power * force, ForceMode2D.Impulse);
        balls_movement.are_balls_moving = true;
    }

    void RenderLine()
    {
        Vector3[] positions = new Vector3[2];

        Vector3 ballPos = main_ball.transform.position;
        Vector3 ballEnd = new Vector3(ballPos.x + (cam.ScreenToWorldPoint(endPos).x - cam.ScreenToWorldPoint(startPos).x), ballPos.y + (cam.ScreenToWorldPoint(endPos).y - cam.ScreenToWorldPoint(startPos).y), 15);
        ballPos.z = 15;

        positions[0] = ballPos;
        positions[1] = ballPos + (ballPos - ballEnd);

        lr.positionCount = 2;
        lr.SetColors(new Color32((byte) Mathf.Clamp((0 + (startPos - endPos).magnitude / 5), 0, 255), (byte) Mathf.Clamp((255 - (startPos - endPos).magnitude / 5), 0, 255), 0, 255), new Color32((byte) Mathf.Clamp((0 + (startPos - endPos).magnitude / 5), 0, 255), (byte) Mathf.Clamp((255 - (startPos - endPos).magnitude / 5), 0, 255), 0, 255));
        lr.SetPositions(positions);
    }

    void HideLine()
    {
        lr.positionCount = 0;
        ring.SetActive(false);
    }
}
