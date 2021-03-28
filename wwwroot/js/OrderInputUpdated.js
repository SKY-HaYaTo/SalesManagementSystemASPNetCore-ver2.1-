
$(document).ready(function () {

    //更新ボタンをクリックして、遷移時、ローカルストレージから値を取り出し各オブジェクトに挿入する。
    $("#clientNameInput").val(localStorage.getItem("key1"));
    $("#totalInput").val(localStorage.getItem("key2"));
    $("#order1Input").val(localStorage.getItem("key3"));
    $("#order1priceInput").val(localStorage.getItem("key4"));
    $("#order1NumInput").val(localStorage.getItem("key5"));
    $("#order2Input").val(localStorage.getItem("key6"));
    $("#order2priceInput").val(localStorage.getItem("key7"));
    $("#order2NumInput").val(localStorage.getItem("key8"));
    $("#order3Input").val(localStorage.getItem("key9"));
    $("#order3priceInput").val(localStorage.getItem("key10"));
    $("#order3NumInput").val(localStorage.getItem("key11"));
    $("#order4Input").val(localStorage.getItem("key12"));
    $("#order4priceInput").val(localStorage.getItem("key13"));
    $("#order4NumInput").val(localStorage.getItem("key14"));
    $("#order5Input").val(localStorage.getItem("key15"));
    $("#order5priceInput").val(localStorage.getItem("key16"));
    $("#order5NumInput").val(localStorage.getItem("key17"));
    $("#clientIdInput").val(localStorage.getItem("key18"));
    $("#orderIdInput").val(localStorage.getItem("key19")); 
    $("#departmentInput").val(localStorage.getItem("key20"));
    $("#postInput").val(localStorage.getItem("key21"));
    $("#postAddressInput").val(localStorage.getItem("key22"));
    $("#telInput").val(localStorage.getItem("key23"));
    $("#faxInput").val(localStorage.getItem("key24"));
    $("#emailInput").val(localStorage.getItem("key25"));
    $("#orderDateInput").val(localStorage.getItem("key26"));
    $("#deliveryDateInput").val(localStorage.getItem("key27"));

    localStorage.removeItem();

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
            clientIdData: $("#clientIdInput").val(), //クライアントID
            clientNameData: $("#clientNameInput").val(), //クライアント名
            departmentData: $("#departmentInput").val(), //部署名
            postData: $("#postInput").val(), //郵便番号
            postAddressData: $("#postAddressInput").val(), //住所
            telData: $("#telInput").val(), //Tel
            faxData: $("#faxInput").val(), //Fax
            emailData: $("#emailInput").val(), //E-mail
            order1Data: $("#order1Input").val(),//購入品目1
            order1priceData: $("#order1priceInput").val(),//購入品目1の単価
            order1NumData: $("#order1NumInput").val(), //購入品目1の注文数
            order2Data: $("#order2Input").val(),//購入品目2
            order2priceData: $("#order2priceInput").val(),//購入品目2の単価
            order2NumData: $("#order2NumInput").val(), //購入品目2の注文数
            order3Data: $("#order3Input").val(),//購入品目3
            order3priceData: $("#order3priceInput").val(),//購入品目3の単価
            order3NumData: $("#order3NumInput").val(), //購入品目3の注文数
            order4Data: $("#order4Input").val(),//購入品目4
            order4priceData: $("#order4priceInput").val(),//購入品目4の単価
            order4NumData: $("#order4NumInput").val(), //購入品目4の注文数
            order5Data: $("#order5Input").val(),//購入品目5
            order5priceData: $("#order5priceInput").val(),//購入品目5の単価
            order5NumData: $("#order5NumInput").val(), //購入品目5の注文数
            orderDateData: $("#orderDateInput").val(), //発注日
            deliveryDateData: $("#deliveryDateInput").val(), //納品予定日
            totalData: $("#totalInput").val()//合計金額
        };

        var formJson = JSON.stringify(sendData); //JSON.stringiftメソッドでObject変数sendDataをJSON化する。
        localStorage.setItem('form_data', formJson); //JSONデータをlocalStorageに保存する。

        location.href = "OrderInputConfirm.html"; //OrderInputConfirm.htmlに遷移する。

    })

});









