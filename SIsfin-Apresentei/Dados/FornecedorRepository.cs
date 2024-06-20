using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dados
{
    public class FornecedorRepository
    {
        public string Insert(Fornecedor fornecedor)
        {
            string resp = "";
            try
            {
                Connection.getConnection();

                MySqlCommand SqlCmd = new MySqlCommand
                {
                    Connection = Connection.SqlCon,
                    CommandText = "INSERT INTO Fornecedor (nome, email, tipoPessoa, cpf_cnpj, razao_social, rua, numero, bairro, cidade, complemento, cep, telefone, celular) VALUES ( @pNome, @pEmail, @pTipoPessoa, @pCpf_cnpj, @pRazao_social, @pRua, @pNumero, @pBairro, @pCidade, @pComplemento, @pCep, @pTelefone, @pCelular ) ",
                    CommandType = CommandType.Text
                };
                SqlCmd.Parameters.AddWithValue("pNome", fornecedor.Nome);
                SqlCmd.Parameters.AddWithValue("pEmail", fornecedor.Email);
                SqlCmd.Parameters.AddWithValue("pCpf_cnpj", fornecedor.Cpf_cnpj);
                SqlCmd.Parameters.AddWithValue("pTipoPessoa", fornecedor.TipoFornecedor);
                SqlCmd.Parameters.AddWithValue("pRazao_social", fornecedor.Razao_social);
                SqlCmd.Parameters.AddWithValue("pRua", fornecedor.Rua);
                SqlCmd.Parameters.AddWithValue("pNumero", fornecedor.Numero);
                SqlCmd.Parameters.AddWithValue("pBairro", fornecedor.Bairro);
                SqlCmd.Parameters.AddWithValue("pCidade", fornecedor.Cidade);
                SqlCmd.Parameters.AddWithValue("pComplemento", fornecedor.Complemento);
                SqlCmd.Parameters.AddWithValue("pCep", fornecedor.Cep);
                SqlCmd.Parameters.AddWithValue("pTelefone", fornecedor.Telefone);
                SqlCmd.Parameters.AddWithValue("pCelular", fornecedor.Celular);
                



                //executa o stored procedure
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "SUCESSO" : "FALHA";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (Connection.SqlCon.State == ConnectionState.Open)
                    Connection.SqlCon.Close();
            }

            return resp;
        }

        public string Update(Fornecedor fornecedor)
        {
            string resp = "";
            try
            {
                Connection.getConnection();

                string updateSql = String.Format("UPDATE Fornecedor SET " +
                                    "tipopessoa = @pTipoPessoa, cpf_cnpj = @pCpf_cnpj,razao_social = @pRazao_social, nome = @pNome, rua = @pRua, numero = @pNumero, bairro = @pBairro, cidade = @pCidade, complemento = @pComplemento, cep = @pCep, telefone = @pTelefone,email = @pEmail, celular = @pCelular " +
                                    "WHERE id = @pId "
                                    );
                MySqlCommand SqlCmd = new MySqlCommand(updateSql, Connection.SqlCon);
                SqlCmd.Parameters.AddWithValue("pNome", fornecedor.Nome);
                SqlCmd.Parameters.AddWithValue("pEmail", fornecedor.Email);
                SqlCmd.Parameters.AddWithValue("pCpf_cnpj", fornecedor.Cpf_cnpj);
                SqlCmd.Parameters.AddWithValue("pTipoPessoa", fornecedor.TipoFornecedor);
                SqlCmd.Parameters.AddWithValue("pRazao_social", fornecedor.Razao_social);
                SqlCmd.Parameters.AddWithValue("pRua", fornecedor.Rua);
                SqlCmd.Parameters.AddWithValue("pNumero", fornecedor.Numero);
                SqlCmd.Parameters.AddWithValue("pBairro", fornecedor.Bairro);
                SqlCmd.Parameters.AddWithValue("pCidade", fornecedor.Cidade);
                SqlCmd.Parameters.AddWithValue("pComplemento", fornecedor.Complemento);
                SqlCmd.Parameters.AddWithValue("pCep", fornecedor.Cep);
                SqlCmd.Parameters.AddWithValue("pTelefone", fornecedor.Telefone);
                SqlCmd.Parameters.AddWithValue("pCelular", fornecedor.Celular);
                SqlCmd.Parameters.AddWithValue("pId", fornecedor.Id);



                //executa o stored procedure
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "SUCESSO" : "FALHA";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (Connection.SqlCon.State == ConnectionState.Open)
                    Connection.SqlCon.Close();
            }
            return resp;
        }

        public string Remove(int idFornecedor)
        {
            string resp = "";
            try
            {
                Connection.getConnection();

                string updateSql = String.Format("DELETE FROM Fornecedor " +
                                    "WHERE id = @pId ");
                MySqlCommand SqlCmd = new MySqlCommand(updateSql, Connection.SqlCon);
                SqlCmd.Parameters.AddWithValue("pId", idFornecedor);

                //executa o stored procedure
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "SUCESSO" : "FALHA";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (Connection.SqlCon.State == ConnectionState.Open)
                    Connection.SqlCon.Close();
            }
            return resp;
        }

        public DataTable getAll()
        {
            DataTable DtResultado = new DataTable("Fornecedor");
            try
            {
                Connection.getConnection();
                String sqlSelect = "select * from Fornecedor";

                MySqlCommand SqlCmd = new MySqlCommand();
                SqlCmd.Connection = Connection.SqlCon;
                SqlCmd.CommandText = sqlSelect;
                SqlCmd.CommandType = CommandType.Text;
                MySqlDataAdapter SqlData = new MySqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable filterByName(string pNome)
        {
            DataTable DtResultado = new DataTable("Fornecedor");
            string selectSql;
            try
            {
                Connection.getConnection();
                if (!string.IsNullOrEmpty(pNome))
                {
                    selectSql = String.Format("SELECT * FROM Fornecedor WHERE nome LIKE @pNome");
                    pNome = '%' + pNome + '%';
                }
                else
                {
                    selectSql = String.Format("SELECT * FROM Fornecedor");
                }
                MySqlCommand SqlCmd = new MySqlCommand(selectSql, Connection.SqlCon);
                if (!string.IsNullOrEmpty(pNome))
                    SqlCmd.Parameters.AddWithValue("pNome", pNome);
                MySqlDataAdapter SqlData = new MySqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

    }
}
