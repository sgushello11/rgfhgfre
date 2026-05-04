using System;
using System.Data;
using System.Data.SqlClient;

namespace Master_floor
{
    public class DatabaseHelper
    {
        private string conn = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=TestBase;Integrated Security=True;TrustServerCertificate=True";

        // Получить всех партнеров с суммой продаж
        public DataTable GetPartnersWithSales()
        {
            string sql = @"
                SELECT 
                    p.ID,
                    p.[Тип партнера] as Тип_партнера,
                    p.[Наименование партнера] as Наименование_партнера,
                    p.Директор,
                    p.[Телефон партнера] as Телефон_партнера,
                    ISNULL(SUM(pp.[Количество продукции]), 0) as Общая_сумма_продаж
                FROM Partners p
                LEFT JOIN Partners_product pp ON p.ID = pp.[ID Partner]
                GROUP BY p.ID, p.[Тип партнера], p.[Наименование партнера], p.Директор, p.[Телефон партнера]
            ";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Добавить партнера
        public void AddPartner(Partner p)
        {
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                string sql = @"
                    INSERT INTO Partners ([Тип партнера], [Наименование партнера], Директор, [Телефон партнера], Рейтинг)
                    VALUES (@t, @n, @d, @ph, 0)";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@t", p.Тип_партнера);
                cmd.Parameters.AddWithValue("@n", p.Наименование_партнера);
                cmd.Parameters.AddWithValue("@d", p.Директор);
                cmd.Parameters.AddWithValue("@ph", p.Телефон_партнера);
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Обновить партнера
        public void UpdatePartner(Partner p)
        {
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                string sql = @"
            UPDATE Partners 
            SET [Тип партнера]=@t, 
                [Наименование партнера]=@n, 
                Директор=@d, 
                [Телефон партнера]=@ph
            WHERE ID=@id";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@id", p.ID);
                cmd.Parameters.AddWithValue("@t", p.Тип_партнера);
                cmd.Parameters.AddWithValue("@n", p.Наименование_партнера);
                cmd.Parameters.AddWithValue("@d", p.Директор);
                cmd.Parameters.AddWithValue("@ph", p.Телефон_партнера);
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Удалить партнера
        public void DeletePartner(int id)
        {
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                cnn.Open();
                SqlCommand cmd1 = new SqlCommand("DELETE FROM Partners_product WHERE [ID Partner]=@id", cnn);
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM Partners WHERE ID=@id", cnn);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.ExecuteNonQuery();
            }
        }

        // Получить партнера по ID
        public Partner GetPartnerById(int id)
        {
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                string sql = "SELECT * FROM Partners WHERE ID=@id";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@id", id);
                cnn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    return new Partner
                    {
                        ID = Convert.ToInt32(r["ID"]),
                        Тип_партнера = r["Тип партнера"].ToString(),
                        Наименование_партнера = r["Наименование партнера"].ToString(),
                        Директор = r["Директор"].ToString(),
                        Телефон_партнера = r["Телефон партнера"].ToString()
                    };
                }
                return null;
            }
        }

        // История продаж
        public DataTable GetSalesHistory(int partnerId)
        {
            string sql = @"
                SELECT 
                    prod.[Наименование продукции] as Продукт,
                    pp.[Количество продукции] as Количество,
                    pp.[Дата продажи] as Дата
                FROM Partners_product pp
                JOIN Product prod ON pp.[ID Product] = prod.ID
                WHERE pp.[ID Partner] = @id";
            SqlCommand cmd = new SqlCommand(sql, new SqlConnection(conn));
            cmd.Parameters.AddWithValue("@id", partnerId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Название партнера по ID
        public string GetPartnerName(int id)
        {
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("SELECT [Наименование партнера] FROM Partners WHERE ID=@id", cnn);
                cmd.Parameters.AddWithValue("@id", id);
                cnn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
        }

        // Общая сумма продаж
        public double GetTotalSales(int id)
        {
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM([Количество продукции]),0) FROM Partners_product WHERE [ID Partner]=@id", cnn);
                cmd.Parameters.AddWithValue("@id", id);
                cnn.Open();
                return Convert.ToDouble(cmd.ExecuteScalar());
            }
        }
    }
}