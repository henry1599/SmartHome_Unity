using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.IO;

public class HomeSceneManager : MonoBehaviour
    {
        public HomeHeaderTab roomTabButton;
        public HomeHeaderTab tempAndHumiTabButton;
        public HomeFooterTab homeFooterButton, statFooterButton, settingFooterButton;
        public StatisticHeaderTab dayHeaderButton, weekHeaderButton, monthHeaderButton;
        public SettingTab offEnergyButton, leftHomeButton, cameHomeButton;
        public AddRoomIconChoose livingRoomIcon, bathRoomIcon, bedRoomIcon, garageRoomIcon, kitchenIcon, officeIcon;
        public GameObject homeHeaderField, statHeaderField, settingHeaderField, addRoomHeaderField;
        public Animator roomtempPanelAnim;
        public Animator mainPanelAnim;
        public HomeState state;
        public DashBoardState dashBoardState;
        public Transform roomField;
        public List<Room> roomTemplates;
        bool isLoadedRooms;
        void Start()
        {

            state = HomeState.Home;
            isLoadedRooms = false;

            roomTabButton.SetStatus(true);
            tempAndHumiTabButton.SetStatus(false);

            homeFooterButton.SetStatus(true);
            statFooterButton.SetStatus(false);
            settingFooterButton.SetStatus(false);

            dayHeaderButton.SetStatus(true);
            weekHeaderButton.SetStatus(false);
            monthHeaderButton.SetStatus(false);

            offEnergyButton.SetStatus(false);
            leftHomeButton.SetStatus(false);
            cameHomeButton.SetStatus(false);

            livingRoomIcon.SetStatus(true);
            bathRoomIcon.SetStatus(false);
            bedRoomIcon.SetStatus(false);
            garageRoomIcon.SetStatus(false);
            kitchenIcon.SetStatus(false);
            officeIcon.SetStatus(false);

            DataTransferManager.OnRoomChangedFromHome += HandleRoomChangedFromHome;
        }
        void OnDestroy()
        {
            DataTransferManager.OnRoomChangedFromHome -= HandleRoomChangedFromHome;
        }
        void Update()
        {
            switch (state)
            {
                case HomeState.Home:
                    HandleHomeState();
                    break;
                case HomeState.Stat:
                    HandleStatState();
                    break;
                case HomeState.Setting:
                    HandleSettingState();
                    break;
                case HomeState.AddRoom:
                    HandleAddRoomState();
                    break;
            }

            mainPanelAnim.SetBool("isRoom", state == HomeState.Home);
            mainPanelAnim.SetBool("isStat", state == HomeState.Stat);
            mainPanelAnim.SetBool("isSetting", state == HomeState.Setting);

            homeFooterButton.SetStatus(state == HomeState.Home);
            statFooterButton.SetStatus(state == HomeState.Stat);
            settingFooterButton.SetStatus(state == HomeState.Setting);

            homeHeaderField.SetActive(state == HomeState.Home);
            statHeaderField.SetActive(state == HomeState.Stat);
            settingHeaderField.SetActive(state == HomeState.Setting);
            addRoomHeaderField.SetActive(state == HomeState.AddRoom);
        }
        public void HandleHomeState()
        {
            switch (dashBoardState)
            {
                case DashBoardState.Room:
                    HandleRoomDashBoardState();
                    break;
                case DashBoardState.Temp:
                    HandleTempDashBoardState();
                    break;
            }
        }
        public void HandleStatState()
        {
            isLoadedRooms = false;
        }
        public void HandleSettingState()
        {
            isLoadedRooms = false;

        }
        public void HandleAddRoomState()
        {
            isLoadedRooms = false;

        }
        public void HandleRoomDashBoardState()
        {
            roomTabButton.SetStatus(true);
            tempAndHumiTabButton.SetStatus(false);
            OnShowRoomPanel();
        }
        public void HandleTempDashBoardState()
        {
            roomTabButton.SetStatus(false);
            tempAndHumiTabButton.SetStatus(true);
            OnShowTempPanel();
        }
        public void OnShowRoomPanel()
        {
            roomtempPanelAnim.SetBool("isRoom", true);
            roomtempPanelAnim.SetBool("isAddRoom", false);
            roomtempPanelAnim.SetBool("isTemp", false);
            if (isLoadedRooms)
            {
                return;
            }
            LoadAllRooms();
        }
        public void OnShowTempPanel()
        {
            roomtempPanelAnim.SetBool("isRoom", false);
            roomtempPanelAnim.SetBool("isAddRoom", false);
            roomtempPanelAnim.SetBool("isTemp", true);
            isLoadedRooms = false;
        }
        public void OnShowAddRoomPanel()
        {
            roomtempPanelAnim.SetBool("isRoom", false);
            roomtempPanelAnim.SetBool("isAddRoom", true);
            roomtempPanelAnim.SetBool("isTemp", false);
        }
        void LoadAllRooms()
        {
            Helper.DeletChildren(roomField);
            string allRoomsJson = ResponseFromGetRequest(G_URL.GetAllRooms);
            DATA.ALL_ROOMS = JsonConvert.DeserializeObject<R_AllRooms>(allRoomsJson);
            // foreach(R_DataRoom dataRoom in DATA.ALL_ROOMS.data)
            // {
            //     Instantiate(roomTemplates.Find(r => r.roomName == dataRoom.name).gameObject, roomField);
            // }
            DATA.ALL_ROOMS.data.ForEach(r => {
                Instantiate(roomTemplates.Find(rt => rt.roomName == r.name).gameObject, roomField);
            });
            isLoadedRooms = true;
        }
        public string ResponseFromGetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString.ToString();
        }
        public void UpdateState(HomeState newState)
        {
            if (state == newState)
            {
                return;
            }
            state = newState;
        }
        public void UpdateState(int newStateid)
        {
            if (state == (HomeState)newStateid)
            {
                return;
            }
            state = (HomeState)newStateid;
        }
        public void UpdateDashBoardState(DashBoardState newState)
        {
            if (dashBoardState == newState)
            {
                return;
            }
            dashBoardState = newState;
        }
        public void UpdateDashBoardState(int newStateid)
        {
            if (dashBoardState == (DashBoardState)newStateid)
            {
                return;
            }
            dashBoardState = (DashBoardState)newStateid;
        }
        public void HandleRoomChangedFromHome()
        {
            SceneManager.LoadScene("Room Scene");
        }
    }

    public enum HomeState
    {
        Home,
        Stat,
        Setting,
        AddRoom
    }

    public enum DashBoardState
    {
        Room,
        Temp
    }
