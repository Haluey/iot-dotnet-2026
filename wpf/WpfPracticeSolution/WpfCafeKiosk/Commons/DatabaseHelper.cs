using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WpfCafeKiosk.Commons
{
    public class DatabaseHelper
    {
        // MySQL 연결문자열 key=value;
        private string connStr = "Server=localhost;" +      // 운영 아이피로 변경할 것
                                 "Port=3306;" +     // 운영 포트로 변경할 것
                                 "Database=cafekiosk;" +
                                 "User ID=root;" +      // 운영DB 사용자로 변경할 것
                                 "Password=my123456;" + // 패스워드 변경할 것
                                 "Charset=utf8mb4;";

        // DB조회 메서드
        public DataTable Select(string sql)
        {
            using MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand(sql, conn);

            using MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        // DB실행 메서드(실행결과 리턴)
        // INSERT, UPDATE, DELETE
        public int ExecuteScalar(string sql)
        {
            using MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand(sql, conn);

            // 1건 INSERT하면 1 리턴, 2건 DELETE하면 2 리턴, COUNT(*)하면 카운트 갯수 리턴 등 결과건수를 확인
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        // DB실행 메서드(실행결과 리턴X)
        // INSERT, UPDATE, DELETE
        public void ExecuteNonQuery(string sql)
        {
            using MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.ExecuteNonQuery();  // 결과 안보고 실행가능, 건수리턴도 가능
        }
    }
}
