using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //tag
    public bool StartTag = false;
    public bool EndTag = false;
    public bool GameOn = false;
    public int WhoTag = 0;
    //get target
    public StandardCheckAttack Standard;
    public SentryCheckAttack Sentry;
    public BaseCheckAttack Base;
    public GameObject StartView, ExitView, RestartView;
    public GameObject Car,Red,Blue;
    //button
    private Button _start, _restart, _exit;
    //image
    private Image _background,_win,_lose;
    //start position
    public Vector3 StartPosition=new Vector3(3.47399997f,0.41199997067451479f,2.977908134460449f);
    public Vector3 RedPosition = new Vector3(-4.2069997787475f,1.5889999866485f,-1.1160000562f);
    public Vector3 BluePosition = new Vector3(4.2059998512f,1.67599999904f,1.083999991416f);
    
    void Start()
    {
        //start
        StartTag = false;
        EndTag = true;
        //get target
        Base =GameObject.Find("board").GetComponent<BaseCheckAttack>();
        Sentry = GameObject.Find("testRed").GetComponent<SentryCheckAttack>();
        Standard = GameObject.Find("Car").GetComponent<StandardCheckAttack>();
        //get button
        _start = GameObject.Find("Start").GetComponent<Button>();
        _restart = GameObject.Find("Restart").GetComponent<Button>();
        _exit = GameObject.Find("Exit").GetComponent<Button>();
        //set tag
        Base.FailTagBase = true;
        Sentry.FailTagSentry = true;
        Standard.FailTagStandard = true;
        //get gameobject
        StartView=GameObject.Find("Start");         
        RestartView=GameObject.Find("Restart");      
        ExitView=GameObject.Find("Exit");
        Car = GameObject.Find("Car");
        Red = GameObject.Find("testRed");
        Blue = GameObject.Find("testBlue");
        //get image
        _background = GameObject.Find("background").GetComponent<Image>(); 
        _win = GameObject.Find("Win").GetComponent<Image>(); 
        _lose = GameObject.Find("Lose").GetComponent<Image>();
        GameOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //prepare
        if (EndTag == true && StartTag == false && GameOn==false)
        {
            //show start button and title & hide other button
            StartView.SetActive(true);
            RestartView.SetActive(false);
            ExitView.SetActive(false);
            //set tag
            Base.FailTagBase = true;
            Sentry.FailTagSentry = true;
            Standard.FailTagStandard = true;
            GameOn = false;
            WhoTag = 0;
            //release mouse
            Cursor.lockState = CursorLockMode.None;
            //show background
            _background.fillAmount = 1;
            //hide win or lose title
            _win.fillAmount = 0;
            _lose.fillAmount = 0;
            //set initial position
            Car.transform.position=StartPosition;
            Red.transform.position=RedPosition;
            Blue.transform.position=BluePosition;
            //add listener
            _start.onClick.AddListener(delegate { ClickStart(); });
            
            //Debug.Log("pre");
            
        }
        
        //game start
        if (StartTag == true && EndTag==true && GameOn==false)
        {
            //lock mouse
            ShowMouse();
            //initial tag
            Base.FailTagBase = false;           
            Sentry.FailTagSentry = false;       
            Standard.FailTagStandard = false;
            Red.GetComponent<SentryShoot>().AwakeFlag = false;
            //initial blood
            Base.BlueBase.Blood = 2000;
            Base.Initial();
            Standard.myRobot.Blood = 500;
            Sentry.myRobot1.Blood = 500;
            //hide start button
            StartView.SetActive(false);
            RestartView.SetActive(false);
            ExitView.SetActive(false);
            //hide background
            _background.fillAmount = 0;
            //change tag
            EndTag = false;
            GameOn = true;
            //hide win or lose title
            _win.fillAmount = 0;
            _lose.fillAmount = 0;
        }
        
        //who tag
        if (Base.FailTagBase == true || Standard.FailTagStandard == true)
        {
            if (StartTag == true && GameOn == true)
            {
                //1 win 2 lose
                if (Base.FailTagBase == true)
                {
                    WhoTag = 1;
                }
                else
                {
                    WhoTag = 2;
                }
            }
        }
        
        //if game over
        if (WhoTag!=0 )
        {
            //show background
            _background.fillAmount = 1;
            //show title
            if (WhoTag==1)
            {
                _win.fillAmount = 1;
            }
            else if (WhoTag==2)
            {
                _lose.fillAmount = 1;
            }
            //set tag
            Base.FailTagBase = true;
            Sentry.FailTagSentry = true;
            Standard.FailTagStandard = true;
            //release mouse
            Cursor.lockState = CursorLockMode.None;
            //show restart and exit button
            StartView.SetActive(false);
            RestartView.SetActive(true);  
            ExitView.SetActive(true);
            //change tag
            EndTag = true;
            //add listener
            _restart.onClick.AddListener(delegate { ClickRestart(); });
            _exit.onClick.AddListener(delegate { ClickExit(); });
                
            
        }
        
    }

    public void ClickStart()
    {
        StartTag = true;
        //Debug.Log(StartTag);
    }
    public void ClickRestart()      
    {                             
        StartTag = false;
        EndTag = true;
        GameOn = false;
        //Debug.Log("restart");
    }
    public void ClickExit()      
    {                             
        //Application.Quit();
        GameOn = false;
        #if UNITY_EDITOR //如果是在编辑器环境下
                UnityEditor.EditorApplication.isPlaying = false;
        #else//在打包出来的环境下
            Application.Quit();
        #endif
        //Debug.Log("exit");
    }
    void ShowMouse()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
