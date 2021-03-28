using Microsoft.Extensions.Options;
using SalesManagementSystem.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesManagementSystem.Dto;
using SalesManagementSystem.Dao;
using Npgsql;
using System.Data.SqlClient;

namespace SalesManagementSystem.Logic
{
    public class OrderInputLogic
    {
        /// <summary>
        /// 設定記述オブジェクト
        /// </summary>
        private readonly IOptions<DBSetting> options = null;

        public OrderInputLogic(IOptions<DBSetting> options) 
        {
            //設定記述オブジェクトの取り込み
            this.options = options;
        }

        //////////////////////////////////【読み取り】////////////////////////////////////////////////////////////////////
        /// <summary>
        /// データベースから全情報を取得する。
        /// </summary>
        /// <returns></returns>
        public List<OrderInputEntity> ReadList() 
        {
            List<OrderInputEntity> orderInputEntities = new List<OrderInputEntity>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; Port = 5432; User Id = postgres; Password = postgres09; Database = SalesManagement"))
            using (NpgsqlCommand cmd = new NpgsqlCommand("",connection)) 
            {
                //NpgsqlConnectionクラスのOpenメソッドを呼び出す。
                connection.Open();

                //引数にcmdを指定し、OrderInputDaoクラスのインスタンスdaoを生成する。
                OrderInputDao dao = new OrderInputDao(cmd);

                try
                {
                    orderInputEntities = dao.Read();
                }
                catch (SqlException e)
                {
                    throw e;
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
            }

            return orderInputEntities;
        }

        //////////////////////////////////【登録】////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 新規で登録する処理
        /// </summary>
        /// <param name="clientEntity"></param>
        /// <param name="ordersEntity"></param>
        public void InsertData(ClientEntity clientEntity,OrdersEntity ordersEntity) //OrderInputToDBEntity inputToDBEntity
        {
            //using (NpgsqlConnection connection = new NpgsqlConnection(options.Value.ConnectionString))
            //Daoを経由してデータを挿入する。 "Server=localhost;Port=5432;User Id=postgres;Password=postgres09;Database=addressbook"
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; Port = 5432; User Id = postgres; Password = postgres09; Database = SalesManagement"))
            using (NpgsqlCommand cmd = new NpgsqlCommand("", connection))
            {
                //NpgsqlConnectionクラスのOpenメソッドを呼び出す。
                connection.Open();

                //引数にcmdを指定し、OrderInputDaoクラスのインスタンスdaoを生成する。
                OrderInputDao dao = new OrderInputDao(cmd);

                try
                {
                    dao.Regist(clientEntity, ordersEntity);
                    //dao.RegistClient(clientEntity);
                    //dao.RegistOrders(ordersEntity);
                }
                catch (SqlException e)
                {
                    throw e;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        //////////////////////////////////【更新】////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 登録情報を更新する処理
        /// </summary>
        public void UpdateData(ClientEntity clientEntity, OrdersEntity ordersEntity) 
        {
            //Daoを経由してデータを更新する。
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; Port = 5432; User Id = postgres; Password = postgres09; Database = SalesManagement"))
            using (NpgsqlCommand cmd = new NpgsqlCommand("", connection)) 
            {
                //NpgsqlConnectionクラスのOpenメソッドを呼び出す。
                connection.Open();

                //引数にcmdを指定し、OrderInputDaoクラスのインスタンスdaoを呼び出す。
                OrderInputDao dao = new OrderInputDao(cmd);

                try
                {
                    dao.Update(clientEntity, ordersEntity);
                }
                catch (SqlException e)
                {
                    throw e;
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
            }
        }

        //////////////////////////////////【削除】////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 登録情報を削除する処理
        /// </summary>
        /// <param name="orderInputEntity"></param>
        public void DeleteData(OrderInputEntity orderInputEntity) 
        {
            //Daoを経由してデータベースを更新する。
            using (NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; Port = 5432; User Id = postgres; Password = postgres09; Database = SalesManagement"))
            using (NpgsqlCommand cmd = new NpgsqlCommand("",connection)) 
            {
                //NpgsqlConnectionクラスのopenメソッドを使ってデータベースに接続する。
                connection.Open();

                //引数にcmdを渡し、OrderInputDaoクラスのインスタンスdaoを呼び出す。
                OrderInputDao dao = new OrderInputDao(cmd);

                try
                {
                    dao.Delete(orderInputEntity);
                }
                catch (SqlException e)
                {
                    throw e;
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
            }
        }
    }
}
