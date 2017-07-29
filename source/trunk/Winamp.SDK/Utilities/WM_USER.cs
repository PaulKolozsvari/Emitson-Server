namespace Winamp.SDK.Utilities
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion //Using Directives

    public class WM_USER
    {
        #region Constants

        public const int IPC_ISPLAYING = 104;		 // Returns status of playback. Returns: 1 = playing, 3 = paused, 0 = not playing)
        public const int IPC_GETVERSION = 0;	     // Returns Winamp version (0x20yx for winamp 2.yx,  Versions previous to Winamp 2.0
        // typically (but not always) use 0x1zyx for 1.zx versions
        public const int IPC_DELETE = 101;		 // Clears Winamp internal playlist;
        public const int IPC_GETOUTPUTTIME = 105;		 // Returns the position in milliseconds of the 
        // current song (mode = 0), or the song length, in seconds (mode = 1). It 
        // returns: -1 if not playing or if there is an error.
        public const int IPC_JUMPTOTIME = 106;		 // Sets the position in milliseconds of the current song (approximately). It
        // returns -1 if not playing, 1 on eof, or 0 if successful. It requires Winamp v1.60+
        public const int IPC_WRITEPLAYLIST = 120;		 // Writes the current playlist to <winampdir>\\Winamp.m3u, and returns the current 
        // playlist position. It requires Winamp v1.666+
        public const int IPC_SETPLAYLISTPOS = 121;		 // Sets the playlist position
        public const int IPC_SETVOLUME = 122;		 // Sets the volume of Winamp (from 0-255)
        public const int IPC_SETPANNING = 123;		 // Sets the panning of Winamp (from 0 (left) to 255 (right))
        public const int IPC_GETLISTLENGTH = 124;		 // Returns the length of the current playlist in tracks
        public const int IPC_GETLISTPOS = 125;      // Returns the playlist position. A lot like IPC_WRITEPLAYLIST only faster since it 
        // doesn't have to write out the list. It requires Winamp v2.05+
        public const int IPC_GETINFO = 126;		 // Returns info about the current playing song (about Kb rate). The value it returns 
        // depends on the value of 'mode'. If mode == 0 then it returns the Samplerate (i.e. 44100), 
        // if mode == 1 then it returns the Bitrate  (i.e. 128), if mode == 2 then it returns the 
        // channels (i.e. 2)

        public const int IPC_GETEQDATA = 127;      // Queries the status of the EQ. The value it returns depends on what 'position' is set to. It
        // requires Winamp v2.05+
        // Value      Meaning
        // ------------------
        // 0-9        The 10 bands of EQ data. 0-63 (+20db - -20db)
        // 10         The preamp value. 0-63 (+20db - -20db)
        // 11         Enabled. zero if disabled, nonzero if enabled.
        // 12         Autoload. zero if disabled, nonzero if enabled.


        public const int IPC_SETEQDATA = 128;		 // Sets the value of the last position retrieved by IPC_GETEQDATA (integer eqPosition). It
        // requires Winamp v2.05+

        #endregion //Constants
    }
}
