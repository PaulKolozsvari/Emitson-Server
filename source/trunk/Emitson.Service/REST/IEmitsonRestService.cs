namespace Emitson.Service.REST
{
    #region Using Directives

    using Figlut.Server.Toolkit.Web.Service.REST;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;
    using System.Threading.Tasks;

    #endregion //Using Directives

    [ServiceContract]
    public interface IEmitsonRestService
    {
        [OperationContract]
        [WebGet(UriTemplate = "*")]
        Stream AllURIs();

        #region Winamp Command Controls

        [OperationContract]
        [WebGet(UriTemplate = "/Stop")]
        Stream Stop();

        [OperationContract]
        [WebGet(UriTemplate = "/Play")]
        Stream Play();

        [OperationContract]
        [WebGet(UriTemplate = "/Pause")]
        Stream Pause();

        [OperationContract]
        [WebGet(UriTemplate = "/PreviousTrack")]
        Stream PreviousTrack();

        [OperationContract]
        [WebGet(UriTemplate = "/NextTrack")]
        Stream NextTrack();

        [OperationContract]
        [WebGet(UriTemplate = "/VolumeUp")]
        Stream VolumeUp();

        [OperationContract]
        [WebGet(UriTemplate = "/VolumeDown")]
        Stream VolumeDown();

        [OperationContract]
        [WebGet(UriTemplate = "/Volume/{percentage}")]
        Stream Volume(string percentage);

        [OperationContract]
        [WebGet(UriTemplate = "/Forward5Sec")]
        Stream Forward5Sec();

        [OperationContract]
        [WebGet(UriTemplate = "/Rewind5Sec")]
        Stream Rewind5Sec();

        [OperationContract]
        [WebGet(UriTemplate = "/GetTrackPosition")]
        Stream GetTrackPosition();

        [OperationContract]
        [WebGet(UriTemplate = "/GetTrackLength")]
        Stream GetTrackLength();

        [OperationContract]
        [WebGet(UriTemplate = "/GetTrackCount")]
        Stream GetTrackCount();

        [OperationContract]
        [WebGet(UriTemplate = "/GetPlaylistPosition")]
        Stream GetPlaylistPosition();

        [OperationContract]
        [WebGet(UriTemplate = "/SetPlaylistPosition/{position}")]
        Stream SetPlaylistPosition(string position);

        [OperationContract]
        [WebGet(UriTemplate = "/GetStatus")]
        Stream GetStatus();

        #endregion //Winamp Command Controls

        #region Winamp User Controls

        //[OperationContract]
        //[WebGet(UriTemplate = "/Stop")]
        //Stream Stop();

        #endregion //Winamp User Controls
    }
}