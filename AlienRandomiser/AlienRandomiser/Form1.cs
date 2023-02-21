﻿using OpenCAGE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SharpCompress;
using SharpCompress.Archives;
using System.Linq;
using System.Threading.Tasks;

namespace AlienRandomiser
{
    public partial class Form1 : Form
    {
        private Random _random = new Random();
        private List<MissionMapping> _missionMaps = new List<MissionMapping>();

        private Label[] _missionEndLabels = null;

        private string _pathToAI = @"G:\SteamLibrary\steamapps\common\Alien Isolation";
        private string _pathToCommands = "Alien Randomizer";

        public Form1()
        {
            InitializeComponent();

            _missionEndLabels = new Label[] { order_1, order_2, order_3, order_4, order_5, order_6, order_7, order_8, order_9, order_10, order_11, order_12, order_13, order_14, order_15, order_16, order_17, order_18 };

#if !DEBUG
            _pathToAI = Application.StartupPath;
            if (!Directory.Exists(_pathToCommands) || !Directory.Exists("DATA"))
            {
                MessageBox.Show("Please open the mission randomiser in your Alien: Isolation directory.", "Incorrectly launched!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Environment.Exit(0);
                return;
            }
#endif

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

            randomiseOrder.Font = FontManager.GetFont(0, 18f);
            launchGame.Font = FontManager.GetFont(0, 20.25f);
            hideOrder.Font = FontManager.GetFont(0, 15.75f);
            multiAlien.Font = FontManager.GetFont(0, 15.75f);

            difficultySelect.SelectedIndex = 0;

            GenerateNewDefaultOrder();
        }

        private void randomiseOrder_Click(object sender, EventArgs e)
        {
            GenerateNewRandomOrder();
            ReSyncUI();
        }

        private void launchGame_Click(object sender, EventArgs e)
        {
            //try
            //{
                CopyNewCommands();
                StartGame();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //    MessageBox.Show("Failed to set & start game!\nIs Alien: Isolation already open?", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void hideOrder_CheckedChanged(object sender, EventArgs e)
        {
            ReSyncUI();
        }

        private void GenerateNewDefaultOrder()
        {
            for (int i = 1; i < 19; i++)
            {
                MissionMapping missionMapping = new MissionMapping();
                missionMapping.mission_start = i;
                missionMapping.mission_end = i + 1;
                _missionMaps.Add(missionMapping);
            }
        }

        private void GenerateNewRandomOrder()
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
        }

        private void ReSyncUI()
        {
            foreach (MissionMapping mapping in _missionMaps)
                _missionEndLabels[mapping.mission_start - 1].Text = (hideOrder.Checked) ? "??" : mapping.mission_end.ToString();
        }

        private bool ValidateInvalidMissions(int startMission, int endMission)
        { 
            return (startMission == 8 && endMission == 10) || (startMission == 2 && endMission == 11) ||
                   (startMission == 12 && endMission == 7) || (startMission == 17 && endMission == 2) ||
                   (startMission == 17 && endMission == 11) || (startMission == 4 && endMission == 3) ||
                   (startMission == 11 && endMission == 10) || (startMission == 16 && endMission == 4);
        }

        private void CopyNewCommands()
        {
            if (_missionMaps.Count == 0) return;

            byte[] content = File.ReadAllBytes("content.bin");
            CommandsContent[] toWrite = new CommandsContent[100];
            Parallel.For(0, _missionMaps.Count, i =>
            {
                IArchive archive = ArchiveFactory.Open(new MemoryStream(content));
                foreach (IArchiveEntry commandsPAK in archive.Entries.Where(o => o.Key.Contains("COMMANDS.PAK")))
                {
                    string[] filePathSplit = commandsPAK.Key.Substring(_pathToCommands.Length + 1).Split('/');

                    if (filePathSplit[0].Substring(0, 1) == "_")
                    {
                        string level = filePathSplit[0].Substring(1, filePathSplit[0].Length - 1 - (" GLOBAL").Length);
                        toWrite[i] = new CommandsContent() { file = commandsPAK, path = level };
                    }
                    else
                    {
                        if (filePathSplit[1] != _missionMaps[i].ToString()) continue;

                        if (filePathSplit[2] != "COMMANDS.PAK")
                        {
                            string pairedLevelCombo = filePathSplit[2].Split('&')[1].Substring(1);
                            bool didFindMatchingPair = false;
                            foreach (MissionMapping pairedMapping in _missionMaps)
                            {
                                if (pairedMapping.ToStringShortened() == pairedLevelCombo)
                                {
                                    didFindMatchingPair = true;
                                    pairedMapping.did_find_pak_paired = true;
                                    break;
                                }
                            }
                            if (!didFindMatchingPair) continue;
                        }

                        string[] levelInfoSplit = filePathSplit[0].Split('(')[1].Split(')');
                        string level = levelInfoSplit[0];
                        if (levelInfoSplit.Length > 1 && levelInfoSplit[1] != "")
                        {
                            int difficulty = Convert.ToInt32(levelInfoSplit[1].Substring(1, 1));
                            if (difficulty != difficultySelect.SelectedIndex + 1) continue;
                        }
                        _missionMaps[i].did_find_pak = true;
                        toWrite[i] = new CommandsContent() { file = commandsPAK, path = level };
                    }
                }
            });
            for (int i = 0; i < toWrite.Length; i++)
            {
                for (int x = i + 1; x < toWrite.Length; x++)
                {
                    if (toWrite[x] != null && toWrite[i] != null &&
                        toWrite[x].path == toWrite[i].path)
                    {
                        toWrite[i] = null;
                    }
                }
            }
            Parallel.ForEach(toWrite, write =>
            {
                CopyCommandsToLevel(write.path, write.file);
            });

#if DEBUG
            foreach (MissionMapping mapping in _missionMaps)
            {
                if (!mapping.did_find_pak && !mapping.did_find_pak_paired)
                    Console.WriteLine("Failed to find PAK for mission " + mapping.mission_start + " -> " + mapping.mission_end + "!!");
            }
#endif
        }
        private void CopyCommandsToLevel(string levelName, IArchiveEntry file)
        {
            string pathToLevelPak = _pathToAI + @"\DATA\ENV\PRODUCTION\" + levelName + @"\WORLD\COMMANDS.PAK";
            Console.WriteLine(pathToLevelPak);
            File.Delete(pathToLevelPak);
            file.WriteToFile(pathToLevelPak);
        }

        private void StartGame()
        {
            ProcessStartInfo alienProcess = new ProcessStartInfo();
            alienProcess.WorkingDirectory = _pathToAI;
            alienProcess.FileName = _pathToAI + "/AI.exe";
            Process.Start(alienProcess);
        }

        private class MissionMapping
        {
            public int mission_start = 1;
            public int mission_end = 1;

            public bool did_find_pak = false;
            public bool did_find_pak_paired = false;

            public string ToString()
            {
                return "Mission " + mission_start + " to " + mission_end;
            }
            public string ToStringShortened()
            {
                return "M" + mission_start + " to M" + mission_end;
            }
        }

        private class CommandsContent
        {
            public string path;
            public IArchiveEntry file;
        }
    }
}
