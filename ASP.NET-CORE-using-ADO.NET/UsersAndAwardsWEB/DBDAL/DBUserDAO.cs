using System.Collections.Generic;
using Interfaces;
using Entities;
using System.Data.SqlClient;
using System.Linq;

namespace DBDAL
{
    public class DBUserDAO : IUserDAO
    {
        private string _connectionString;

        public DBUserDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User this[int index] 
        {
            get
            {
                User user = new User();
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand commandGetUser = connection.CreateCommand();
                    commandGetUser.CommandText = "GetUser";
                    commandGetUser.CommandType = System.Data.CommandType.StoredProcedure;
                    commandGetUser.Parameters.Add("USERID", System.Data.SqlDbType.Int).Value = index;
                    connection.Open();
                    var reader = commandGetUser.ExecuteReader();
                    if (reader.Read())
                    {
                        user.ID = reader.GetInt32(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        user.BirthDate = reader.GetDateTime(3);
                        connection.Close();
                        user.Awards = (List<Award>)GetAllAwardsOfCurrentUser(index);
                    }
                }
                return user;
            }
        }

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand addUser = connection.CreateCommand();
                addUser.CommandText = "AddUser";
                addUser.CommandType = System.Data.CommandType.StoredProcedure;
                addUser.Parameters.Add("FirstName", System.Data.SqlDbType.VarChar).Value = user.FirstName;
                addUser.Parameters.Add("LastName", System.Data.SqlDbType.VarChar).Value = user.LastName;
                addUser.Parameters.Add("BirthDate", System.Data.SqlDbType.Date).Value = user.BirthDate;
                connection.Open();
                addUser.ExecuteNonQuery();
                SqlCommand getId = new SqlCommand("SELECT CAST(@@IDENTITY AS INT)", connection);
                int ID = (int)getId.ExecuteScalar();
                user.ID = ID;
                foreach (var item in user.Awards)
                {
                    SqlCommand addAward = new SqlCommand("AddAwardForCurrentUser", connection);
                    addAward.CommandType = System.Data.CommandType.StoredProcedure;
                    addAward.Parameters.Add("USERID", System.Data.SqlDbType.Int).Value = user.ID;
                    addAward.Parameters.Add("AWARDID", System.Data.SqlDbType.Int).Value = item.ID;
                    addAward.ExecuteNonQuery();
                }
            }
        }

        public void ChangeUser(User user, int index)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand addUser = connection.CreateCommand();
                addUser.CommandText = "ChangeUser";
                addUser.CommandType = System.Data.CommandType.StoredProcedure;
                addUser.Parameters.Add("USERID", System.Data.SqlDbType.Int).Value = index;
                addUser.Parameters.Add("FirstName", System.Data.SqlDbType.VarChar).Value = user.FirstName;
                addUser.Parameters.Add("LastName", System.Data.SqlDbType.VarChar).Value = user.LastName;
                addUser.Parameters.Add("BirthDate", System.Data.SqlDbType.Date).Value = user.BirthDate;
                connection.Open();
                addUser.ExecuteNonQuery();

                var awards = GetAllAwardsOfCurrentUser(index);

                var addItems = user.Awards.Except(awards).ToList();
                var delItems = awards.Except(user.Awards).ToList();

                foreach (var item in addItems)
                {
                    SqlCommand addAward = new SqlCommand("AddAwardForCurrentUser", connection);
                    addAward.CommandType = System.Data.CommandType.StoredProcedure;
                    addAward.Parameters.Add("USERID", System.Data.SqlDbType.Int).Value = index;
                    addAward.Parameters.Add("AWARDID", System.Data.SqlDbType.Int).Value = item.ID;
                    addAward.ExecuteNonQuery();
                }
                foreach (var item in delItems)
                {
                    SqlCommand addAward = new SqlCommand("DeleteAwardForCurrentUser", connection);
                    addAward.CommandType = System.Data.CommandType.StoredProcedure;
                    addAward.Parameters.Add("AWARDID", System.Data.SqlDbType.Int).Value = item.ID;
                    addAward.Parameters.Add("USERID", System.Data.SqlDbType.Int).Value = index;
                    addAward.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand deleteUser = connection.CreateCommand();
                deleteUser.CommandText = "DeleteUser";
                deleteUser.CommandType = System.Data.CommandType.StoredProcedure;
                deleteUser.Parameters.Add("USERID", System.Data.SqlDbType.Int).Value = user.ID;
                connection.Open();
                deleteUser.ExecuteNonQuery();
            }
        }

        public IList<User> GetAll()
        {
            List<User> users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand commandGetUsers = connection.CreateCommand();
                commandGetUsers.CommandText = "GetAllUsers";
                commandGetUsers.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                var reader = commandGetUsers.ExecuteReader();
                int counterOfUsers = 0;
                while (reader.Read())
                {
                    User user = new User();
                    user.ID = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.BirthDate = reader.GetDateTime(3);
                    user.Awards = (List<Award>)GetAllAwardsOfCurrentUser(counterOfUsers++);
                    users.Add(user);
                }
            }
            return users;
        }

        public IList<Award> GetAllAwardsOfCurrentUser(int index)
        {
            List<Award> awards = new List<Award>();

            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand getAwards = connection.CreateCommand();
                getAwards.CommandText = "GetAwardsOfCurrentUser";
                getAwards.CommandType = System.Data.CommandType.StoredProcedure;
                getAwards.Parameters.Add("USERID", System.Data.SqlDbType.Int).Value = index;
                connection.Open();
                var readerAwards = getAwards.ExecuteReader();
                while (readerAwards.Read())
                {
                    Award award = new Award();
                    award.ID = readerAwards.GetInt32(0);
                    award.Title = readerAwards.GetString(1);
                    award.Description = readerAwards.GetString(2);
                    awards.Add(award);
                }
            }
            return awards;
        }
    }
}
