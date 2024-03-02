using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NAudio.Wave;
using System.Windows.Forms;

namespace MerryDllFramework
{
    public static class plays
    {
        private static SoundPlayer player;

        private static WaveInEvent mWavIn;

        private static WaveFileWriter mWavWriter;

        //private bool _flag = true;
        private static string path;



        public static WaveInEvent MWavIn { get => mWavIn; set => mWavIn = value; }

        static plays()
        {
            plays.player = new SoundPlayer();
        }

        private static void MWavIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            plays.mWavWriter.Write(e.Buffer, 0, e.BytesRecorded);
            int secondsRecorded = (int)plays.mWavWriter.Length / plays.mWavWriter.WaveFormat.AverageBytesPerSecond;
        }

        public static void RecordTest()
        {
            path = Application.StartupPath;

            plays.StartRecord(".\\Music\\rec.wav");
            MessageBox.Show("录音");
            plays.StopRecord();
            plays.player.SoundLocation = ".\\Music\\rec.wav";
            plays.player.Load();
            plays.player.PlayLooping();
            MessageBox.Show("播放录音");
            plays.player.Stop();
            plays.player.Dispose();
        }

        public static void StartMusic(string music)
        {
            plays.player.SoundLocation = music;
            plays.player.Load();
            plays.player.PlayLooping();
        }

        public static void StartRecord(string filePath)
        {
            plays.mWavIn = new WaveInEvent();
            plays.mWavIn.DataAvailable += new EventHandler<WaveInEventArgs>((object sender, WaveInEventArgs e) =>
            {
                plays.mWavWriter.Write(e.Buffer, 0, e.BytesRecorded);
                int secondsRecorded = (int)plays.mWavWriter.Length / plays.mWavWriter.WaveFormat.AverageBytesPerSecond;
            });
            plays.mWavWriter = new WaveFileWriter(filePath, plays.mWavIn.WaveFormat);
            plays.mWavIn.StartRecording();
        }

        public static void StopMusic()
        {
            plays.player.Stop();
            plays.player.Dispose();
        }

        public static void StopRecord()
        {
            WaveInEvent waveIn = plays.mWavIn;
            if (waveIn != null)
            {
                waveIn.StopRecording();
            }
            else
            {
            }
            WaveInEvent waveIn1 = plays.mWavIn;
            if (waveIn1 != null)
            {
                waveIn1.Dispose();
            }
            else
            {
            }
            plays.mWavIn = null;
            WaveFileWriter waveFileWriter = plays.mWavWriter;
            if (waveFileWriter != null)
            {
                waveFileWriter.Close();
            }
            else
            {
            }
            plays.mWavWriter = null;
        }

        public static string MusicPlay()
        {
            path = Application.StartupPath;
            //int timemusicdelay = Convert.ToInt32(data.playMusicDelay);
            //string a = string.Concat(path, "\\mic.wav");
            plays.StartMusic(string.Concat(path, "\\mic.wav"));
            //plays.StartMusic("E:\\SW\\Chung\\668_editing\\HDT668_DLL\\MerryDllFramework_Debug\\bin\\Debug\\mic.wav");

            Messages mes = new Messages("Choi Nhac", 3000);
            mes.StartPosition = FormStartPosition.CenterParent;
            if (!(mes.ShowDialog() == DialogResult.Yes))
            {
                plays.StopMusic();
                return false.ToString();
            }
            else
            {
                plays.StopMusic();
                return true.ToString();
            }
        }

        public static string MusicPlayLR()
        {
            path = Application.StartupPath;
            plays.StartMusic(string.Concat(path, "\\Music\\MusicalTrackLeftRight.wav"));
 
            Messages mes = new Messages("Play Musical Track Left Right", 3000);
            mes.StartPosition = FormStartPosition.CenterParent;
            if (!(mes.ShowDialog() == DialogResult.Yes))
            {
                plays.StopMusic();
                return false.ToString();
            }
            else
            {
                plays.StopMusic();
                return true.ToString();
            }
        }

        public static string MusicPlay503k()
        {
            path = Application.StartupPath;
            plays.StartMusic(string.Concat(path, "\\Music\\50~3k.wav"));

            Messages mes = new Messages("Play Music 50~3k", 3000);
            mes.StartPosition = FormStartPosition.CenterParent;
            if (!(mes.ShowDialog() == DialogResult.Yes))
            {
                plays.StopMusic();
                return false.ToString();
            }
            else
            {
                plays.StopMusic();
                return true.ToString();
            }
        }

        public static string RecordPlayTest()
        {
            path = Application.StartupPath;
            plays.StartRecord(string.Concat(path, "\\Music\\rec.wav"));
            Messages mes1 = new Messages("Start Record", 500);
            mes1.StartPosition = FormStartPosition.CenterParent;
            if (!(mes1.ShowDialog() == DialogResult.Yes))
            {
                plays.StopRecord();
                return false.ToString();
            }
            else
            {
                plays.StopRecord();
                plays.StartMusic(string.Concat(path, "\\Music\\rec.wav"));
                Messages mes = new Messages("Play Record Test", 2000);
                mes.StartPosition = FormStartPosition.CenterParent;
                if (!(mes.ShowDialog() == DialogResult.Yes))
                {
                    plays.StopMusic();
                    return false.ToString();
                }
                else
                {
                    plays.StopMusic();
                    return true.ToString();
                }
            }
            
            
        }


    }
}
