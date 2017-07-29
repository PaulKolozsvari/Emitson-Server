namespace Emitson.Service.DTO
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion //Using Directives

    public class StatusDTO
    {
        #region Properties

        /// <summary>
        /// 0.	Stop
        /// 1.	Play
        /// 3.	Paused
        /// </summary>
        public int PlayBackStatus { get; set; }

        /// <summary>
        /// Current track position in milliseconds. -1 if Winamp is currently not playing a song (or if it's paused).
        /// </summary>
        public int TrackPosition { get; set; }

        /// <summary>
        /// Current track's length in seconds.
        /// </summary>
        public int TrackLength { get; set; }

        /// <summary>
        /// The total number of tracks in the playlist.
        /// </summary>
        public int TrackCount { get; set; }

        /// <summary>
        /// The zero based index of the current playing track in the playlist.
        /// </summary>
        public int PlaylistPosition { get; set; }

        /// <summary>
        /// The title of the current song playing. "Stopped" or "Paused" if not currently playing.
        /// </summary>
        public string CurrentSongTitle { get; set; }

        #endregion //Properties
    }
}