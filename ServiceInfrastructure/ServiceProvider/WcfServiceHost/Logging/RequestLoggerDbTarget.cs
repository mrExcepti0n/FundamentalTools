using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider.WcfServiceHost.Logging
{
    public class RequestLoggerDbTarget : IRequestLoggerTarget
    {
        private readonly string _connectionString;

        public RequestLoggerDbTarget(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void SaveRequest(RequestMessage request)
        {
            string queryString =
                "insert into MethodLoggers (MethodName, UserId, MethodParams, CallDate, ShortCallDate, SessionGuid)" +
                "values (@methodName, @userId, @methodParams, @callDate, @shortCallDate, @sessionGuid)";
            DateTime callDate = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@methodName", request.MethodName);
                command.Parameters.AddWithValue("@userId", request.UserId);
                command.Parameters.AddWithValue("@methodParams", request.Parameters);
                command.Parameters.AddWithValue("@callDate", callDate);
                command.Parameters.AddWithValue("@shortCallDate", callDate);
                command.Parameters.AddWithValue("@sessionGuid", request.SessionId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SaveResponse(Guid sessionId)
        {
            string queryString = "update MethodLoggers set FinishDate = @finishDate where SessionGuid = @sessionGuid";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@finishDate", DateTime.Now);
                command.Parameters.AddWithValue("@sessionGuid", sessionId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
