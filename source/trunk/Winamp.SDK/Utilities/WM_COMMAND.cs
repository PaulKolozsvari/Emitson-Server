namespace Winamp.SDK.Utilities
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion //Using Directives

    public class WM_COMMAND
    {
        #region Constants

        public const int WA_NOTHING = 0;
        public const int WA_MODE_1 = 1;
        public const int WINAMP_OPTIONS_PREFS = 40012; // pops up the preferences
        public const int WINAMP_OPTIONS_AOT = 40019; // toggles always on top
        public const int WINAMP_FILE_PLAY = 40029; // pops up the load file(s) box
        public const int WINAMP_OPTIONS_EQ = 40036; // toggles the EQ window
        public const int WINAMP_OPTIONS_PLEDIT = 40040; // toggles the playlist window
        public const int WINAMP_HELP_ABOUT = 40041; // pops up the about box
        public const int WA_PREVTRACK = 40044; // plays previous track
        public const int WA_PLAY = 40045; // plays selected track
        public const int WA_PAUSE = 40046; // pauses/unpauses currently playing track
        public const int WA_STOP = 40047; // stops currently playing track
        public const int WA_NEXTTRACK = 40048; // plays next track
        public const int WA_VOLUMEUP = 40058; // turns volume up
        public const int WA_VOLUMEDOWN = 40059; // turns volume down
        public const int WINAMP_FFWD5S = 40060; // fast forwards 5 seconds
        public const int WINAMP_REW5S = 40061; // rewinds 5 seconds
        public const int WINAMP_BUTTON1_SHIFT = 40144; // fast-rewind 5 seconds
        //public const int WINAMP_BUTTON2_SHIFT  = 40145;
        //public const int WINAMP_BUTTON3_SHIFT  = 40146;
        public const int WINAMP_BUTTON4_SHIFT = 40147; // stop after current track
        public const int WINAMP_BUTTON5_SHIFT = 40148; // fast-forward 5 seconds
        public const int WINAMP_BUTTON1_CTRL = 40154; // start of playlist
        public const int WINAMP_BUTTON2_CTRL = 40155; // open URL dialog
        //const int WINAMP_BUTTON3_CTRL   = 40156;
        public const int WINAMP_BUTTON4_CTRL = 40157; // fadeout and stop
        public const int WINAMP_BUTTON5_CTRL = 40158; // end of playlist
        public const int WINAMP_FILE_DIR = 40187; // pops up the load directory box
        public const int ID_MAIN_PLAY_AUDIOCD1 = 40323; // starts playing the audio CD in the first CD reader
        public const int ID_MAIN_PLAY_AUDIOCD2 = 40323; // plays the 2nd
        public const int ID_MAIN_PLAY_AUDIOCD3 = 40323; // plays the 3rd
        public const int ID_MAIN_PLAY_AUDIOCD4 = 40323; // plays the 4th

        #endregion //Constants
    }
}