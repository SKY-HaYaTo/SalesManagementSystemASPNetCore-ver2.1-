$(document).ready(function () {

    var localData = localStorage.getItem('form_data');//localStorageに'form_data'で保存していたデータを取得する。
    localData = JSON.parse(localData); //localStorageから取得したJSONデータをオブジェクトに戻す。

    //今日の日付を取得する。
    var dt = new Date();
    var y = dt.getFullYear();
    var m = ("00" + (dt.getMonth() + 1)).slice(-2);
    var d = ("00" + dt.getDate()).slice(-2);
    var today = y + m + d;

    //ローカルストレージにデータがなければ null が返ってくるので、nullで分岐
    if (localData !== null) {
        $("#clientIdInput").val(localData.clientIdData); //
        $("#clientNameInput").val(localData.clientNameData); //
        $("#departmentInput").val(localData.departmentData); //
        $("#postInput").val(localData.postData); //
        $("#postAddressInput").val(localData.postAddressData); //
        $("#telInput").val(localData.telData); //
        $("#faxInput").val(localData.faxData); //
        $("#emailInput").val(localData.emailData); //

        $("#order1Input").val(localData.order1Data); //
        $("#order1priceInput").val(localData.order1priceData); //
        $("#order1NumInput").val(localData.order1NumData); //

        $("#order2Input").val(localData.order2Data); //
        $("#order2priceInput").val(localData.order2priceData); //
        $("#order2NumInput").val(localData.order2NumData); //

        $("#order3Input").val(localData.order3Data); //
        $("#order3priceInput").val(localData.order3priceData); //
        $("#order3NumInput").val(localData.order3NumData); //

        $("#order4Input").val(localData.order4Data); //
        $("#order4priceInput").val(localData.order4priceData); //
        $("#order4NumInput").val(localData.order4NumData); //

        $("#order5Input").val(localData.order5Data); //
        $("#order5priceInput").val(localData.order5priceData); //
        $("#order5NumInput").val(localData.order5NumData); //

        $("#orderDateInput").val(localData.orderDateData);
        $("#deliveryDateInput").val(localData.deliveryDateData);
        $("#totalInput").val(localData.totalData);
        $("#orderIdInput").val(localData.clientIdData + today);//注文ID：クライアントIDと今日の日付を連結

        //一連の処理が終わったタイミングなどで、ローカルストレージの情報を削除する。
        localStorage.removeItem('form_data');
    }

    //前画面に戻る時の処理
    $("#backToPrePageBtn").click(function () {
        backToPrePageSetLocalStorage();
    })

    //DB登録処理
    $("#registrationBtn").click(function () {
        torokuToDb();
    })

});

//前画面に戻る処理
function backToPrePageSetLocalStorage() {
    //フォームの内容をJSONデータで一括取得する。
    var form = $(".orderFormClass");
    var formData = form.serializeArray(form); //フォームの情報をオブジェクト化する。
    var formJson = JSON.stringify(formData); //オブジェクトデータをJSONデータに変換する。

    //localStirageを使って保存する。
    localStorage.setItem('form_data', formJson);
    location.href = "OrderInput.html"; //OrderInput.htmlへ遷移する。
}

//DBに登録する処理
function torokuToDb() {
    //フォームのデータをJsonデータで一括取得する。
    var form = $(".orderFormClass");

    //配列オブジェクトobjectを宣言する。
    var object = {};

    //フォームの内容を取得する。
    $(form.serializeArray()).each(function (i, v) {
        object[v.name] = v.value;
    })

    var jsonData = JSON.stringify(object);
    console.log(jsonData);
    

    //POST通信を実行する。
    $.ajax({
        url: "../api/OrderInput/postTorokuData",
        type: "POST",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert("OK通信");
        },
        error: function (xhr) {
            alert(xhr.status);
        }
    });

}