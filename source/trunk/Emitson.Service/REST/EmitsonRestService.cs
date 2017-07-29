namespace Emitson.Service.REST
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using Winamp.SDK;
    using Figlut.Server.Toolkit.Utilities;
    using Figlut.Server.Toolkit.Utilities.Logging;
    using Emitson.Service.DTO;
    using System.Diagnostics;

    #endregion //Using Directives

    public class EmitsonRestService : EmitsonRestServiceUtilities, IEmitsonRestService
    {
        public Stream AllURIs()
        {
            try
            {
                return StreamHelper.GetStreamFromString(
                    string.IsNullOrEmpty(GOC.Instance.ApplicationName) ? "REST Web Service" : GOC.Instance.ApplicationName,
                    GOC.Instance.Encoding);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        #region Winamp Command Controls

        public Stream Stop()
        {
            try
            {
                WmCommandController.Stop();
                LogInfoMessage("Stop");
                return GetStreamFromObject(WmExtraController.GetCurrentSongTitle());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream Play()
        {
            try
            {
                WmCommandController.Play();
                LogInfoMessage("Play");
                return GetStreamFromObject(WmExtraController.GetCurrentSongTitle());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream Pause()
        {
            try
            {
                WmCommandController.Pause();
                LogInfoMessage("Pause");
                return GetStreamFromObject(WmExtraController.GetCurrentSongTitle());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream PreviousTrack()
        {
            try
            {
                WmCommandController.PreviousTrack();
                LogInfoMessage("PreviousTrack");
                return GetStreamFromObject(WmExtraController.GetCurrentSongTitle());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream NextTrack()
        {
            try
            {
                WmCommandController.NextTrack();
                LogInfoMessage("NextTrack");
                return GetStreamFromObject(WmExtraController.GetCurrentSongTitle());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream VolumeUp()
        {
            try
            {
                WmCommandController.VolumeUp();
                LogInfoMessage("VolumeUp");
                return GetStreamFromObject(WmExtraController.Volume);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex; 
            }
        }

        public Stream VolumeDown()
        {
            try
            {
                WmCommandController.VolumeDown();
                LogInfoMessage("VolumeDown");
                return GetStreamFromObject(WmExtraController.Volume);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream Volume(string percentage)
        {
            try
            {
                int percentageValue = 0;
                if (!int.TryParse(percentage, out percentageValue))
                {
                    throw new ArgumentException(string.Format("Volume percentage '{0}', not valid integer number.", percentage));
                }
                if(percentageValue > 100 || percentageValue < 0)
                {
                    throw new ArgumentException("Volume percentage must be between 0 and 100.");
                }
                int volumeValue = Convert.ToInt32((Convert.ToDouble(percentageValue) / 100) * 255);
                WmUserController.SetVolume(volumeValue); //Scale of 1 - 255
                LogInfoMessage(string.Format("Volume {0}", percentageValue.ToString()));
                return GetStreamFromObject(WmExtraController.Volume);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw;
            }
        }

        public Stream Forward5Sec()
        {
            try
            {
                WmCommandController.Forward5Sec();
                WmCommandController.Forward5Sec();
                LogInfoMessage("Forward5Sec");
                return GetStreamFromObject(WmUserController.GetTrackPosition());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream Rewind5Sec()
        {
            try
            {
                WmCommandController.Rewind5Sec();
                LogInfoMessage("Rewind5Sec");
                return GetStreamFromObject(WmUserController.GetTrackPosition());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream GetTrackPosition()
        {
            try
            {
                int result = WmUserController.GetTrackPosition();
                LogInfoMessage("GetTrackPosition");
                return GetStreamFromObject(result);
            }
            catch(Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream GetTrackLength()
        {
            try
            {
                int result = WmUserController.GetTrackLength();
                LogInfoMessage("GetTrackLength");
                return GetStreamFromObject(result);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream GetTrackCount()
        {
            try
            {
                int result = WmUserController.GetTrackCount();
                LogInfoMessage("GetTrackCount");
                return GetStreamFromObject(result);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw ex;
            }
        }

        public Stream GetPlaylistPosition()
        {
            try
            {
                int result = WmUserController.GetPlaylistPosition();
                LogInfoMessage("GetPlaylistPosition");
                return GetStreamFromObject(result);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw;
            }
        }

        public Stream SetPlaylistPosition(string position)
        {
            try
            {
                int p = -1;
                if (!int.TryParse(position, out p))
                {
                    throw new ArgumentException(string.Format("Invalid playlist position '{0}'", position));
                }
                WmUserController.SetPlaylistPosition(p);
                LogInfoMessage("SetPlaylistPosition");
                return GetStreamFromObject(WmUserController.GetPlaylistPosition());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw;
            }
        }

        public Stream GetStatus()
        {
            try
            {
                StatusDTO result = new StatusDTO();
                result.PlayBackStatus = WmUserController.GetPlaybackStatus();
                if (result.PlayBackStatus == 3) //Paused
                {
                    result.PlayBackStatus = 2; //Correct it to 2 (for consecutive number sequence consistency).
                }
                result.TrackPosition = WmUserController.GetTrackPosition();
                result.TrackLength = WmUserController.GetTrackLength();
                result.TrackCount = WmUserController.GetTrackCount();
                result.PlaylistPosition = WmUserController.GetPlaylistPosition();
                result.CurrentSongTitle = WmExtraController.GetCurrentSongTitle();
                LogInfoMessage("GetStatus");
                return GetStreamFromObject(result);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                UpdateHttpStatusOnException(ex);
                throw;
            }
        }

        #endregion //Winamp Command Controls
    }
}
