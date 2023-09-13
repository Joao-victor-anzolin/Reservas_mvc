using NuGet.Protocol;
using System.Data.SqlClient;
using System.Drawing.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System;

namespace Mvc_Reservas.Models
{
    public class ReservasDB
    {
        private readonly string _stringDeConexao;

        public ReservasDB()
        {
            _stringDeConexao = "Data Source=SB-1491281\\SQLSENAI;Initial Catalog=dbCrudMVC;Integrated Security=True";
        }

        public List<Reserva> getList() 
        {
            List<Reserva> aux = new List<Reserva>();

            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                string query = "Select * From tbReservas";
                SqlCommand _cmd = new SqlCommand(query, _conn);

                _conn.Open();

                using (SqlDataReader reader = _cmd.ExecuteReader())
                {


                    //Data Chegada


                    while (reader.Read())
                    {
                        Reserva reservas = new Reserva
                        {
                            Id =            new Guid(reader["Id"].ToString()),
                            Acomodacao =    reader["Acomodacao"].ToString(),
                            DataChegada =   DateOnly.Parse(reader["DataChegada"].ToString().Split(" ")[0]),
                            DataSaida =     DateOnly.Parse(reader["DataSaida"].ToString().Split(" ")[0]),
                            CafeIncluso =   System.Boolean.Parse(reader["CafeIncluso"].ToString()),
                            Total =         Decimal.Parse(reader["Total"].ToString())
                        };
                        aux.Add(reservas);
                    }
                }
                _conn.Close();
            }
            return aux;
        }
        public List<Reserva> filter(string condicao)
        {
            condicao = condicao == null ? "" : condicao.Trim().ToLower();

            List<Reserva> aux = getList();
            return aux.Where(
                        p => p.Acomodacao.ToLower().Contains(condicao)
                   ).ToList();
        }

        public void insert(Reserva reserva)
        {
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                // Criando instrução de insert
                string instrucao = "INSERT INTO tbReservas(Id, Acomodacao, CafeIncluso, DataChegada, DataSaida,Total) " +
                                   "VALUES(@Id, @Acomodacao, @CafeIncluso, @DataChegada, @DataSaida, @Total) ";

                // Criando o objeto command
                SqlCommand _cmd = new SqlCommand(instrucao, _conn);

                // Preenchendo os parâmetros do Insert
                _cmd.Parameters.AddWithValue("@Id", reserva.Id);
                _cmd.Parameters.AddWithValue("@Acomodacao", reserva.Acomodacao);
                _cmd.Parameters.AddWithValue("@CafeIncluso", reserva.CafeIncluso);
                _cmd.Parameters.AddWithValue("@DataChegada", reserva.DataChegada.ToString());
                _cmd.Parameters.AddWithValue("@DataSaida", reserva.DataSaida.ToString());
                _cmd.Parameters.AddWithValue("@Total", reserva.Total);

                _conn.Open();

                _cmd.ExecuteNonQuery();

                _conn.Close();
            }
        }

        public Reserva getById(string id)
        {
            Reserva aux = new Reserva();

            
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                
                string query = "Select * From tbReservas Where Id=@Id";
                
                SqlCommand _cmd = new SqlCommand(query, _conn);
                
                _cmd.Parameters.AddWithValue("@Id", id);

                _conn.Open();

                
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {

                    while (reader.Read())
                    { 
                        aux.Id = new Guid(reader["Id"].ToString());
                        aux.Acomodacao = reader["Acomodacao"].ToString();
                        aux.CafeIncluso = System.Boolean.Parse(reader["CafeIncluso"].ToString());
                        aux.DataChegada = DateOnly.Parse(reader["DataChegada"].ToString().Split(" ")[0]);
                        aux.DataSaida = DateOnly.Parse(reader["DataSaida"].ToString().Split(" ")[0]);
                        aux.Total = Decimal.Parse(reader["Total"].ToString());
                    }
                }
                // Fechar a conexão
                _conn.Close();
            }

            return aux;
        }

        public void update(Reserva reserva)
        {
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                string instrucao = "UPDATE tbReservas " +
                                   "SET Acomodacao = @Acomodacao, " +
                                        "CafeIncluso = @CafeIncluso, " +
                                        "DataChegada = @DataChegada, " +
                                        "DataSaida = @DataSaida, " +
                                        "Total = @Total " +
                                    "WHERE Id = @Id;";

                SqlCommand _cmd = new SqlCommand(instrucao, _conn);

                // Preenchendo os parâmetros do Insert
                _cmd.Parameters.AddWithValue("@Id", reserva.Id);
                _cmd.Parameters.AddWithValue("@Acomodacao", reserva.Acomodacao);
                _cmd.Parameters.AddWithValue("@CafeIncluso", reserva.CafeIncluso);
                _cmd.Parameters.AddWithValue("@DataChegada", reserva.DataChegada.ToString());
                _cmd.Parameters.AddWithValue("@DataSaida", reserva.DataSaida.ToString());
                _cmd.Parameters.AddWithValue("@Total", reserva.Total);

                _conn.Open();

                _cmd.ExecuteNonQuery();

                _conn.Close();
            }
        }

        public void delete(string id)
        {
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                string instrucao = "DELETE FROM tbReservas " +
                                    "WHERE Id = @Id;";

                SqlCommand _cmd = new SqlCommand(instrucao, _conn);

                _cmd.Parameters.AddWithValue("@Id", id);

                _conn.Open();

                _cmd.ExecuteNonQuery();

                _conn.Close();
            }
        }

    }

}
