using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialPanel : MonoBehaviour {

    public GameObject objLightPanel;
    public GameObject objLeftBtn;
    public GameObject objRightBtn;
    public GameObject objCenterBtn;
    public GameObject objTopBtn;
    public GameObject objBottomBtn;
    public GameObject objInvenBtn;

    private Image LightPanel;
    //private Image LeftBtn;
    //private Image RightBtn;
    //private Image CenterBtn;
    //private Image CenterTopBtn;
    //private Image CenterBottomBtn;


    // Use this for initialization
    void Start()
    {

        LightPanel = objLightPanel.GetComponent<Image>();
        //LeftBtn = objLeftBtn.GetComponent<Image>();
        //RightBtn = objRightBtn.GetComponent<Image>();
        //CenterBtn = objCenterBtn.GetComponent<Image>();
        //CenterTopBtn = objCenterTopBtn.GetComponent<Image>();
        //CenterBottomBtn = objCenterBottomBtn.GetComponent<Image>();

    }

    public void OnLeftRight()
    {
        // 라이트 패널 세팅
        // 활성화
        objLightPanel.SetActive(true);

        // 알파조정
        LightPanel.color = new Color(0, 0, 0, 0.6f);

        // 버튼패널 활성화
        // 왼쪽
        objLeftBtn.SetActive(true);
        // 오른쪽
        objRightBtn.SetActive(true);
    }
    public void OffLeftRight()
    {
        // 라이트 패널 끄기
        objLightPanel.SetActive(false);

        // 버튼패널 끄기
        // 왼쪽
        objLeftBtn.SetActive(false);
        // 오른쪽
        objRightBtn.SetActive(false);

    }

    public void OnCenter()
    {
        objLightPanel.SetActive(true);
        LightPanel.color = new Color(0, 0, 0, 0.6f);
        objCenterBtn.SetActive(true);
    }
    public void OffCenter()
    {
        objLightPanel.SetActive(false);
        objCenterBtn.SetActive(false);
    }



    public void OnTopBottom()
    {
        objLightPanel.SetActive(true);
        LightPanel.color = new Color(0, 0, 0, 0.6f);
        objTopBtn.SetActive(true);
        objBottomBtn.SetActive(true);
    }
    public void OffTopBottom()
    {
        objLightPanel.SetActive(false);
        objTopBtn.SetActive(false);
        objBottomBtn.SetActive(false);
    }

    public void OnInven()
    {
        objLightPanel.SetActive(true);
        LightPanel.color = new Color(0, 0, 0, 0.6f);
        objInvenBtn.SetActive(true);
    }
    public void OffInven()
    {
        objLightPanel.SetActive(false);
        objInvenBtn.SetActive(false);
    }
    
}
