
$(document).ready(function () {

    //確認画面から戻ってきたときの処理
    var localData = localStorage.getItem('form_data'); //ローカルストレージから値を取得する。
    localData = JSON.parse(localData); //ローカルストレージから取得した値をオブジェクトデータに戻す。
    if (localData !== null) {
        for (var index in localData) {
            var data = localData[index];
            var formName = data['name'];
            var formVal = data['value'];
            //もう一度値をセットする。
            $('[name=' + formName + ']').val(formVal);
        }
    }

    //確認画面に進むボタンをクリックしたときの処理
    $("#confirmBtn").click(function () {

        //Object変数sendDataに値を格納する。
        var sendData = {
            clientIdData : $("#clientIdInput").val(), //クライアントID
            clientNameData : $("#clientNameInput").val(), //クライアント名
            departmentData : $("#departmentInput").val(), //部署名
            postData : $("#postInput").val(), //郵便番号
            postAddressData : $("#postAddressInput").val(), //住所
            telData : $("#telInput").val(), //Tel
            faxData : $("#faxInput").val(), //Fax
            emailData : $("#emailInput").val(), //E-mail
            order1Data : $("#order1Input").val(),//購入品目1
            order1priceData : $("#order1priceInput").val(),//購入品目1の単価
            order1NumData : $("#order1NumInput").val(), //購入品目1の注文数
            order2Data : $("#order2Input").val(),//購入品目2
            order2priceData : $("#order2priceInput").val(),//購入品目2の単価
            order2NumData : $("#order2NumInput").val(), //購入品目2の注文数
            order3Data : $("#order3Input").val(),//購入品目3
            order3priceData : $("#order3priceInput").val(),//購入品目3の単価
            order3NumData : $("#order3NumInput").val(), //購入品目3の注文数
            order4Data : $("#order4Input").val(),//購入品目4
            order4priceData : $("#order4priceInput").val(),//購入品目4の単価
            order4NumData : $("#order4NumInput").val(), //購入品目4の注文数
            order5Data : $("#order5Input").val(),//購入品目5
            order5priceData : $("#order5priceInput").val(),//購入品目5の単価
            order5NumData : $("#order5NumInput").val(), //購入品目5の注文数
            orderDateData : $("#orderDateInput").val(), //発注日
            deliveryDateData : $("#deliveryDateInput").val(), //納品予定日
            totalData: $("#totalInput").val()//合計金額
        };

        var formJson = JSON.stringify(sendData); //JSON.stringiftメソッドでObject変数sendDataをJSON化する。
        localStorage.setItem('form_data',formJson); //JSONデータをlocalStorageに保存する。

        location.href = "OrderInputConfirm.html"; //OrderInputConfirm.htmlに遷移する。

    })

});









