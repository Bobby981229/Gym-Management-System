/*
 * Name: Liu Shangyuan
 * 
 * SCN: 197076658
 * 
 * School: BUAA
 * 
 * Version: Gym Management System - Final
 * 
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Gym_Management_System
{
    public partial class MutiMedia_Form : Form
    {
        string address; // Local music, video storage address
        string Website; // Video playback website resolution address
        private VlcPlayer vlcPlayer;
        // Determine whether to play
        private bool is_playinig;
        //Mark whether the media file is open. If it is not open, tsbtn_play reads the file before opening it.
        private bool media_is_open;

        public MutiMedia_Form()
        {
            InitializeComponent();           
            address = Environment.CurrentDirectory;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            lblMediaName.Hide();
            picLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblRollTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // Binary Streaming Media
            if (!File.Exists(address + "\\Menu.ini"))
            {
                FileStream fs = new FileStream(address + "\\Menu.ini", FileMode.Create, FileAccess.Write); 
                fs.Close();
            }

            string pluginPath = System.Environment.CurrentDirectory + "\\plugins\\";
            vlcPlayer = new VlcPlayer(pluginPath);
            IntPtr render_wnd = this.panel1.Handle;
            vlcPlayer.SetRenderWindow((int)render_wnd);
            
            // Set the media begin time
            tbVideoTime.Text = "00:00:00/00:00:00";
            is_playinig = false;
            media_is_open = false;
            trackBarVolume.Value = 50;
            this.Size = new Size(800, 600); 
        }

        /// <summary>
        /// Add history from Menu.ini to the file menu bar
        /// </summary>
        private void readFilePath()
        {
            // Add file name to list
            int items_count = this.ToolStripFile.DropDownItems.Count;
            // Remove excess file names
            switch (items_count)
            {
                case 4:
                    this.ToolStripFile.DropDownItems.RemoveAt(1);
                    break;
                case 5:
                    // After removing the first item, the second item became the first item again, so continue to remove the first item
                    this.ToolStripFile.DropDownItems.RemoveAt(1);
                    this.ToolStripFile.DropDownItems.RemoveAt(1);
                    break;
                case 6:
                    this.ToolStripFile.DropDownItems.RemoveAt(1); // Remove excess file names
                    this.ToolStripFile.DropDownItems.RemoveAt(1);
                    this.ToolStripFile.DropDownItems.RemoveAt(1);
                    break;
                default:
                    break;
            }

            // Read binary stream and convert to audio or video file
            StreamReader sr = new StreamReader(address + "\\Menu.ini", true);

            int i =1;
            // Peek is used to determine whether the file you read is finished.If it is finished, it will return int type - 1
            while (sr.Peek() > -1) 
            {
                    ToolStripMenuItem menuitem = new ToolStripMenuItem(sr.ReadLine());
                    this.ToolStripFile.DropDownItems.Insert(i, menuitem);
                    i++;
                    menuitem.Click += new EventHandler(menuitem_Click); 
            }
            sr.Close();
        }

        // Click the saved audio and video name to play
        private void menuitem_Click(object sender, EventArgs e)
        {
            try
            {
                // Initial configuration
                ToolStripMenuItem menu = (ToolStripMenuItem)sender;
                vlcPlayer.PlayFile(menu.Text);
                trackBarSpace.SetRange(0, (int)vlcPlayer.Duration());
                trackBarSpace.Value = 0;
                timer1.Start();
                is_playinig = true;
                tSBtn_play.Image = Properties.Resources.Pause;
                tSBtn_play.Text = "Pause";
                media_is_open = true;
                // Show the current playing music or video name
                lblMediaName.Text = Path.GetFileNameWithoutExtension(menu.Text);
                lblMediaName.Show();
            }
            catch
            {
                MessageBox.Show("The file does not exist !", "Warning !");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (is_playinig)
            {

                if (trackBarSpace.Value == trackBarSpace.Maximum)
                {
                    vlcPlayer.Stop();
                    timer1.Stop();
                    lblMediaName.Hide();
                }
                else
                {
                    trackBarSpace.Value = trackBarSpace.Value + 1;
                    tbVideoTime.Text = string.Format("{0}/{1}",GetTimeString(trackBarSpace.Value), GetTimeString(trackBarSpace.Maximum));
                }
            }
        }

        // Record audio and video length
        private string GetTimeString(int val)
        {
            int hour = val / 3600;
            val %= 3600;
            int minute = val / 60;
            int second = val % 60;
            return string.Format("{0:00}:{1:00}:{2:00}", hour, minute, second);
        }

        // Adjust audio and video playback progress
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (is_playinig)
            {
                // Adjust audio and video playback progress
                vlcPlayer.SetPlayTime(trackBarSpace.Value);
                // Feedback progress
                trackBarSpace.Value = (int)vlcPlayer.GetPlayTime();
            }
        }

        /// <summary>
        /// The method of playing video
        /// </summary>
        private void playVideo()
        {
            // When the video is playing, pause th music or video
            if (is_playinig)
            {
                vlcPlayer.Pause(); // Pause to play
                timer1.Stop();  // Timer is pause
                is_playinig = false;
                tSBtn_play.Image = Properties.Resources.Play;
                tSBtn_play.Text = "Play";
            }
            else
            {
                try
                {
                    // Add the new file name to the list
                    string[] text = File.ReadAllLines(address + "\\Menu.ini");
                    string openfilepath = "";  // Set the file path
                    int row = text.Length;
                    // Read the file as a binary stream
                    StreamReader sr1 = new StreamReader(address + "\\Menu.ini", true);
                    switch (row)
                    {
                        case 1:
                            openfilepath = sr1.ReadLine();
                            break;  
                        case 2:
                            sr1.ReadLine();
                            openfilepath = sr1.ReadLine();
                            break;
                        case 3:
                            sr1.ReadLine();
                            sr1.ReadLine();
                            openfilepath = sr1.ReadLine();
                            break;
                        default:
                            break;
                    }
                    if (row == 1 || row == 2 || row == 3)
                    {

                        if (!media_is_open)
                        {
                            vlcPlayer.PlayFile(openfilepath);
                        }
                        // Initial configuration
                        trackBarSpace.SetRange(0, (int)vlcPlayer.Duration());
                        vlcPlayer.SetPlayTime(trackBarSpace.Value);
                        vlcPlayer.Play();
                        trackBarSpace.Value = (int)vlcPlayer.GetPlayTime();
                        // trackBar1.Value = 0;
                        timer1.Start();
                        is_playinig = true;
                        tSBtn_play.Image = Properties.Resources.Pause;
                        tSBtn_play.Text = "Pause";
                        media_is_open = true;
                        // Show the current playing music or video name
                        lblMediaName.Text = Path.GetFileNameWithoutExtension(openfilepath);
                        lblMediaName.Show();
                    }
                    sr1.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("File does not exist ! ", "Prompt");
                }
            }
        }

        private void tSBtn_play_Click(object sender, EventArgs e)
        {
            playVideo();
        }

        // Audio and video stop to play
        private void tSBtn_stop_Click(object sender, EventArgs e)
        {
            vlcPlayer.Stop();  // Stop play the video or music
            timer1.Stop();   // Stop timer
            is_playinig = false;
            media_is_open = false;
            tSBtn_play.Image = Properties.Resources.Play;
            tSBtn_play.Text = "Play";

            // Video time return 0
            tbVideoTime.Text = "00:00:00/00:00:00";
            trackBarSpace.Value = 0;
            lblMediaName.Hide();
        }

        // Open the folder
        private void tSBtn_openfile_Click(object sender, EventArgs e)
        {
            ReadFile();
        }

        // Adjust audio and video volume
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            // Show the current volume
            lblVolume.Text = "Volume: " + trackBarVolume.Value.ToString();
            // Volume feedback adjustment
            vlcPlayer.SetVolume(trackBarVolume.Value);
        }

        // Fast forward audio and video
        private void tSB_forward_Click(object sender, EventArgs e)
        {
            vlcPlayer.Pause();
            // Fast forward audio and video by 15 seconds
            int time = (int)vlcPlayer.GetPlayTime() + 5;
            if (time < trackBarSpace.Maximum)
            {
                vlcPlayer.SetPlayTime(time);
            }
            else
            {
                vlcPlayer.SetPlayTime(trackBarSpace.Maximum); // Set the maximum play time 
            }
            vlcPlayer.Play();
            trackBarSpace.Value = (int)vlcPlayer.GetPlayTime();
        }

        // Rewind audio and video 
        private void tSB_backward_Click(object sender, EventArgs e)
        {
            vlcPlayer.Pause();
            // Rewind audio and video for 5 seconds
            int time = (int)vlcPlayer.GetPlayTime() - 5;
            if (time > 0)
            {
                vlcPlayer.SetPlayTime(time);
            }
            else
            {
                vlcPlayer.SetPlayTime(0);
            }
            vlcPlayer.Play();
            trackBarSpace.Value = (int)vlcPlayer.GetPlayTime();
        }

        // Play the music or video by double click
        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            playVideo();
        }

        // Close the Muti-Media play page
        private void picLogo_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void rangeTrackBarControl1_MouseMove(object sender, MouseEventArgs e)
        {
            vlcPlayer.SetVolume(trackBarVolume.Value);
        }

        // Openthe tool strip file and show the items
        private void ToolStripFile_Click(object sender, EventArgs e)
        {
            readFilePath();
        }

        /// <summary>
        /// The Class to open the file
        /// </summary>
        private void ReadFile()
        {
            openFileDialog1.FileName = "";
            // Confirm to select this file
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Binary stream rewrite of the file
                StreamWriter s = new StreamWriter(address + "\\Menu.ini", true);
                s.WriteLine(openFileDialog1.FileName);
                s.Flush();  // Clear cache
                s.Close();  // Close the basic streams and objects

                string[] text = File.ReadAllLines(address + "\\Menu.ini");
                int row = text.Length;  
                int rowcount;
                string[] tempdata = new string[] { "", "", "" };

                /// If the number of record lines exceeds 3, record the last three data first, 
                /// then re-create a Menu.ini (clear data),
                /// and then write the last three data records
                if (row > 3)
                {
                    StreamReader sr1 = new StreamReader(address + "\\Menu.ini", true);
                    while (sr1.Peek() > -1)
                    {
                        // Empty read, skip the original first data, and start reading from the second data
                        sr1.ReadLine();
                        for (rowcount = 0; rowcount < 3; rowcount++)
                        {
                            tempdata[rowcount] = sr1.ReadLine();
                        }
                    }
                    sr1.Close();
                    // Binary Streaming Media
                    FileStream fs = new FileStream(address + "\\Menu.ini", FileMode.Create, FileAccess.Write);  
                    fs.Close();
                    // Binary stream rewrite of the file
                    StreamWriter s1 = new StreamWriter(address + "\\Menu.ini", true);
                    s1.WriteLine(tempdata[0]);
                    s1.WriteLine(tempdata[1]);
                    s1.WriteLine(tempdata[2]);
                    s1.Flush();
                    s1.Close();
                }

                vlcPlayer.PlayFile(openFileDialog1.FileName);
                trackBarSpace.SetRange(0, (int)vlcPlayer.Duration());
                trackBarSpace.Value = 0;
                timer1.Start();
                is_playinig = true;
                tSBtn_play.Image = Properties.Resources.Pause;
                tSBtn_play.Text = "Pause";
                media_is_open = true;

                //  Another way to get the file name without suffix
                lblMediaName.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                lblMediaName.Show();
                lblRollTitle.Visible = false;
            }
        }

        /// <summary>
        /// Open the file
        /// </summary>
        private void ToolStripOpen_Click(object sender, EventArgs e)
        {
            ReadFile();  // Open file and read audio and video
        }

        private void ToolStripExit_Click_1(object sender, EventArgs e)
        {
            this.Hide();  // Close the Muti-Media Form
        }

        /// <summary>
        /// Open the file
        /// </summary>
        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            ReadFile();  // Open file and read audio and video
        }

        // Video website resolution address 1
        private void radioBut1_CheckedChanged(object sender, EventArgs e)
        {
            Website = "http://jx.aeidu.cn/index.php?url=";
        }

        // Video website resolution address 2
        private void radioBut2_CheckedChanged(object sender, EventArgs e)
        {
            Website = "https://jx.ab33.top/vip/?url=";
        }

        // Open system default browser and play the movie
        private void btnPaly_Click(object sender, EventArgs e)
        {
            if(txtWebsite.Text == "")
            {
                MessageBox.Show("Please enter the URL of video !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(radioBut1.Checked == false && radioBut2.Checked == false)
            {
                MessageBox.Show("Please select a resolution address !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Open system default browser
                System.Diagnostics.Process.Start(Website + txtWebsite.Text.ToString()); 
            }
        }

        // Press the Space to play or pasue muaiscs and videos
        private void MutiMedia_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                tSBtn_play_Click(sender, e);
            }
        }
    }
}
