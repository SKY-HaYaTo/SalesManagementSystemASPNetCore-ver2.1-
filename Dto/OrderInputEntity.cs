using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagementSystem.Dto
{
    /// <summary>
    /// HTTP Requestパラメータをモデルバインディングするプロパティと
    /// DBカラム名を宣言する。
    /// </summary>
    public class OrderInputEntity
    {
        //【★重要ポイント★】jsでJson.stringifyで文字列にしているのですべてString型にする。
        public string ClientIdInputName { get; set; } //クライアントID int
        public string ClientName { get; set; } //会社名
        public string Department { get; set; } //部署名
        public string Post { get; set; } //郵便番号
        public string PostAddress { get; set; } //住所
        public string Tel { get; set; } //TEL
        public string Fax { get; set; } //Fax
        public string Email { get; set; } //email
        public string OrderId { get; set; } //注文ID long
        public string OrderDate { get; set; } //発注日 DateTime
        public string DeliveryDate { get; set; } //納品予定日 DateTime
        public string Total { get; set; } //合計金額 long
        public string order1 { get; set; } //購入品目1
        public string order1price { get; set; } //購入品目1の単価 int
        public string order1Num { get; set; } //購入品目1の注文数 int
        public string order2 { get; set; } //購入品目2
        public string order2price { get; set; } //購入品目2の単価 int
        public string order2Num { get; set; } //購入品目2の注文数 int
        public string order3 { get; set; } //購入品目3
        public string order3price { get; set; } //購入品目3の単価 int
        public string order3Num { get; set; } //購入品目3の注文数 int
        public string order4 { get; set; } //購入品目4
        public string order4price { get; set; } //購入品目4の単価 int
        public string order4Num { get; set; } //購入品目4の注文数 int
        public string order5 { get; set; } //購入品目5
        public string order5price { get; set; } //購入品目5の単価 int
        public string order5Num { get; set; } //購入品目5の注文数 int

        //DBのカラム名と同じにする。
        /// <summary>
        /// Clientテーブルのカラム名：会社ID
        /// </summary>
        public const string COL_NAME_KAISHAID = "kaishaid";

        /// <summary>
        /// Clientテーブルのカラム名：会社名
        /// </summary>
        public const string COL_NAME_KAISHAMEI = "kaishamei";

        /// <summary>
        /// Clientテーブルのカラム名：部署名
        /// </summary>
        public const string COL_NAME_BUSHOMEI = "bushomei";

        /// <summary>
        /// Clientテーブルのカラム名：郵便番号
        /// </summary>
        public const string COL_NAME_POSTNUM = "postnum";

        /// <summary>
        /// Clientテーブルのカラム名：住所
        /// </summary>
        public const string COL_NAME_POSTADDRESS = "postaddress";

        /// <summary>
        /// Clientテーブルのカラム名：TEL
        /// </summary>
        public const string COL_NAME_TEL = "tel";

        /// <summary>
        /// Clientテーブルのカラム名：FAX
        /// </summary>
        public const string COL_NAME_FAX = "fax";

        /// <summary>
        /// Clientテーブルのカラム名：EMAIL
        /// </summary>
        public const string COL_NAME_EMAIL = "email";

        /// <summary>
        /// Clientテーブルのカラム名：ORDERID
        /// </summary>
        public const string COL_NAME_ORDERID = "orderid";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERDATE
        /// </summary>
        public const string COL_NAME_ORDERDATE = "orderdate";

        /// <summary>
        /// Ordersテーブルのカラム名：DELIVERYDATE
        /// </summary>
        public const string COL_NAME_DELIVERYDATE = "deliverydate";

        /// <summary>
        /// Ordersテーブルのカラム名：TOTAL
        /// </summary>
        public const string COL_NAME_TOTAL = "total";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNAME1
        /// </summary>
        public const string COL_NAME_ORDERNAME1 = "ordername1";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERPRICE1
        /// </summary>
        public const string COL_NAME_ORDERPRICE1 = "orderprice1";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNUM1
        /// </summary>
        public const string COL_NAME_ORDERNUM1 = "ordernum1";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNAME2
        /// </summary>
        public const string COL_NAME_ORDERNAME2 = "ordername2";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERPRICE2
        /// </summary>
        public const string COL_NAME_ORDERPRICE2 = "orderprice2";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNUM2
        /// </summary>
        public const string COL_NAME_ORDERNUM2 = "ordernum2";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNAME3
        /// </summary>
        public const string COL_NAME_ORDERNAME3 = "ordername3";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERPRICE3
        /// </summary>
        public const string COL_NAME_ORDERPRICE3 = "orderprice3";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNUM3
        /// </summary>
        public const string COL_NAME_ORDERNUM3 = "ordernum3";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNAME4
        /// </summary>
        public const string COL_NAME_ORDERNAME4 = "ordername4";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERPRICE4
        /// </summary>
        public const string COL_NAME_ORDERPRICE4 = "orderprice4";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNUM4
        /// </summary>
        public const string COL_NAME_ORDERNUM4 = "ordernum4";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNAME5
        /// </summary>
        public const string COL_NAME_ORDERNAME5 = "ordername5";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERPRICE5
        /// </summary>
        public const string COL_NAME_ORDERPRICE5 = "orderprice5";

        /// <summary>
        /// Ordersテーブルのカラム名：ORDERNUM5
        /// </summary>
        public const string COL_NAME_ORDERNUM5 = "ordernum5";
    }
}
