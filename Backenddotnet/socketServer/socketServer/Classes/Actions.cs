using Microsoft.Extensions.Configuration;
using socketServer.Interface;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace socketServer.Classes
{
    public class Actions : IActions
    {
        private IConfiguration _config;
        public Actions(IConfiguration config)
        {
            _config = config;
        }
        public Task LogErrorAsync(Exception exception, string business, string ip = null)
        {
            //return Task.CompletedTask;
            string methdeName = "";
            string moduleName = "";
            try
            {
                var st2 = new StackTrace(exception, true);
                var frame2 = st2.GetFrame(1);
                if (frame2 != null)
                {
                    methdeName = string.Format("{0}.{1}.{1}", frame2.GetMethod().DeclaringType.FullName, frame2.GetFileLineNumber(), exception.TargetSite.ToString());
                    moduleName = exception.TargetSite.DeclaringType.Module.Name;
                    //var assemblyName = exception.TargetSite.DeclaringType.Assembly.FullName;
                }
                //var st = new StackTrace(true);
                //var frame = st.GetFrame(0);
                //if (frame != null)
                //{
                //    methdeName = string.Format("{0}.{1}", frame.GetMethod().DeclaringType.FullName, exception.TargetSite.ToString());
                //    moduleName = exception.TargetSite.DeclaringType.Module.Name;
                //    var assemblyName = exception.TargetSite.DeclaringType.Assembly.FullName;
                //}

            }
            catch (Exception)
            {
                // Console.WriteLine("Error In LogErrorAsync Error:{0} \n MethodNamd or MoudleName don't have value", e.Message);

            }
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var com = con.CreateCommand();
                //change To Sp by Parameter . 1400
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 100000;
                com.CommandText = "Tcp_LogErr";
                com.Parameters.AddWithValue("@Business", business);
                com.Parameters.AddWithValue("@Module", moduleName);
                com.Parameters.AddWithValue("@Methode", methdeName);
                com.Parameters.AddWithValue("@Message", exception.Message);
                com.Parameters.AddWithValue("@RawError", exception.ToString());
                com.Parameters.AddWithValue("@ExtraData", ip ?? ""); //ip set in ExtraData
                try
                {
                    con.Open();
                    com.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error In Save Log  Error:{0}", e);
                    Console.WriteLine("Module \n ", moduleName);
                    Console.WriteLine("Methode \n ", methdeName);
                    Console.WriteLine("Message \n ", e.Message);
                    Console.WriteLine("Inner Ex \n ", e.InnerException);
                    Console.WriteLine("Stack Trace \n ", e.StackTrace.ToString());
                    //throw e; 
                }
                finally
                {
                    con.Close();
                }
                GC.Collect();
            }
            return Task.CompletedTask;
        }
    }
}
