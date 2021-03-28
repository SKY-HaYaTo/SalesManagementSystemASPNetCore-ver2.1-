using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesManagementSystem.Dto;
using Npgsql;
using NpgsqlTypes;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SalesManagementSystem.Dao
{
    public class OrderInputDao
    {
        /// <summary>
        /// SQLコマンド
        /// </summary>
        private readonly NpgsqlCommand sqlCommand = null;

        /// <summary>
        /// インスタンス生成時に、接続の取り込みを行う。
        /// </summary>
        /// <param name="command"></param>
        public OrderInputDao(NpgsqlCommand command) 
        {
            //接続の取り込み
            this.sqlCommand = command;
        }

        //////////////////////////////////【読み取り】////////////////////////////////////////////////////////////////////

        //Clientテーブルで使用するパラメータの初期化
        int paramClientIdInputName = 0;
        long paramOrderId = 0;
        string paramClientName = string.Empty;
        string paramDepartment = string.Empty;
        string paramPost = string.Empty;
        string paramPostAddress = string.Empty;
        string paramTel = string.Empty;
        string paramFax = string.Empty;
        string paramEmail = string.Empty;

        //Ordersテーブルで使用するパラメータの初期化
        int ordersTblParamClientIdInputName = 0;
        long ordersTblParamOrderId = 0;
        DateTime paramOrderDate = DateTime.MaxValue; //『DateTime.MaxValue』または『DateTime.MinValue』のいずれかで初期化する。
        DateTime paramDeliveryDate = DateTime.MaxValue; //『DateTime.MaxValue』または『DateTime.MinValue』のいずれかで初期化する。
        long paramTotal = 0;
        string paramOrder1 = string.Empty;
        int paramOrder1price = 0;
        int paramOrder1Num = 0;
        string paramOrder2 = string.Empty;
        int paramOrder2price = 0;
        int paramOrder2Num = 0;
        string paramOrder3 = string.Empty;
        int paramOrder3price = 0;
        int paramOrder3Num = 0;
        string paramOrder4 = string.Empty;
        int paramOrder4price = 0;
        int paramOrder4Num = 0;
        string paramOrder5 = string.Empty;
        int paramOrder5price = 0;
        int paramOrder5Num = 0;

        /// <summary>
        /// 会社IDを昇順に並べるクエリ。
        /// </summary>
        private const string QUERY_COUNT_CLIENT = "SELECT kaishaId FROM client ORDER BY kaishaId";

        /// <summary>
        /// Ordersテーブルから会社IDを使ってレコードを1件ないし複数件取得するクエリ。
        /// </summary>
        private const string QUERY_READ_ORDERS = "SELECT * FROM orders WHERE kaishaid";

        /// <summary>
        /// Clientテーブルから会社IDを使ってレコードを1件ないし複数件取得するクエリ。
        /// </summary>
        private const string QUERY_READ_CLIENT = "SELECT * FROM client WHERE kaishaid";

        /// <summary>
        /// ClientとOrdersテーブルから【会社ID】と【注文ID】をキーとしてレコード(27列)を取得する。
        /// 【会社ID】キーで昇順にする。
        /// </summary>
        private const string QUERY_READ_CLIENT_ORDERS = @"
            SELECT client.kaishaid,client.kaishamei,client.bushomei,client.postnum,client.postaddress,client.tel,client.fax,client.email,client.orderid,
                orders.orderdate,orders.deliverydate,orders.total,orders.ordername1,orders.orderprice1,orders.ordernum1,orders.ordername2,orders.orderprice2,orders.ordernum2,
                orders.ordername3,orders.orderprice3,orders.ordernum3,orders.ordername4,orders.orderprice4,orders.ordernum4,orders.ordername5,orders.orderprice5,orders.ordernum5
            FROM 
                client
            INNER JOIN
                orders
            ON
                client.kaishaid = orders.kaishaid AND client.orderid = orders.orderid
                ORDER BY client.kaishaid ASC;";

        /// <summary>
        /// テーブルに表示するためMicrosoft.AspNetCore.MvcパッケージのJsonResultクラスを使用してJsonを戻り値とする。
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        public List<OrderInputEntity> Read() //public string Read()
        {
            //List型エンティティクラスを初期化する。
            List<OrderInputEntity> orderInputEntities = new List<OrderInputEntity>();

            //クエリを指定する。
            sqlCommand.CommandText =OrderInputDao.QUERY_READ_CLIENT_ORDERS ;
            //var cmd = new SqlCommand();
            //cmd.CommandText = QUERY_READ_CLIENT_ORDERS;

            //コマンドを実行する。
            using (NpgsqlDataReader  reader = sqlCommand.ExecuteReader()) 
            {
                while (reader.Read()) 
                {
                    //DBカラムの型がBigIntの場合はlong型の変数kaishaidを用意し、SqlDataReaderクラスのGetInt64メソッドでlong型に変換する。
                    long kaishaid = reader.GetInt64(reader.GetOrdinal(OrderInputEntity.COL_NAME_KAISHAID));
                    string kaishamei = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_KAISHAMEI));
                    string bushomei = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_BUSHOMEI));
                    string postnum = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_POSTNUM));
                    string postaddress = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_POSTADDRESS));
                    string tel = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_TEL));
                    string fax = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_FAX));
                    string email = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_EMAIL));
                    //DBカラムの型がBigIntの場合はlong型の変数orderidを用意し、SqlDataReaderクラスのGetInt64メソッドでlong型に変換する。
                    long orderid = reader.GetInt64(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERID)); 
                    //DBカラムの型がDateTime without TimeZoneの場合はDateTime型の変数orderdateを用意し、SqlDataReaderクラスのGetDateTimeメソッドでDateTime型に変換する。
                    DateTime orderdate = reader.GetDateTime(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERDATE));
                    //DBカラムの型がDateTime without TimeZoneの場合はDateTime型の変数deliverydateを用意し、SqlDataReaderクラスのGetDateTimeメソッドでDateTime型に変換する。
                    DateTime deliverydate = reader.GetDateTime(reader.GetOrdinal(OrderInputEntity.COL_NAME_DELIVERYDATE));
                    //DBカラムの型がBigIntの場合はlong型の変数totalを用意し、SqlDataReaderクラスのGetInt64メソッドでlong型に変換する。
                    long total = reader.GetInt64(reader.GetOrdinal(OrderInputEntity.COL_NAME_TOTAL)); //string total = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_TOTAL));
                    string ordername1 = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNAME1));
                    //DBカラムの型がintの場合はint型の変数orderprice1を用意し、SqlDataReaderクラスのGetInt32メソッドでint型に変換する。
                    int orderprice1 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERPRICE1));
                    int ordernum1 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNUM1));
                    string ordername2 = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNAME2));
                    int orderprice2 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERPRICE2));
                    int ordernum2 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNUM2));
                    string ordername3 = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNAME3));
                    int orderprice3 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERPRICE3));
                    int ordernum3 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNUM3));
                    string ordername4 = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNAME4));
                    int orderprice4 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERPRICE4));
                    int ordernum4 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNUM4));
                    string ordername5 = reader.GetString(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNAME5));
                    int orderprice5 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERPRICE5));
                    int ordernum5 = reader.GetInt32(reader.GetOrdinal(OrderInputEntity.COL_NAME_ORDERNUM5));

                    //取得データを格納する。
                    OrderInputEntity item = new OrderInputEntity()
                    {
                        ClientIdInputName = kaishaid.ToString(),
                        ClientName = kaishamei,
                        Department = bushomei,
                        Post = postnum,
                        PostAddress = postaddress,
                        Tel = tel,
                        Fax = fax,
                        Email = email,
                        OrderId = orderid.ToString(),
                        OrderDate = orderdate.ToString(),
                        DeliveryDate = deliverydate.ToString(),
                        Total = total.ToString(),
                        order1 = ordername1,
                        order1price = orderprice1.ToString(),
                        order1Num = ordernum1.ToString(),
                        order2 = ordername2,
                        order2price = orderprice2.ToString(),
                        order2Num = ordernum2.ToString(),
                        order3 = ordername3,
                        order3price = orderprice3.ToString(),
                        order3Num = ordernum3.ToString(),
                        order4 = ordername4,
                        order4price = orderprice4.ToString(),
                        order4Num = ordernum4.ToString(),
                        order5 = ordername5,
                        order5price = orderprice5.ToString(),
                        order5Num = ordernum5.ToString()

                    };

                    //Listに詰める。
                    orderInputEntities.Add(item);
                }
            }


            /*
            while (reader.Read()) 
            {
                string clientIdParam = (string)reader.GetValue(0);
                string orderIdParam = (string)reader.GetValue(1);
                string clientNameParam = (string)reader.GetValue(2);
                string departmentParam = (string)reader.GetValue(3);
                string postParam = (string)reader.GetValue(4);
                string postAddressParam = (string)reader.GetValue(5);
                string telParam = (string)reader.GetValue(6);
                string faxParam = (string)reader.GetValue(7);
                string emailParam = (string)reader.GetValue(8);
                string orderDateParam = (string)reader.GetValue(9);
                string deliveryDateParam = (string)reader.GetValue(10);
                string totalParam = (string)reader.GetValue(11);
                string order1Param = (string)reader.GetValue(12);
                string order1priceParam = (string)reader.GetValue(13);
                string order1NumParam = (string)reader.GetValue(14);
                string order2Param = (string)reader.GetValue(15);
                string order2priceParam = (string)reader.GetValue(16);
                string order2NumParam = (string)reader.GetValue(17);
                string order3Param = (string)reader.GetValue(18);
                string order3priceParam = (string)reader.GetValue(19);
                string order3NumParam = (string)reader.GetValue(20);
                string order4Param = (string)reader.GetValue(21);
                string order4priceParam = (string)reader.GetValue(22);
                string order4NumParam = (string)reader.GetValue(23);
                string order5Param = (string)reader.GetValue(24);
                string order5priceParam = (string)reader.GetValue(25);
                string order5NumParam = (string)reader.GetValue(26);
                orderInputEntities.Add(new OrderInputEntity(
                    clientIdParam, orderIdParam, clientNameParam, departmentParam, postParam, postAddressParam,
                    telParam, faxParam, emailParam, orderDateParam, deliveryDateParam, totalParam, order1Param, order1priceParam, order1NumParam,
                    order2Param, order2priceParam, order2NumParam, order3Param, order3priceParam, order3NumParam, order4Param, order4priceParam, order4NumParam,
                    order5Param, order5priceParam, order5NumParam));
            }
            
            //ClientEntityインスタンスを宣言し、ReadClient()メソッドから結果を格納する。
            ClientEntity clientEntity = ReadClient();

            //OrdersEntityインスタンスを宣言し、ReadOrders()メソッドから結果を格納する。
            OrdersEntity ordersEntity = ReadOrders();

            //RecordCount()メソッドからClientテーブルに登録されている件数を変数recordCountに入れる。
            int recordCount = RecordCount();

            for (int i=0; i<=recordCount; i++) 
            {
                orderInputEntities[i].ClientIdInputName = clientEntity.ClientIdInputName.ToString();
                orderInputEntities[i].ClientName = clientEntity.ClientName;
                orderInputEntities[i].Department = clientEntity.Department;
                orderInputEntities[i].Post = clientEntity.Post;
                orderInputEntities[i].PostAddress = clientEntity.PostAddress;
                orderInputEntities[i].Tel = clientEntity.Tel;
                orderInputEntities[i].Fax = clientEntity.Fax;
                orderInputEntities[i].Email = clientEntity.Email;
                orderInputEntities[i].OrderId = clientEntity.OrderId.ToString();
                orderInputEntities[i].OrderDate = ordersEntity.OrderDate.ToString();
                orderInputEntities[i].DeliveryDate = ordersEntity.DeliveryDate.ToString();
                orderInputEntities[i].Total = ordersEntity.Total.ToString();
                orderInputEntities[i].order1 = ordersEntity.order1;
                orderInputEntities[i].order1price = ordersEntity.order1price.ToString();
                orderInputEntities[i].order1Num = ordersEntity.order1Num.ToString();
                orderInputEntities[i].order2 = ordersEntity.order2;
                orderInputEntities[i].order2price = ordersEntity.order2price.ToString();
                orderInputEntities[i].order2Num = ordersEntity.order2Num.ToString();
                orderInputEntities[i].order3 = ordersEntity.order3;
                orderInputEntities[i].order3price = ordersEntity.order3price.ToString();
                orderInputEntities[i].order3Num = ordersEntity.order3Num.ToString();
                orderInputEntities[i].order4 = ordersEntity.order4;
                orderInputEntities[i].order4price = ordersEntity.order4price.ToString();
                orderInputEntities[i].order4Num = ordersEntity.order4Num.ToString();
                orderInputEntities[i].order5 = ordersEntity.order5;
                orderInputEntities[i].order5price = ordersEntity.order5price.ToString();
                orderInputEntities[i].order5Num = ordersEntity.order5Num.ToString();
            }
            */
            //Newtonsoft.JsonパッケージのJsonConvertメソッドでJsonオブジェクトを生成する。
            string jsonData = JsonConvert.SerializeObject(orderInputEntities);
            
            return orderInputEntities;

        }

        /// <summary>
        /// SqlCommand.ExecuteScalar メソッドを使って件数を取得する。
        /// </summary>
        /// <returns></returns>
        private int RecordCount() 
        {
            //変数cntを初期化する。
            int cnt = 0;

            var cmd = new SqlCommand();
            cmd.CommandText = QUERY_COUNT_CLIENT;

            //SqlCommand.ExecuteScalar メソッドを使ってクエリの結果を取得する。
            if (cmd.ExecuteScalar() != null) 
            {
                cnt = (int)cmd.ExecuteScalar();
            }

            //取得件数をログ出力
            Console.WriteLine("取得件数：" + cnt);

            cmd.Dispose();

            return cnt;
            
        }

        /// <summary>
        /// 会社IDと注文IDをキーにしてClientテーブルからレコードを1件取得する。
        /// </summary>
        /// <returns></returns>
        private ClientEntity ReadClient() 
        {
            //エンティティクラスを初期化する。
            ClientEntity clientEntity = null;

            //クエリを指定する。
            sqlCommand.CommandText = OrderInputDao.QUERY_READ_CLIENT;

            //SQLパラメータをクリアする。
            sqlCommand.Parameters.Clear();

            //クエリを実行する。
            using (NpgsqlDataReader reader = sqlCommand.ExecuteReader()) 
            {
                //レコードを取得する。
                if (reader.Read()) 
                {
                    paramClientIdInputName = (int)reader.GetValue(0);
                    paramOrderId = (long)reader.GetValue(1);
                    paramClientName = (string)reader.GetValue(2);
                    paramDepartment = (string)reader.GetValue(3);
                    paramPost = (string)reader.GetValue(4);
                    paramPostAddress = (string)reader.GetValue(5);
                    paramTel = (string)reader.GetValue(6);
                    paramFax = (string)reader.GetValue(7);
                    paramEmail = (string)reader.GetValue(8);

                    clientEntity = new ClientEntity() 
                    {
                        ClientIdInputName = paramClientIdInputName,
                        OrderId = paramOrderId,
                        ClientName = paramClientName,
                        Department = paramDepartment,
                        Post = paramPost,
                        PostAddress = paramPostAddress,
                        Tel = paramTel,
                        Fax = paramFax,
                        Email = paramEmail
                    };
                }
            }

            //クエリとパラメタをクリアする。
            sqlCommand.CommandText = "";
            sqlCommand.Parameters.Clear();

            return clientEntity;
        }

        /// <summary>
        /// 会社IDと注文IDをキーにしてOrdersテーブルからレコードを1件取得する。
        /// </summary>
        /// <returns></returns>
        private OrdersEntity ReadOrders() 
        {
            //エンティティクラスを初期化する。
            OrdersEntity ordersEntity = null;

            //クエリを指定する。
            sqlCommand.CommandText = OrderInputDao.QUERY_READ_ORDERS;

            //SQLパラメータをクリアする。
            sqlCommand.Parameters.Clear();

            //コマンドを実行する。
            using (NpgsqlDataReader reader = sqlCommand.ExecuteReader()) 
            {
                //レコードを取得する。
                if (reader.Read()) 
                {
                    ordersTblParamClientIdInputName = (int)reader.GetValue(0); //reader.GetInt32(ordersEntities[0].ClientIdInputName);
                    ordersTblParamOrderId = (long)reader.GetValue(1);
                    paramOrderDate = (DateTime)reader.GetValue(2);
                    paramDeliveryDate = (DateTime)reader.GetValue(3);
                    paramTotal = (long)reader.GetValue(4);
                    paramOrder1 = (string)reader.GetValue(5);
                    paramOrder1price = (int)reader.GetValue(6);
                    paramOrder1Num = (int)reader.GetValue(7);
                    paramOrder2 = (string)reader.GetValue(8);
                    paramOrder2price = (int)reader.GetValue(9);
                    paramOrder2Num = (int)reader.GetValue(10);
                    paramOrder3 = (string)reader.GetValue(11);
                    paramOrder3price = (int)reader.GetValue(12);
                    paramOrder3Num = (int)reader.GetValue(13);
                    paramOrder4 = (string)reader.GetValue(14);
                    paramOrder4price = (int)reader.GetValue(15);
                    paramOrder4Num = (int)reader.GetValue(16);
                    paramOrder5 = (string)reader.GetValue(17);
                    paramOrder5price = (int)reader.GetValue(18);
                    paramOrder5Num = (int)reader.GetValue(19);

                    //取得データをエンティティに格納する。
                    //エンティティクラスをnewする。
                    ordersEntity = new OrdersEntity() 
                    {
                        ClientIdInputName = ordersTblParamClientIdInputName,
                        OrderId = ordersTblParamOrderId,
                        OrderDate = paramOrderDate,
                        DeliveryDate = paramDeliveryDate,
                        Total = paramTotal,
                        order1 = paramOrder1,
                        order1price = paramOrder1price,
                        order1Num = paramOrder1Num,
                        order2 = paramOrder2,
                        order2price = paramOrder2price,
                        order2Num = paramOrder2Num,
                        order3 = paramOrder3,
                        order3price = paramOrder3price,
                        order3Num = paramOrder3Num,
                        order4 = paramOrder4,
                        order4price = paramOrder4price,
                        order4Num = paramOrder4Num,
                        order5 = paramOrder5,
                        order5price = paramOrder5price,
                        order5Num = paramOrder5Num
                    };

                    /*
                    ordersEntities.Add(new List<OrdersEntity>(ClientIdInputName, OrderId, OrderDate, DeliveryDate, Total, order1, order1price, order1Num
                        , order2, order2price, order2Num, order3, order3price, order3Num, order4, order4price, order4Num, order5, order5price, order5Num));

                    ordersEntities.Add((ordersEntities[0].ClientIdInputName).Parse(ClientIdInputName)); 

                    ordersEntities.AddRange(ClientIdInputName, OrderId, OrderDate, DeliveryDate, Total, order1, order1price, order1Num
                        , order2, order2price, order2Num, order3, order3price, order3Num, order4, order4price, order4Num, order5, order5price, order5Num);
                    */
                }
            }

            //クエリとパラメタをクリアする。
            sqlCommand.CommandText = "";
            sqlCommand.Parameters.Clear();

            return ordersEntity;
        }


        //////////////////////////////////【登録】////////////////////////////////////////////////////////////////////

        /// <summary>
        /// clientテーブルにデータを登録するクエリ
        /// </summary>
        private const string QUERY_REGIST_TO_CLIENT = "INSERT INTO client(kaishaid,kaishamei,bushomei,postnum,postaddress,tel,fax,email,orderid)" +
            " VALUES(:targetKaishaid,:targetKaishamei,:targetBushomei,:targetPostnum,:targetPostaddress,:targetTel,:targetFax,:targetEmail,:targetOrderid);";

        /// <summary>
        /// ordersテーブルにデータを登録するクエリ
        /// </summary>
        private const string QUERY_REGIST_TO_ORDERS = "INSERT INTO orders(kaishaid,orderid,orderdate,deliverydate,total,ordername1,orderprice1,ordernum1,ordername2,orderprice2,ordernum2,ordername3,orderprice3,ordernum3,ordername4,orderprice4,ordernum4,ordername5,orderprice5,ordernum5)" +
            " VALUES(:targetKaishaid,:targetOrderid,:targetOrderdate,:targetDeliverydate,:targetTotal,:targetOrdername1,:targetOrderprice1,:targetOrdernum1,:targetOrdername2,:targetOrderprice2,:targetOrdernum2,:targetOrdername3,:targetOrderprice3,:targetOrdernum3,:targetOrdername4,:targetOrderprice4,:targetOrdernum4,:targetOrdername5,:targetOrderprice5,:targetOrdernum5);";

        /// <summary>
        /// 新規でClientテーブルとOrdersテーブルにデータを登録する。
        /// </summary>
        public void Regist(ClientEntity clientEntity,OrdersEntity ordersEntity) //OrderInputEntity orderInputEntity OrderInputToDBEntity inputToDBEntity
        {
            RegistClient(clientEntity);
            RegistOrders(ordersEntity);
        }

        /// <summary>
        /// Clientテーブルにデータを登録する。
        /// </summary>
        /// <param name="orderInputEntity"></param>
        public void RegistClient(ClientEntity clientEntity) //OrderInputEntity orderInputEntity OrderInputToDBEntity inputToDBEntity
        {
            //クエリの指定
            sqlCommand.CommandText = OrderInputDao.QUERY_REGIST_TO_CLIENT;

            //SQLパラメタをクリアする。
            sqlCommand.Parameters.Clear();

            //会社(クライアント)ID
            NpgsqlParameter paramKaishaId = new NpgsqlParameter(":targetKaishaid",NpgsqlDbType.Bigint); //Integer
            paramKaishaId.Value = clientEntity.ClientIdInputName;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramKaishaId);

            //会社名
            NpgsqlParameter paramKaishamei = new NpgsqlParameter(":targetKaishamei",NpgsqlDbType.Varchar);
            paramKaishamei.Value = clientEntity.ClientName;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramKaishamei);

            //部署名
            NpgsqlParameter paramBushomei = new NpgsqlParameter(":targetBushomei", NpgsqlDbType.Varchar);
            paramBushomei.Value = clientEntity.Department;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramBushomei);

            //郵便番号
            NpgsqlParameter paramPostnum = new NpgsqlParameter(":targetPostnum", NpgsqlDbType.Varchar);
            paramPostnum.Value = clientEntity.Post;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramPostnum);

            //住所
            NpgsqlParameter paramPostaddress = new NpgsqlParameter(":targetPostaddress",NpgsqlDbType.Varchar);
            paramPostaddress.Value = clientEntity.PostAddress;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramPostaddress);

            //TEL
            NpgsqlParameter paramTel = new NpgsqlParameter(":targetTel",NpgsqlDbType.Varchar);
            paramTel.Value = clientEntity.Tel;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramTel);

            //FAX
            NpgsqlParameter paramFax = new NpgsqlParameter(":targetFax", NpgsqlDbType.Varchar);
            paramFax.Value = clientEntity.Fax;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramFax);

            //E-mail
            NpgsqlParameter paramEmail = new NpgsqlParameter(":targetEmail",NpgsqlDbType.Varchar);
            paramEmail.Value = clientEntity.Email;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramEmail);

            //注文ID
            NpgsqlParameter paramOrderid = new NpgsqlParameter(":targetOrderid",NpgsqlDbType.Integer); //DoubleからIntegerへ修正した。
            paramOrderid.Value = clientEntity.OrderId;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderid);

            //コマンドの実行
            sqlCommand.ExecuteNonQuery();

            //クエリとパラメータをクリアする。
            sqlCommand.CommandText = "";
            sqlCommand.Parameters.Clear();
        }

        /// <summary>
        /// Ordersテーブルにデータを登録する。
        /// </summary>
        public void RegistOrders(OrdersEntity ordersEntity) //OrderInputEntity orderInputEntity OrderInputToDBEntity inputToDBEntity
        {
            //クエリを指定する。
            sqlCommand.CommandText = OrderInputDao.QUERY_REGIST_TO_ORDERS;

            //SQLパラメタのクリア
            sqlCommand.Parameters.Clear();

            //会社(クライアント)ID
            NpgsqlParameter paramKaishaId = new NpgsqlParameter(":targetKaishaid",NpgsqlDbType.Bigint); //Integer
            paramKaishaId.Value = ordersEntity.ClientIdInputName;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramKaishaId);

            //注文ID
            NpgsqlParameter paramOrderid = new NpgsqlParameter(":targetOrderid",NpgsqlDbType.Integer); //Double
            paramOrderid.Value = ordersEntity.OrderId;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderid);

            //注文日
            NpgsqlParameter paramOrderdate = new NpgsqlParameter(":targetOrderdate",NpgsqlDbType.Timestamp);
            paramOrderdate.Value = ordersEntity.OrderDate;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderdate);

            //納品予定日
            NpgsqlParameter paramDeliverydate = new NpgsqlParameter(":targetDeliverydate",NpgsqlDbType.Timestamp);
            paramDeliverydate.Value = ordersEntity.DeliveryDate;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramDeliverydate);

            //合計金額
            NpgsqlParameter paramTotal = new NpgsqlParameter(":targetTotal",NpgsqlDbType.Integer);  //Double
            paramTotal.Value = ordersEntity.Total;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramTotal);

            //購入品目1名 ordername1,orderprice1,ordernum1 :targetOrdername1,:targetOrderprice1,:targetOrdernum1
            NpgsqlParameter paramOrdername1 = new NpgsqlParameter(":targetOrdername1",NpgsqlDbType.Varchar);
            paramOrdername1.Value = ordersEntity.order1;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername1);

            //購入品目1の単価
            NpgsqlParameter paramOrderprice1 = new NpgsqlParameter(":targetOrderprice1", NpgsqlDbType.Integer);
            paramOrderprice1.Value = ordersEntity.order1price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice1);

            //購入品目1の注文数
            NpgsqlParameter paramOrdernum1 = new NpgsqlParameter(":targetOrdernum1",NpgsqlDbType.Integer);
            paramOrdernum1.Value = ordersEntity.order1Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum1);

            //購入品目2名
            NpgsqlParameter paramOrdername2 = new NpgsqlParameter(":targetOrdername2", NpgsqlDbType.Varchar);
            paramOrdername2.Value = ordersEntity.order2;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername2);

            //購入品目2の単価
            NpgsqlParameter paramOrderprice2 = new NpgsqlParameter(":targetOrderprice2", NpgsqlDbType.Integer);
            paramOrderprice2.Value = ordersEntity.order2price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice2);

            //購入品目2の注文数
            NpgsqlParameter paramOrdernum2 = new NpgsqlParameter(":targetOrdernum2", NpgsqlDbType.Integer);
            paramOrdernum2.Value = ordersEntity.order2Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum2);

            //購入品目3名
            NpgsqlParameter paramOrdername3 = new NpgsqlParameter(":targetOrdername3", NpgsqlDbType.Varchar);
            paramOrdername3.Value = ordersEntity.order3;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername3);

            //購入品目3の単価
            NpgsqlParameter paramOrderprice3 = new NpgsqlParameter(":targetOrderprice3", NpgsqlDbType.Integer);
            paramOrderprice3.Value = ordersEntity.order3price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice3);

            //購入品目3の注文数
            NpgsqlParameter paramOrdernum3 = new NpgsqlParameter(":targetOrdernum3", NpgsqlDbType.Integer);
            paramOrdernum3.Value = ordersEntity.order3Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum3);

            //購入品目4名
            NpgsqlParameter paramOrdername4 = new NpgsqlParameter(":targetOrdername4", NpgsqlDbType.Varchar);
            paramOrdername4.Value = ordersEntity.order4;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername4);

            //購入品目4の単価
            NpgsqlParameter paramOrderprice4 = new NpgsqlParameter(":targetOrderprice4", NpgsqlDbType.Integer);
            paramOrderprice4.Value = ordersEntity.order4price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice4);

            //購入品目4の注文数
            NpgsqlParameter paramOrdernum4 = new NpgsqlParameter(":targetOrdernum4", NpgsqlDbType.Integer);
            paramOrdernum4.Value = ordersEntity.order4Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum4);

            //購入品目5名
            NpgsqlParameter paramOrdername5 = new NpgsqlParameter(":targetOrdername5", NpgsqlDbType.Varchar);
            paramOrdername5.Value = ordersEntity.order5;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername5);

            //購入品目5の単価
            NpgsqlParameter paramOrderprice5 = new NpgsqlParameter(":targetOrderprice5", NpgsqlDbType.Integer);
            paramOrderprice5.Value = ordersEntity.order5price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice5);

            //購入品目5の注文数
            NpgsqlParameter paramOrdernum5 = new NpgsqlParameter(":targetOrdernum5", NpgsqlDbType.Integer);
            paramOrdernum5.Value = ordersEntity.order5Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum5);

            //コマンドの実行
            sqlCommand.ExecuteNonQuery();

            //クエリとパラメタをクリア
            sqlCommand.CommandText = "";
            sqlCommand.Parameters.Clear();

        }

        //////////////////////////////////【更新】////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Clientテーブルを更新するクエリを宣言する。
        /// </summary>
        private const string QUERY_UPDATE_CLIENT = "UPDATE public.client SET kaishaid = :targetKaishaid, kaishamei = :targetKaishamei, bushomei = :targetBushomei" +
            "postnum = :targetPostnum, postaddress = :targetPostaddress, tel = :targetTel, fax = :targetFax, email = :targetEmail, orderid = :targetOrderid where kaishaid = :targetKaishaid";

        /// <summary>
        /// Ordersテーブルを更新するクエリを宣言する。
        /// </summary>
        private const string QUERY_UPDATE_ORDERS = "UPDATE public.orders SET kaishaid = :targetKaishaid, orderid = :targetOrderid, orderdate = :targetOrderdate, deliverydate = :targetDeliverydate" +
            "total = :targetTotal, ordername1 = :targetOrdername1, orderprice1 = :targetOrderprice1, ordernum1 = :targetOrdernum1, ordername2 = :targetOrdername2, orderprice2 = :targetOrderprice2, ordernum2 = :targetOrdernum2" +
            ", ordername3 = :targetOrdername3, orderprice3 = :targetOrderprice3, ordernum3 = :targetOrdernum3, ordername4 = :targetOrdername4, orderprice4 = :targetOrderprice4, ordernum4 = :targetOrdernum4" +
            ", ordername5 = :targetOrdername5, orderprice5 = :targetOrderprice5, ordernum5 = :targetOrdernum5";


        /// <summary>
        /// 登録情報を更新する。
        /// </summary>
        public void Update(ClientEntity clientEntity, OrdersEntity ordersEntity)
        {
            UpdateClient(clientEntity);
            UpdateOrders(ordersEntity);
        }

        /// <summary>
        /// 登録したClientテーブルのレコードを更新する。
        /// </summary>
        /// <param name="clientEntity"></param>
        private void UpdateClient(ClientEntity clientEntity)
        {
            //クエリを指定する。
            sqlCommand.CommandText = OrderInputDao.QUERY_UPDATE_CLIENT;

            //SQLパラメタをクリアする。
            sqlCommand.Parameters.Clear();

            //会社(クライアント)ID
            NpgsqlParameter paramKaishaId = new NpgsqlParameter(":targetKaishaid", NpgsqlDbType.Bigint); //Integer
            paramKaishaId.Value = clientEntity.ClientIdInputName;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramKaishaId);

            //会社名
            NpgsqlParameter paramKaishamei = new NpgsqlParameter(":targetKaishamei", NpgsqlDbType.Varchar);
            paramKaishamei.Value = clientEntity.ClientName;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramKaishamei);

            //部署名
            NpgsqlParameter paramBushomei = new NpgsqlParameter(":targetBushomei", NpgsqlDbType.Varchar);
            paramBushomei.Value = clientEntity.Department;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramBushomei);

            //郵便番号
            NpgsqlParameter paramPostnum = new NpgsqlParameter(":targetPostnum", NpgsqlDbType.Varchar);
            paramPostnum.Value = clientEntity.Post;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramPostnum);

            //住所
            NpgsqlParameter paramPostaddress = new NpgsqlParameter(":targetPostaddress", NpgsqlDbType.Varchar);
            paramPostaddress.Value = clientEntity.PostAddress;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramPostaddress);

            //TEL
            NpgsqlParameter paramTel = new NpgsqlParameter(":targetTel", NpgsqlDbType.Varchar);
            paramTel.Value = clientEntity.Tel;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramTel);

            //FAX
            NpgsqlParameter paramFax = new NpgsqlParameter(":targetFax", NpgsqlDbType.Varchar);
            paramFax.Value = clientEntity.Fax;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramFax);

            //E-mail
            NpgsqlParameter paramEmail = new NpgsqlParameter(":targetEmail", NpgsqlDbType.Varchar);
            paramEmail.Value = clientEntity.Email;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramEmail);

            //注文ID
            NpgsqlParameter paramOrderid = new NpgsqlParameter(":targetOrderid", NpgsqlDbType.Integer); //DoubleからIntegerへ修正した。
            paramOrderid.Value = clientEntity.OrderId;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderid);

            //コマンドの実行
            sqlCommand.ExecuteNonQuery();

            //クエリとパラメータをクリアする。
            sqlCommand.CommandText = "";
            sqlCommand.Parameters.Clear();

        }

        /// <summary>
        /// 登録したOrdersテーブルのレコードを更新する。
        /// </summary>
        /// <param name="ordersEntity"></param>
        private void UpdateOrders(OrdersEntity ordersEntity) 
        {
            //クエリを指定する。
            sqlCommand.CommandText = OrderInputDao.QUERY_UPDATE_ORDERS;

            //SQLパラメタのクリア
            sqlCommand.Parameters.Clear();

            //会社(クライアント)ID
            NpgsqlParameter paramKaishaId = new NpgsqlParameter(":targetKaishaid", NpgsqlDbType.Bigint); //Integer
            paramKaishaId.Value = ordersEntity.ClientIdInputName;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramKaishaId);

            //注文ID
            NpgsqlParameter paramOrderid = new NpgsqlParameter(":targetOrderid", NpgsqlDbType.Integer); //Double
            paramOrderid.Value = ordersEntity.OrderId;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderid);

            //注文日
            NpgsqlParameter paramOrderdate = new NpgsqlParameter(":targetOrderdate", NpgsqlDbType.Timestamp);
            paramOrderdate.Value = ordersEntity.OrderDate;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderdate);

            //納品予定日
            NpgsqlParameter paramDeliverydate = new NpgsqlParameter(":targetDeliverydate", NpgsqlDbType.Timestamp);
            paramDeliverydate.Value = ordersEntity.DeliveryDate;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramDeliverydate);

            //合計金額
            NpgsqlParameter paramTotal = new NpgsqlParameter(":targetTotal", NpgsqlDbType.Integer);  //Double
            paramTotal.Value = ordersEntity.Total;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramTotal);

            //購入品目1名 ordername1,orderprice1,ordernum1 :targetOrdername1,:targetOrderprice1,:targetOrdernum1
            NpgsqlParameter paramOrdername1 = new NpgsqlParameter(":targetOrdername1", NpgsqlDbType.Varchar);
            paramOrdername1.Value = ordersEntity.order1;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername1);

            //購入品目1の単価
            NpgsqlParameter paramOrderprice1 = new NpgsqlParameter(":targetOrderprice1", NpgsqlDbType.Integer);
            paramOrderprice1.Value = ordersEntity.order1price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice1);

            //購入品目1の注文数
            NpgsqlParameter paramOrdernum1 = new NpgsqlParameter(":targetOrdernum1", NpgsqlDbType.Integer);
            paramOrdernum1.Value = ordersEntity.order1Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum1);

            //購入品目2名
            NpgsqlParameter paramOrdername2 = new NpgsqlParameter(":targetOrdername2", NpgsqlDbType.Varchar);
            paramOrdername2.Value = ordersEntity.order2;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername2);

            //購入品目2の単価
            NpgsqlParameter paramOrderprice2 = new NpgsqlParameter(":targetOrderprice2", NpgsqlDbType.Integer);
            paramOrderprice2.Value = ordersEntity.order2price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice2);

            //購入品目2の注文数
            NpgsqlParameter paramOrdernum2 = new NpgsqlParameter(":targetOrdernum2", NpgsqlDbType.Integer);
            paramOrdernum2.Value = ordersEntity.order2Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum2);

            //購入品目3名
            NpgsqlParameter paramOrdername3 = new NpgsqlParameter(":targetOrdername3", NpgsqlDbType.Varchar);
            paramOrdername3.Value = ordersEntity.order3;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername3);

            //購入品目3の単価
            NpgsqlParameter paramOrderprice3 = new NpgsqlParameter(":targetOrderprice3", NpgsqlDbType.Integer);
            paramOrderprice3.Value = ordersEntity.order3price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice3);

            //購入品目3の注文数
            NpgsqlParameter paramOrdernum3 = new NpgsqlParameter(":targetOrdernum3", NpgsqlDbType.Integer);
            paramOrdernum3.Value = ordersEntity.order3Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum3);

            //購入品目4名
            NpgsqlParameter paramOrdername4 = new NpgsqlParameter(":targetOrdername4", NpgsqlDbType.Varchar);
            paramOrdername4.Value = ordersEntity.order4;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername4);

            //購入品目4の単価
            NpgsqlParameter paramOrderprice4 = new NpgsqlParameter(":targetOrderprice4", NpgsqlDbType.Integer);
            paramOrderprice4.Value = ordersEntity.order4price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice4);

            //購入品目4の注文数
            NpgsqlParameter paramOrdernum4 = new NpgsqlParameter(":targetOrdernum4", NpgsqlDbType.Integer);
            paramOrdernum4.Value = ordersEntity.order4Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum4);

            //購入品目5名
            NpgsqlParameter paramOrdername5 = new NpgsqlParameter(":targetOrdername5", NpgsqlDbType.Varchar);
            paramOrdername5.Value = ordersEntity.order5;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdername5);

            //購入品目5の単価
            NpgsqlParameter paramOrderprice5 = new NpgsqlParameter(":targetOrderprice5", NpgsqlDbType.Integer);
            paramOrderprice5.Value = ordersEntity.order5price;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrderprice5);

            //購入品目5の注文数
            NpgsqlParameter paramOrdernum5 = new NpgsqlParameter(":targetOrdernum5", NpgsqlDbType.Integer);
            paramOrdernum5.Value = ordersEntity.order5Num;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramOrdernum5);

            //コマンドの実行
            sqlCommand.ExecuteNonQuery();

            //クエリとパラメタをクリア
            sqlCommand.CommandText = "";
            sqlCommand.Parameters.Clear();
        }

        //////////////////////////////////【削除】////////////////////////////////////////////////////////////////////

        //Ordersテーブルの該当レコードを削除するクエリ
        private const string QUERY_DELETE_ORDERS = "DELETE FROM orders WHERE kaishaid = :targetKaishaid;";

        //Clientテーブルの該当レコードを削除するクエリ
        private const string QUERY_DELETE_CLIENT = "DELETE FROM client WHERE kaishaid = :targetKaishaid;";

        /// <summary>
        /// 会社名IDを使ってレコードを削除する。
        /// </summary>
        public void Delete(OrderInputEntity orderInputEntity) 
        {
            //Ordersテーブルの該当レコードを削除する。
            DeleteOrders(orderInputEntity);
            //Clientテーブルの該当レコードを削除する。
            DeleteClient(orderInputEntity);
        }

        /// <summary>
        /// Ordersテーブルの削除対象レコードを削除する。
        /// </summary>
        private void DeleteOrders(OrderInputEntity orderInputEntity) 
        {
            //クエリを指定する。
            sqlCommand.CommandText = OrderInputDao.QUERY_DELETE_ORDERS;

            //SQLパラメタのクリア
            sqlCommand.Parameters.Clear();

            //会社ID
            NpgsqlParameter paramKaishaId = new NpgsqlParameter(":targetKaishaid",NpgsqlDbType.Bigint);
            paramKaishaId.Value = orderInputEntity.ClientIdInputName;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramKaishaId);

            //クエリを実行する。
            sqlCommand.ExecuteNonQuery();

            //クエリとパラメータをクリアする。
            sqlCommand.CommandText = "";
            sqlCommand.Parameters.Clear();
        }

        /// <summary>
        /// Clientテーブルの削除対象レコードを削除する。
        /// </summary>
        private void DeleteClient(OrderInputEntity orderInputEntity) 
        {
            //クエリを指定する。
            sqlCommand.CommandText = OrderInputDao.QUERY_DELETE_CLIENT;

            //SQLパラメタのクリア
            sqlCommand.Parameters.Clear();

            //会社ID
            NpgsqlParameter paramKaishaId = new NpgsqlParameter(":targetKaishaid", NpgsqlDbType.Bigint);
            paramKaishaId.Value = orderInputEntity.ClientIdInputName;
            //定義を追加する。
            sqlCommand.Parameters.Add(paramKaishaId);

            //クエリを実行する。
            sqlCommand.ExecuteNonQuery();

            //クエリとパラメータをクリアする。
            sqlCommand.CommandText = "";
            sqlCommand.Parameters.Clear();
        }
    }
}
