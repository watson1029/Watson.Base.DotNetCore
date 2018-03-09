using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Watson.Base.DotNetCore.DataBase
{
    internal class SysLog
    {
        private Guid _logid;
        private string _logmsg;
        private string _logdetail;
        private DateTime? _logtime;
        [Key]
        internal Guid LogID
        {
            get { return _logid; }
            set { _logid = value; }
        }
        internal string LogMsg
        {
            get { return _logmsg; }
            set { _logmsg = value; }
        }
        internal string LogDetail
        {
            get { return _logdetail; }
            set { _logdetail = value; }
        }
        internal DateTime? LogTime
        {
            get { return _logtime; }
            set { _logtime = value; }
        }
    }
}
