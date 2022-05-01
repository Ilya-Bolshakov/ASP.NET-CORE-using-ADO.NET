using System;
using System.Collections.Generic;
using Interfaces;
using Entities;
using System.Data.SqlClient;

namespace DBDAL
{
    public class DBAwardsDAO : IAwardDAO
    {
        private string _connectionString;

        public DBAwardsDAO(string connectiongString)
        {
            _connectionString = connectiongString;
        }
        public Award this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddNewAward(Award award)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand addNewAward = connection.CreateCommand();
                addNewAward.CommandText = "AddAward";
                addNewAward.CommandType = System.Data.CommandType.StoredProcedure;
                addNewAward.Parameters.Add("TITLE", System.Data.SqlDbType.VarChar).Value = award.Title;
                addNewAward.Parameters.Add("DESCRIPTION", System.Data.SqlDbType.VarChar).Value = award.Description;
                connection.Open();
                addNewAward.ExecuteNonQuery();
            }
        }

        public void ChangeAward(Award award, int index)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand changeAward = connection.CreateCommand();
                changeAward.CommandText = "ChangeAward";
                changeAward.CommandType = System.Data.CommandType.StoredProcedure;
                changeAward.Parameters.Add("AWARDID", System.Data.SqlDbType.Int).Value = index;
                changeAward.Parameters.Add("TITLE", System.Data.SqlDbType.VarChar).Value = award.Title;
                changeAward.Parameters.Add("DESCRIPTION", System.Data.SqlDbType.VarChar).Value = award.Description;
                connection.Open();
                changeAward.ExecuteNonQuery();
            }
        }

        public void DeleteAward(Award award)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand deleteAward = connection.CreateCommand();
                deleteAward.CommandText = "DeleteAward";
                deleteAward.CommandType = System.Data.CommandType.StoredProcedure;
                deleteAward.Parameters.Add("AWARDID", System.Data.SqlDbType.Int).Value = award.ID;
                connection.Open();
                deleteAward.ExecuteNonQuery();
            }
        }

        public IList<Award> GetAll()
        {
            List<Award> awards = new List<Award>();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand commandGetAwards = connection.CreateCommand();
                commandGetAwards.CommandText = "GetAllAwards";
                commandGetAwards.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                var reader = commandGetAwards.ExecuteReader();
                while (reader.Read())
                {
                    Award award = new Award();
                    award.ID = reader.GetInt32(0);
                    award.Title = reader.GetString(1);
                    award.Description = reader.GetString(2);
                    awards.Add(award);
                }
            }
            return awards;
        }

        public Award GetAward(int ID)
        {
            Award award = new Award();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand GetAward = connection.CreateCommand();
                GetAward.CommandText = "GetAward";
                GetAward.CommandType = System.Data.CommandType.StoredProcedure;
                GetAward.Parameters.Add("AWARDID", System.Data.SqlDbType.Int).Value = ID;
                connection.Open();
                var reader = GetAward.ExecuteReader();
                if (reader.Read())
                {
                    award.ID = reader.GetInt32(0);
                    award.Title = reader.GetString(1);
                    award.Description = reader.GetString(2);
                }    
                
            }
            return award;
        }
    }
}
