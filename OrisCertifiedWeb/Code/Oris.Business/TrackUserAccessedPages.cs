#region SYSTEM
using System;
using System.Collections.Generic;
using System.Text;
#endregion

#region Others
using Oris.DataAccess;
#endregion
namespace Oris.Business
{
    public abstract class TrackUserAccessedPages
    {
        #region Variables
            private int _TrackUserAccessedPagesId, _TaskeroUserId;
            private string _PageName, _WebPageURL, _AccessedFromIP, _BrowserInfo, _OS;
            private DateTime _AccessedDateTime;
            private bool _IsTrackUserAccessedPages;
        #endregion

        #region Properties
            public int TrackUserAccessedPagesId
            {
                get { return _TrackUserAccessedPagesId; }
                set { _TrackUserAccessedPagesId = value; }
            }
            public int TaskeroUserId
            {
                get { return _TaskeroUserId; }
                set { _TaskeroUserId = value; }
            }
            public string PageName
            {
                get { return _PageName; }
                set { _PageName = value; }
            }
            public string WebPageURL
            {
                get { return _WebPageURL; }
                set { _WebPageURL = value; }
            }
	        public DateTime AccessedDateTime
            {
                get { return _AccessedDateTime; }
                set { _AccessedDateTime = value; }
            }
            public string AccessedFromIP
            {
                get { return _AccessedFromIP; }
                set { _AccessedFromIP = value; }
            }
            public string BrowserInfo
            {
                get { return _BrowserInfo; }
                set { _BrowserInfo = value; }
            }
            public string OS
            {
                get { return _OS; }
                set { _OS = value; }
            }
            public bool IsTrackUserAccessedPages
            {
                get
                {
                    try { return _IsTrackUserAccessedPages; }
                    catch{return false;}
                }
                set 
                {
                    try { _IsTrackUserAccessedPages = value; }
                    catch { _IsTrackUserAccessedPages = false; }
                }
            }
        #endregion
    }
}
