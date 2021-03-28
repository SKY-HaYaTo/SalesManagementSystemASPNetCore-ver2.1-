using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagementSystem.Dto
{
    /// <summary>
    /// OrderInputEntity.csの各プロパティの型をDB登録前に変換する。
    /// </summary>
    public class OrderInputToDBEntity
    {
        public int ClientIdInputName { get; set; } //クライアントID int
        public string ClientName { get; set; } //会社名
        public string Department { get; set; } //部署名
        public string Post { get; set; } //郵便番号
        public string PostAddress { get; set; } //住所
        public string Tel { get; set; } //TEL
        public string Fax { get; set; } //Fax
        public string Email { get; set; } //email
        public long OrderId { get; set; } //注文ID long

        public DateTime OrderDate { get; set; } //発注日 DateTime
        public DateTime DeliveryDate { get; set; } //納品予定日 DateTime
        public long Total { get; set; } //合計金額 long
        public string order1 { get; set; } //購入品目1
        public int order1price { get; set; } //購入品目1の単価 int
        public int order1Num { get; set; } //購入品目1の注文数 int
        public string order2 { get; set; } //購入品目2
        public int order2price { get; set; } //購入品目2の単価 int
        public int order2Num { get; set; } //購入品目2の注文数 int
        public string order3 { get; set; } //購入品目3
        public int order3price { get; set; } //購入品目3の単価 int
        public int order3Num { get; set; } //購入品目3の注文数 int
        public string order4 { get; set; } //購入品目4
        public int order4price { get; set; } //購入品目4の単価 int
        public int order4Num { get; set; } //購入品目4の注文数 int
        public string order5 { get; set; } //購入品目5
        public int order5price { get; set; } //購入品目5の単価 int
        public int order5Num { get; set; } //購入品目5の注文数 int
    }
}
