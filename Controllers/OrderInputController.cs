using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalesManagementSystem.Setting;
using SalesManagementSystem.Dto;
using SalesManagementSystem.Logic;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using NetOffice;
using ClosedXML;
using Spire.Xls;
using System.IO;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using Worksheet = Spire.Xls.Worksheet;
using Workbook = Spire.Xls.Workbook;
using PivotCache = Spire.Xls.PivotCache;
using PivotTable = Spire.Xls.PivotTable;

namespace SalesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderInputController : ControllerBase
    {
        /// <summary>
        /// 設定記述オブジェクト
        /// </summary>
        private readonly IOptions<DBSetting> options = null;

        public OrderInputController(IOptions<DBSetting> options) 
        {
            //設定記述子
            this.options = options;

        }

        //////////////////////////////////【読み取り】////////////////////////////////////////////////////////////////////

        /// <summary>
        /// テーブルに全情報を表示する処理。
        /// </summary>
        /// <returns></returns>
        [HttpPost("ListData")] //←【重要Point】[HttpPost("ListData")]と記述する。
        public List<OrderInputEntity> ListData() //public string JsonData()
        {
            List<OrderInputEntity> orderInputEntities = new List<OrderInputEntity>();
            OrderInputLogic logic = new OrderInputLogic(options);
            orderInputEntities = logic.ReadList();

            string jsonData = JsonConvert.SerializeObject(orderInputEntities);

            orderInputEntities = logic.ReadList();

            return orderInputEntities;
        }

        //////////////////////////////////【登録】////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 新規で登録する処理
        /// </summary>
        /// <param name="orderInputEntity"></param>
        [HttpPost("postTorokuData")]
        public void postTorokuData([FromBody]OrderInputEntity orderInputEntity) 
        {
            OrderInputToDBEntity inputToDBEntity = new OrderInputToDBEntity();

            ClientEntity clientEntity = new ClientEntity();
            clientEntity.ClientIdInputName = int.Parse(orderInputEntity.ClientIdInputName);
            clientEntity.ClientName = orderInputEntity.ClientName;
            clientEntity.Department = orderInputEntity.Department;
            clientEntity.Post = orderInputEntity.Post;
            clientEntity.PostAddress = orderInputEntity.PostAddress;
            clientEntity.Tel = orderInputEntity.Tel;
            clientEntity.Fax = orderInputEntity.Fax;
            clientEntity.Email = orderInputEntity.Email;
            clientEntity.OrderId = long.Parse(orderInputEntity.OrderId);

            OrdersEntity ordersEntity = new OrdersEntity();
            ordersEntity.ClientIdInputName = int.Parse(orderInputEntity.ClientIdInputName);
            ordersEntity.OrderId = long.Parse(orderInputEntity.OrderId);
            ordersEntity.OrderDate = DateTime.Parse(orderInputEntity.OrderDate);
            ordersEntity.DeliveryDate = DateTime.Parse(orderInputEntity.DeliveryDate);
            ordersEntity.Total = long.Parse(orderInputEntity.Total);
            ordersEntity.order1 = orderInputEntity.order1;
            ordersEntity.order1price = int.Parse(orderInputEntity.order1price);
            ordersEntity.order1Num = int.Parse(orderInputEntity.order1Num);
            ordersEntity.order2 = orderInputEntity.order2;
            ordersEntity.order2price = int.Parse(orderInputEntity.order2price);
            ordersEntity.order2Num = int.Parse(orderInputEntity.order2Num);
            ordersEntity.order3 = orderInputEntity.order3;
            ordersEntity.order3price = int.Parse(orderInputEntity.order3price);
            ordersEntity.order3Num = int.Parse(orderInputEntity.order3Num);
            ordersEntity.order4 = orderInputEntity.order4;
            ordersEntity.order4price = int.Parse(orderInputEntity.order4price);
            ordersEntity.order4Num = int.Parse(orderInputEntity.order4Num);
            ordersEntity.order5 = orderInputEntity.order5;
            ordersEntity.order5price = int.Parse(orderInputEntity.order5price);
            ordersEntity.order5Num = int.Parse(orderInputEntity.order5Num);

            OrderInputLogic logic = new OrderInputLogic(options);
            logic.InsertData(clientEntity,ordersEntity); //orderInputEntity inputToDBEntity
        }

        //////////////////////////////////【更新】////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 登録情報を更新する。
        /// </summary>
        [HttpPost("torokuDataUpdate")]
        public void torokuDataUpdate([FromBody] OrderInputEntity orderInputEntity) 
        {
            ClientEntity clientEntity = new ClientEntity();
            clientEntity.ClientIdInputName = int.Parse(orderInputEntity.ClientIdInputName);
            clientEntity.ClientName = orderInputEntity.ClientName;
            clientEntity.Department = orderInputEntity.Department;
            clientEntity.Post = orderInputEntity.Post;
            clientEntity.PostAddress = orderInputEntity.PostAddress;
            clientEntity.Tel = orderInputEntity.Tel;
            clientEntity.Fax = orderInputEntity.Fax;
            clientEntity.Email = orderInputEntity.Email;
            clientEntity.OrderId = long.Parse(orderInputEntity.OrderId);

            OrdersEntity ordersEntity = new OrdersEntity();
            ordersEntity.ClientIdInputName = int.Parse(orderInputEntity.ClientIdInputName);
            ordersEntity.OrderId = long.Parse(orderInputEntity.OrderId);
            ordersEntity.OrderDate = DateTime.Parse(orderInputEntity.OrderDate);
            ordersEntity.DeliveryDate = DateTime.Parse(orderInputEntity.DeliveryDate);
            ordersEntity.Total = long.Parse(orderInputEntity.Total);
            ordersEntity.order1 = orderInputEntity.order1;
            ordersEntity.order1price = int.Parse(orderInputEntity.order1price);
            ordersEntity.order1Num = int.Parse(orderInputEntity.order1Num);
            ordersEntity.order2 = orderInputEntity.order2;
            ordersEntity.order2price = int.Parse(orderInputEntity.order2price);
            ordersEntity.order2Num = int.Parse(orderInputEntity.order2Num);
            ordersEntity.order3 = orderInputEntity.order3;
            ordersEntity.order3price = int.Parse(orderInputEntity.order3price);
            ordersEntity.order3Num = int.Parse(orderInputEntity.order3Num);
            ordersEntity.order4 = orderInputEntity.order4;
            ordersEntity.order4price = int.Parse(orderInputEntity.order4price);
            ordersEntity.order4Num = int.Parse(orderInputEntity.order4Num);
            ordersEntity.order5 = orderInputEntity.order5;
            ordersEntity.order5price = int.Parse(orderInputEntity.order5price);
            ordersEntity.order5Num = int.Parse(orderInputEntity.order5Num);

            OrderInputLogic logic = new OrderInputLogic(options);
            logic.UpdateData(orderInputEntity,clientEntity, ordersEntity);
            
        }

        //////////////////////////////////【削除】///////////////////////////////////////////////////////////////////////
        [HttpPost("torokuDataDelete")]
        public void torokuDataDelete([FromBody] OrderInputEntity orderInputEntity) 
        {
            OrderInputLogic logic = new OrderInputLogic(options);
            logic.DeleteData(orderInputEntity);
        }

        //////////////////////////////////【売上表をエクセル出力】////////////////////////////////////////////////////////

        /// <summary>
        /// 売上表をエクセル出力する処理。
        /// </summary>
        [HttpPost("CreateSalesList")]
        public void CreateSalesList() 
        {
            //EnvironmentクラスのGetFolderPathメソッドを使ってデスクトップ(Desktop)の絶対パスを取得する。
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"売り上げ実績2.xlsx");

            //Excelファイル作成
            using (var book = new XLWorkbook(XLEventTracking.Disabled)) 
            {
                //シート名を「売上実績」に設定する。
                var sheet = book.AddWorksheet("売上実績");
                sheet.Cell("A1").Value = "会社名"; //【ヘッダー】会社名
                //sheet.Range("A1:A2").Merge(); //【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。
                sheet.Range("A1:A2").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("B1").Value = "合計金額"; //【ヘッダー】合計金額
                sheet.Range("B1:B2").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                
                sheet.Cell("C1").Value = "購入品目名1"; //【ヘッダー】購入品目名1
                sheet.Range("C1:E1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("C2").Value = "品目名"; //【ヘッダー】品目名
                sheet.Cell("C2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("D2").Value = "価格"; //【ヘッダー】価格
                sheet.Cell("D2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("E2").Value = "注文数"; //【ヘッダー】注文数
                sheet.Cell("E2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                sheet.Cell("F1").Value = "購入品目名2"; //【ヘッダー】購入品目名2
                sheet.Range("F1:H1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("F2").Value = "品目名"; //【ヘッダー】品目名
                sheet.Cell("F2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("G2").Value = "価格"; //【ヘッダー】価格
                sheet.Cell("G2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("H2").Value = "注文数"; //【ヘッダー】注文数
                sheet.Cell("H2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                sheet.Cell("I1").Value = "購入品目名3"; //【ヘッダー】購入品目名3
                sheet.Range("I1:K1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("I2").Value = "品目名"; //【ヘッダー】品目名
                sheet.Cell("I2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("J2").Value = "価格"; //【ヘッダー】価格
                sheet.Cell("J2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("K2").Value = "注文数"; //【ヘッダー】注文数
                sheet.Cell("K2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                sheet.Cell("L1").Value = "購入品目名4"; //【ヘッダー】購入品目名4
                sheet.Range("L1:N1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("L2").Value = "品目名"; //【ヘッダー】品目名
                sheet.Cell("L2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("M2").Value = "価格"; //【ヘッダー】価格
                sheet.Cell("M2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("N2").Value = "注文数"; //【ヘッダー】注文数
                sheet.Cell("N2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                sheet.Cell("O1").Value = "購入品目名5"; //【ヘッダー】購入品目名5
                sheet.Range("O1:Q1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("O2").Value = "品目名"; //【ヘッダー】品目名
                sheet.Cell("O2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("P2").Value = "価格"; //【ヘッダー】価格
                sheet.Cell("P2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("Q2").Value = "注文数"; //【ヘッダー】注文数
                sheet.Cell("Q2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                List<OrderInputEntity> salesList = new List<OrderInputEntity>();

                salesList = ListData();

                Console.WriteLine(salesList);

                //3行目
                for (int i=0; i<salesList.Count;i++) 
                {
                    sheet.Cell(i+3, 1).Value = salesList[i].ClientName;
                    sheet.Cell(i+3, 2).Value = salesList[i].Total;
                    sheet.Cell(i+3, 3).Value = salesList[i].order1;
                    sheet.Cell(i+3, 4).Value = salesList[i].order1price;
                    sheet.Cell(i+3, 5).Value = salesList[i].order1Num;
                    sheet.Cell(i+3, 6).Value = salesList[i].order2;
                    sheet.Cell(i+3, 7).Value = salesList[i].order2price;
                    sheet.Cell(i+3, 8).Value = salesList[i].order2Num;
                    sheet.Cell(i+3, 9).Value = salesList[i].order3;
                    sheet.Cell(i+3, 10).Value = salesList[i].order3price;
                    sheet.Cell(i+3, 11).Value = salesList[i].order3Num;
                    sheet.Cell(i+3, 12).Value = salesList[i].order4;
                    sheet.Cell(i+3, 13).Value = salesList[i].order4price;
                    sheet.Cell(i+3, 14).Value = salesList[i].order4Num;
                    sheet.Cell(i+3, 15).Value = salesList[i].order5;
                    sheet.Cell(i+3, 16).Value = salesList[i].order5price;
                    sheet.Cell(i+3, 17).Value = salesList[i].order5Num;
                }

                book.SaveAs(path);
            }

        }

        //////////////////////////////////【ピボットテーブル作成】////////////////////////////////////////////////////////
        [HttpPost("CreatePivotTable")]
        public void CreatePivotTable() 
        {
            // ----------------【ファイルを読み込む】----------------
            //EnvironmentクラスのGetFolderPathメソッドを使ってデスクトップ(Desktop)の絶対パスを取得する。
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "売り上げ実績2.xlsx");
            Workbook workbook = new Workbook();
            //ディレクトリにあるファイルを読み込む。
            workbook.LoadFromFile(path);
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Data Source";
            Worksheet sheet2 = workbook.CreateEmptySheet();
            sheet2.Name = "Pivot Table";

            // ----------------【ピボットテーブルを作成する】----------------
            CellRange dataRange = sheet.Range["A2:Q4"];
            PivotCache cache = workbook.PivotCaches.Add(dataRange);
            PivotTable pt = sheet2.PivotTables.Add("Pivot Table",sheet.Range["A2"],cache);

            // ----------------【行ラベルを定義する】----------------
            var r1 = pt.PivotFields["会社名"];
            r1.Axis = AxisTypes.Row;
            pt.Options.RowHeaderCaption = "会社名";
            
            pt.DataFields.Add(pt.PivotFields["合計金額"], "合計金額", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名1の価格"], "合計/購入品目名1の価格", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名1の注文数"], "合計/購入品目名1の注文数", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名2の価格"], "合計/購入品目名2の価格", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名2の注文数"], "合計/購入品目名2の注文数", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名3の価格"], "合計/購入品目名3の価格", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名3の注文数"], "合計/購入品目名3の注文数", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名4の価格"], "合計/購入品目名4の価格", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名4の注文数"], "合計/購入品目名4の注文数", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名5の価格"], "合計/購入品目名5の価格", SubtotalTypes.Sum);
            pt.DataFields.Add(pt.PivotFields["購入品目名5の注文数"], "合計/購入品目名5の注文数", SubtotalTypes.Sum);
            
            pt.BuiltInStyle = PivotBuiltInStyles.PivotStyleMedium12;

            //下記のディレクトリに保存する。
            workbook.SaveToFile("C:\\Users\\Owner\\Desktop\\PivotTable.xlsx", ExcelVersion.Version2010);
            
        }




        //////////////////////////////////【ピボットテーブル用に売上表を加工する】////////////////////////////////////////////////////////

        /// <summary>
        /// ピボットテーブル用に売上表を加工する処理。
        /// </summary>
        [HttpPost("CreateSalesListForPivotTable")]
        public void CreateSalesListForPivotTable()
        {
            //EnvironmentクラスのGetFolderPathメソッドを使ってデスクトップ(Desktop)の絶対パスを取得する。
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "売り上げ実績2.xlsx");

            //Excelファイル作成
            using (var book = new XLWorkbook(XLEventTracking.Disabled))
            {
                //シート名を「売上実績」に設定する。
                var sheet = book.AddWorksheet("売上実績PivotTable");
                sheet.Cell("A2").Value = "会社名"; //【ヘッダー】会社名
                //sheet.Range("A1:A2").Merge(); //【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。
                //sheet.Range("A1:A2").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; 
                sheet.Cell("B2").Value = "合計金額"; //【ヘッダー】合計金額
                //sheet.Range("B1:B2").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; 

                sheet.Cell("C1").Value = "購入品目名1"; //【ヘッダー】購入品目名1
                sheet.Range("C1:E1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("C2").Value = "購入品目名1"; //【ヘッダー】品目名
                sheet.Cell("C2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("D2").Value = "購入品目名1の価格"; //【ヘッダー】価格
                sheet.Cell("D2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("E2").Value = "購入品目名1の注文数"; //【ヘッダー】注文数
                sheet.Cell("E2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                sheet.Cell("F1").Value = "購入品目名2"; //【ヘッダー】購入品目名2
                sheet.Range("F1:H1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("F2").Value = "購入品目名2"; //【ヘッダー】品目名
                sheet.Cell("F2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("G2").Value = "購入品目名2の価格"; //【ヘッダー】価格
                sheet.Cell("G2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("H2").Value = "購入品目名2の注文数"; //【ヘッダー】注文数
                sheet.Cell("H2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                sheet.Cell("I1").Value = "購入品目名3"; //【ヘッダー】購入品目名3
                sheet.Range("I1:K1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("I2").Value = "購入品目名3"; //【ヘッダー】品目名
                sheet.Cell("I2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("J2").Value = "購入品目名3の価格"; //【ヘッダー】価格
                sheet.Cell("J2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("K2").Value = "購入品目名3の注文数"; //【ヘッダー】注文数
                sheet.Cell("K2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                sheet.Cell("L1").Value = "購入品目名4"; //【ヘッダー】購入品目名4
                sheet.Range("L1:N1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("L2").Value = "購入品目名4"; //【ヘッダー】品目名
                sheet.Cell("L2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("M2").Value = "購入品目名4の価格"; //【ヘッダー】価格
                sheet.Cell("M2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("N2").Value = "購入品目名4の注文数"; //【ヘッダー】注文数
                sheet.Cell("N2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                sheet.Cell("O1").Value = "購入品目名5"; //【ヘッダー】購入品目名5
                sheet.Range("O1:Q1").Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;//【重要ポイント】セル範囲に使う「A1:A2」はコロン間の間隔を空けないこと。中央寄せに設定する。
                sheet.Cell("O2").Value = "購入品目名5"; //【ヘッダー】品目名
                sheet.Cell("O2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("P2").Value = "購入品目名5の価格"; //【ヘッダー】価格
                sheet.Cell("P2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell("Q2").Value = "購入品目名5の注文数"; //【ヘッダー】注文数
                sheet.Cell("Q2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                List<OrderInputEntity> salesList = new List<OrderInputEntity>();

                salesList = ListData();

                Console.WriteLine(salesList);

                //3行目
                for (int i = 0; i < salesList.Count; i++)
                {
                    sheet.Cell(i + 3, 1).Value = salesList[i].ClientName;
                    sheet.Cell(i + 3, 2).Value = salesList[i].Total;
                    sheet.Cell(i + 3, 3).Value = salesList[i].order1;
                    sheet.Cell(i + 3, 4).Value = salesList[i].order1price;
                    sheet.Cell(i + 3, 5).Value = salesList[i].order1Num;
                    sheet.Cell(i + 3, 6).Value = salesList[i].order2;
                    sheet.Cell(i + 3, 7).Value = salesList[i].order2price;
                    sheet.Cell(i + 3, 8).Value = salesList[i].order2Num;
                    sheet.Cell(i + 3, 9).Value = salesList[i].order3;
                    sheet.Cell(i + 3, 10).Value = salesList[i].order3price;
                    sheet.Cell(i + 3, 11).Value = salesList[i].order3Num;
                    sheet.Cell(i + 3, 12).Value = salesList[i].order4;
                    sheet.Cell(i + 3, 13).Value = salesList[i].order4price;
                    sheet.Cell(i + 3, 14).Value = salesList[i].order4Num;
                    sheet.Cell(i + 3, 15).Value = salesList[i].order5;
                    sheet.Cell(i + 3, 16).Value = salesList[i].order5price;
                    sheet.Cell(i + 3, 17).Value = salesList[i].order5Num;
                }

                book.SaveAs(path);
            }

        }


    }
}
