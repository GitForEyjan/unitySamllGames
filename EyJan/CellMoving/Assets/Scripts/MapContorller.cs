using UnityEngine;
using System.Collections;

public class MapContorller : MonoBehaviour {
	// 地图控制器
	public GameObject mapCell;
    public GameObject playerPrefab;
    public GameObject goldPrefab;
    public GameObject singleGoldPrefab;

    private GameObject[,] cellArray = new GameObject[3,3];

    private GameObject player;
    private int _playerPosX = 0;
    private int _playerPosY = 0;

    private bool _canMovePlayer = false;
    private Vector3 _mousePos = Vector3.zero;

    private GameObject gold;
    private int _goldPosX = 1;
    private int _goldPosY = 1;

    private string _lastMoveOfPlayer = "";

    private GameObject singleGold;

	void Awake () {
        this.initMapFloor();
        this.initPlayer();
        this.initGold();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.checkMounsEvent();
	}

    private void checkMounsEvent() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !this._canMovePlayer)
        {
            this._canMovePlayer = true;
            this._mousePos = Input.mousePosition;
        }
        
        if (Input.GetKeyUp(KeyCode.Mouse0) && this._canMovePlayer) 
        {
            this.resetMoveState();
        }

        this.OnMounsMove();
    }

    private void OnMounsMove()
    {
        if (this._canMovePlayer)
        {
            Vector3 mousePos = Input.mousePosition;

            float mouseXDir = mousePos.x - this._mousePos.x;
            float mouseYDir = mousePos.y - this._mousePos.y;


            if (mouseXDir < 0 && mouseXDir < -30f)
            {
                this.resetMoveState();
                this._playerPosX -= 1;
                this._playerPosX = (this._playerPosX < 0) ? 0 : this._playerPosX;
                this._lastMoveOfPlayer = "left";
            }
            else if (mouseXDir > 0 && mouseXDir > 30f)
            {
                this.resetMoveState();
                this._playerPosX += 1;
                this._playerPosX = (this._playerPosX > 2) ? 2 : this._playerPosX;
                this._lastMoveOfPlayer = "right";
            }
            else if (mouseYDir < 0 && mouseYDir < -30f)
            {
                this.resetMoveState();
                this._playerPosY -= 1;
                this._playerPosY = (this._playerPosY < 0) ? 0 : this._playerPosY;
                this._lastMoveOfPlayer = "down";
            }
            else if (mouseYDir > 0 && mouseYDir > 30f)
            {
                this.resetMoveState();
                this._playerPosY += 1;
                this._playerPosY = (this._playerPosY > 2) ? 2 : this._playerPosY;
                this._lastMoveOfPlayer = "up";
            }

            this.setPlayerPos();
        }
    }

    private void initMapFloor()
    {
        Vector3 cellSzie = mapCell.gameObject.renderer.bounds.size;
        float width = cellSzie.x;
        float height = cellSzie.y;
        float posX = 0;
        float posY = 0;
        int lineNum = 0;
        int lowNum = 0;

        GameObject cellFloor;
        Transform cellFloorTransform;
        for (lineNum = 0; lineNum < 3; lineNum++)
        {
            for (lowNum = 0; lowNum < 3; lowNum++)
            {
                posX = lineNum * width;
                posY = lowNum * height;
                cellFloor = GameObject.Instantiate(this.mapCell) as GameObject;
                cellFloorTransform = cellFloor.transform;
                cellFloorTransform.parent = this.transform;
                cellFloorTransform.position = new Vector2(posX, posY);

                this.cellArray[lineNum,lowNum] = cellFloor;
            }
        }
    }

    private void initPlayer() {
        this.player = GameObject.Instantiate(this.playerPrefab) as GameObject;
        Transform playerTrans = this.player.transform;
        playerTrans.parent = this.transform;

        this.resetPlayerPos();
    }

    private void initGold()
    {
        this.gold = GameObject.Instantiate(this.goldPrefab) as GameObject;
        this.gold.transform.parent = this.transform;

        this.gold.transform.position = this.cellArray[this._goldPosX, this._goldPosY].transform.position;

        this.singleGold = GameObject.Instantiate(this.singleGoldPrefab) as GameObject;
        this.singleGold.transform.parent = this.transform;
        this.singleGold.transform.position = this.cellArray[2, 2].transform.position;
    }

    private void resetPlayerPos() { 
        this._playerPosX = 0;
        this._playerPosY = 0;
        Vector2 pos = this.cellArray[this._playerPosX, this._playerPosY].transform.position;

        this.player.transform.position = pos;
    }

    private void resetMoveState()
    {
        this._canMovePlayer = false;
        this._mousePos = Vector3.zero;
    }

    private void setPlayerPos() {
        if (this._playerPosX < 0) {
            this._playerPosX = 0;
        }
        else if (this._playerPosX > 2)
        {
            this._playerPosX = 2;
        }

        if (this._playerPosY < 0) {
            this._playerPosY = 0;
        }
        else if (this._playerPosY > 2)
        {
            this._playerPosY = 2;
        }

        Vector2 pos = this.cellArray[this._playerPosX, this._playerPosY].transform.position;

        this.player.transform.position = pos;
    }

    public void moveGold()
    {
        int moveX = 0;
        int moveY = 0;
        switch (this._lastMoveOfPlayer)
        {
            case "left":
            {
                moveX = this._goldPosX - 1;
                this._goldPosX = (moveX < 0) ? (this._goldPosX + 1) : moveX;
                break;
            }

            case "right":
            {
                moveX = this._goldPosX + 1;
                this._goldPosX = (moveX > 2) ? (this._goldPosX - 1) : moveX;
                break;
            }

            case "up":
            {
                moveY = this._goldPosY + 1;
                this._goldPosY = (moveY > 2) ? (this._goldPosY - 1) : moveY;
                break;
            }

            case "down":
            {
                moveY = this._goldPosY - 1;
                this._goldPosY = (moveY < 0) ? (this._goldPosY + 1) : moveY;
                break;
            }

            default:
                break;
        }

        Vector3 moveCellPos = this.cellArray[this._goldPosX, this._goldPosY].transform.position;
        this.gold.transform.position = moveCellPos;

        this.setPlayerPos();
    }
}
