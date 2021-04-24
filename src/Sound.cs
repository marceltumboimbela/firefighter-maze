using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;  // DllImport()

//kelas sound digunakan untuk memutar background music (BGM)
namespace FireFighter
{
    class Sound
    {
           
      private string _command;
      private bool isOpen;
      
      [DllImport("winmm.dll")]
      private static extern long mciSendString(string strCommand,StringBuilder strReturn,int iReturnLength, IntPtr hwndCallback);
      
        //method ini digunakan untuk menghentikan suara
        public void Close()
 
      {
           _command = "close MediaFile";
           mciSendString(_command, null, 0, IntPtr.Zero);
           isOpen = false;
       }
      //method ini digunakan untuk membuka file mp3
      public void Open(string sFileName)
  
      {
           _command = "open \"" + sFileName + "\" type mpegvideo alias MediaFile";
           mciSendString(_command, null, 0, IntPtr.Zero);
           isOpen = true;
       }
      //method ini digunakan untuk memutar suara
      public void Play(bool loop)
  
      {
          if(isOpen)
           {
              _command = "play MediaFile";
              if (loop)
               _command += " REPEAT";
              mciSendString(_command, null, 0, IntPtr.Zero);
           }
        }
    }
}


