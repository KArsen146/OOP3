using System;
using System.Media;
using NAudio;
using NAudio.Wave;

class sound
{
    public static void Main()
    {
      

        //waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
        var a = new WaveFileReader("/Users/arsenijkarpov/Downloads/Звуки_Сирена_Mobilaz.Net.wav");
        var b = new WaveOut();
        b.Init(a);
        b.Play();

    }

    //void waveInStream_DataAvailable(object sender, WaveInEventArgs e)
    //{
    //    wavefile.WriteData(e.Buffer, 0, e.BytesRecorded);
    //}

}