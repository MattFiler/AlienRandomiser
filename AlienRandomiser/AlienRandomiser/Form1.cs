﻿using OpenCAGE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlienRandomiser
{
    public partial class Form1 : Form
    {
        private Random _random = new Random();
        private List<MissionMapping> _missionMaps = new List<MissionMapping>();

        private Label[] _missionEndLabels = null;

        public Form1()
        {
            InitializeComponent();
            _missionEndLabels = new Label[] { order_1, order_2, order_3, order_4, order_5, order_6, order_7, order_8, order_9, order_10, order_11, order_12, order_13, order_14, order_15, order_16, order_17, order_18 };

            label1.Font = FontManager.GetFont(1, 27.75f);
            label4.Font = FontManager.GetFont(1, 27.75f);
            label6.Font = FontManager.GetFont(1, 27.75f);
            label8.Font = FontManager.GetFont(1, 27.75f);
            label10.Font = FontManager.GetFont(1, 27.75f);
            label12.Font = FontManager.GetFont(1, 27.75f);
            label14.Font = FontManager.GetFont(1, 27.75f);
            label16.Font = FontManager.GetFont(1, 27.75f);
            label18.Font = FontManager.GetFont(1, 27.75f);
            label20.Font = FontManager.GetFont(1, 27.75f);
            label22.Font = FontManager.GetFont(1, 27.75f);
            label24.Font = FontManager.GetFont(1, 27.75f);
            label26.Font = FontManager.GetFont(1, 27.75f);
            label28.Font = FontManager.GetFont(1, 27.75f);
            label30.Font = FontManager.GetFont(1, 27.75f);
            label32.Font = FontManager.GetFont(1, 27.75f);
            label34.Font = FontManager.GetFont(1, 27.75f);
            label36.Font = FontManager.GetFont(1, 27.75f);
            order_1.Font = FontManager.GetFont(1, 27.75f);
            order_2.Font = FontManager.GetFont(1, 27.75f);
            order_3.Font = FontManager.GetFont(1, 27.75f);
            order_4.Font = FontManager.GetFont(1, 27.75f);
            order_5.Font = FontManager.GetFont(1, 27.75f);
            order_6.Font = FontManager.GetFont(1, 27.75f);
            order_7.Font = FontManager.GetFont(1, 27.75f);
            order_8.Font = FontManager.GetFont(1, 27.75f);
            order_9.Font = FontManager.GetFont(1, 27.75f);
            order_10.Font = FontManager.GetFont(1, 27.75f);
            order_11.Font = FontManager.GetFont(1, 27.75f);
            order_12.Font = FontManager.GetFont(1, 27.75f);
            order_13.Font = FontManager.GetFont(1, 27.75f);
            order_14.Font = FontManager.GetFont(1, 27.75f);
            order_15.Font = FontManager.GetFont(1, 27.75f);
            order_16.Font = FontManager.GetFont(1, 27.75f);
            order_17.Font = FontManager.GetFont(1, 27.75f);
            order_18.Font = FontManager.GetFont(1, 27.75f);

            label2.Font = FontManager.GetFont(2, 24);

            randomiseOrder.Font = FontManager.GetFont(0, 20.25f);
            launchGame.Font = FontManager.GetFont(0, 20.25f);
        }

        private void randomiseOrder_Click(object sender, EventArgs e)
        {
            _missionMaps.Clear();
            List<int> usedMissions = new List<int>();
            int nextMission = 1;
            for (int i = 1; i < 18; i++)
            {
                int startMission = nextMission;
                while (nextMission == 1 || nextMission == startMission || usedMissions.Contains(nextMission) || ValidateInvalidMissions(startMission, nextMission))
                    nextMission = _random.Next(18) + 1;
                usedMissions.Add(nextMission);

                MissionMapping missionMapping = new MissionMapping();
                missionMapping.mission_start = startMission;
                missionMapping.mission_end = nextMission;
                _missionMaps.Add(missionMapping);
            }
            MissionMapping finalMapping = new MissionMapping();
            finalMapping.mission_start = nextMission;
            finalMapping.mission_end = 19;
            _missionMaps.Add(finalMapping);

            ReSyncUI();
        }

        private void launchGame_Click(object sender, EventArgs e)
        {
            CopyNewCommands();
            StartGame();
        }

        private void ReSyncUI()
        {
            foreach (MissionMapping mapping in _missionMaps)
                _missionEndLabels[mapping.mission_start - 1].Text = mapping.mission_end.ToString();
        }

        private bool ValidateInvalidMissions(int startMission, int endMission)
        { 
            return (startMission == 8 && endMission == 10) || (startMission == 2 && endMission == 11) ||
                   (startMission == 12 && endMission == 7) || (startMission == 17 && endMission == 2) ||
                   (startMission == 17 && endMission == 11) || (startMission == 4 && endMission == 3) ||
                   (startMission == 11 && endMission == 10);
        }

        private void CopyNewCommands()
        {

        }

        private void StartGame()
        {

        }

        private class MissionMapping
        {
            public int mission_start = 1;
            public int mission_end = 1;

            public string ToString()
            {
                return "Mission " + mission_start + " to " + mission_end;
            }
        }
    }
}
