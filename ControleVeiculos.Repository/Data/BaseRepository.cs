using System.Configuration;

namespace Lean.Test.Cloud.Repository.Data
{
    public abstract class BaseRepository
    {
        protected string ConnectionString
        {
#if DEBUG
            //get { return @"Server=TW-DBA-008\WISE_HML,48000; Database=SimuladorDetran;User ID=usrAppTecnobank;Password=G$wE25*mn;Connect Timeout=30;"; }
            //get { return @"Server=localhost\SQLEXPRESS,46000; Database=LeanTestManager;User ID=sa;Password=as;Connect Timeout=30;"; }
            get { return ConfigurationManager.ConnectionStrings["connLeanTest"].ConnectionString; }
#else
            get { return ConfigurationManager.ConnectionStrings["connLeanTest"].ConnectionString; }

#endif
        }
    }
}
