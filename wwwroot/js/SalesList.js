$(document).ready(function () {

    $.ajax({
        url: "../api/OrderInput/ListData",
        type: "Post",
        contentType: "application/json",
        success: function (data) {

            console.log(data);
            console.log(data.length);

            //連想配列に変換
            //var jsonList = JSON.parse(data);
            //console.log(jsonList.length);
            //console.log(jsonList);

            for (var i = 0; i < data.length; i++) {
                // テーブルの行を jsonList行分追加する
                var tr = document.createElement('tr');

                $("#tbodyId").append("<tr><td>" + '<input class="detailBtnClass" type="button" id="detailBtn" value="詳細">' + "</td>" +
                    "<td>" + '<input class="updateBtnClass" type="button" id="updateBtn" value="更新">' + "</td>" +
                    "<td>" + '<input class="deleteBtnClass" type="button" id="deleteBtn" value="削除">' + "</td>" +
                    "<td id=clientNameID" + i + ">" + data[i].clientName + "</td>" + 
                    "<td id=totalID" + i +">" + data[i].total + "</td>" +
                    "<td id=order1ID" + i +">" + data[i].order1 + "</td>" +
                    "<td id=order1priceID" + i +">" + data[i].order1price + "</td>" +
                    "<td id=order1NumID" + i +">" + data[i].order1Num + "</td>" +
                    "<td id=order2ID" + i +">" + data[i].order2 + "</td>" +
                    "<td id=order2priceID" + i +">" + data[i].order2price + "</td>" +
                    "<td id=order2NumID" + i +">" + data[i].order2Num + "</td>" +
                    "<td id=order3ID" + i +">" + data[i].order3 + "</td>" +
                    "<td id=order3priceID" + i +">" + data[i].order3price + "</td>" +
                    "<td id=order3NumID" + i +">" + data[i].order3Num + "</td>" +
                    "<td id=order4ID" + i +">" + data[i].order4 + "</td>" +
                    "<td id=order4priceID" + i +">" + data[i].order4price + "</td>" +
                    "<td id=order4NumID" + i +">" + data[i].order4Num + "</td>" +
                    "<td id=order5ID" + i +">" + data[i].order5 + "</td>" +
                    "<td id=order5priceID" + i +">" + data[i].order5price + "</td>" +
                    "<td id=order5NumID" + i +">" + data[i].order5Num + "</td>" +
                    "<td hidden id=clientIdInputNameID" + i +">" + data[i].clientIdInputName + "</td>" +
                    "<td hidden id=orderID" + i +">" + data[i].orderId + "</td>" +
                    "<td hidden id=departmentID" + i +">" + data[i].department + "</td>" +
                    "<td hidden id=postID" + i +">" + data[i].post + "</td>" +
                    "<td hidden id=postAddressID" + i +">" + data[i].postAddress + "</td>" +
                    "<td hidden id=telID" + i +">" + data[i].tel + "</td>" +
                    "<td hidden id=faxID" + i +">" + data[i].fax + "</td>" +
                    "<td hidden id=emailID" + i +">" + data[i].email + "</td>" +
                    "<td hidden id=orderDateID" + i +">" + data[i].orderDate + "</td>" +
                    "<td hidden id=deliveryDateID" + i +">" + data[i].deliveryDate + "</td>" +
                    "</tr>"
                    );
            }

        },
        error: function (xhr) {
            alert(xhr.status);
        }
        
    })

    //動的生成した[更新]ボタンの「class=updateBtnClass」を要素追加：クリック時の処理
    $(document).on('click', ".updateBtnClass", function () {

        //ローカルストレージ内のデータをClear()メソッドで全削除する。
        localStorage.clear();

        //直近の親要素<tr>をclosest()メソッドで取得する。
        var tr = $(this).closest('tr')[0];

        //クリックした行を変数rowNumに格納する。
        var rowNum = tr.rowIndex;
        //ヘッダー行も含み、かつ<tbody>以下の行の列のIDは「0」から始まるので「-2」で減算する。
        rowNum = rowNum - 2;

        //ローカルストレージに詰める
        localStorage.setItem("key1", $("#clientNameID" + rowNum).text()); //localStorage.setItem("key1", $("#clientNameID" + (rowNum - 1)).text());
        localStorage.setItem("key2", $("#totalID" + rowNum).text());
        localStorage.setItem("key3", $("#order1ID" + rowNum).text());
        localStorage.setItem("key4", $("#order1priceID" + rowNum).text());
        localStorage.setItem("key5", $("#order1NumID" + rowNum).text());
        localStorage.setItem("key6", $("#order2ID" + rowNum).text());
        localStorage.setItem("key7", $("#order2priceID" + rowNum).text());
        localStorage.setItem("key8", $("#order2NumID" + rowNum).text());
        localStorage.setItem("key9", $("#order3ID" + rowNum).text());
        localStorage.setItem("key10", $("#order3priceID" + rowNum).text());
        localStorage.setItem("key11", $("#order3NumID" + rowNum).text());
        localStorage.setItem("key12", $("#order4ID" + rowNum).text());
        localStorage.setItem("key13", $("#order4priceID" + rowNum).text());
        localStorage.setItem("key14", $("#order4NumID" + rowNum).text());
        localStorage.setItem("key15", $("#order5ID" + rowNum).text());
        localStorage.setItem("key16", $("#order5priceID" + rowNum).text());
        localStorage.setItem("key17", $("#order5NumID" + rowNum).text());
        localStorage.setItem("key18", $("#clientIdInputNameID" + rowNum).text());
        localStorage.setItem("key19", $("#orderID" + rowNum).text());
        localStorage.setItem("key20", $("#departmentID" + rowNum).text());
        localStorage.setItem("key21", $("#postID" + rowNum).text());
        localStorage.setItem("key22", $("#postAddressID" + rowNum).text());
        localStorage.setItem("key23", $("#telID" + rowNum).text());
        localStorage.setItem("key24", $("#faxID" + rowNum).text());
        localStorage.setItem("key25", $("#emailID" + rowNum).text());
        localStorage.setItem("key26", $("#orderDateID" + rowNum).text());
        localStorage.setItem("key27", $("#deliveryDateID" + rowNum).text());

        //更新のページに遷移する。
        location.href = "OrderInputUpdated.html";

    })

    //動的生成した[詳細]ボタンの「class=updateBtnClass」を要素追加：クリック時の処理
    $(document).on('click', ".detailBtnClass", function () {

        //clear()メソッドでローカルストレージの値を全削除する。
        localStorage.clear();

        //直近の親要素<tr>をclosest()メソッドで取得する。
        var tr = $(this).closest('tr')[0];

        //クリックした行を変数rowNumに格納する。
        var rowNum = tr.rowIndex;
        //ヘッダー行も含み、かつ<tbody>以下の行の列のIDは「0」から始まるので「-2」で減算する。
        rowNum = rowNum - 2;

        //ローカルストレージに詰める
        localStorage.setItem("key1", $("#clientNameID" + rowNum).text()); // localStorage.setItem("key1", $("#clientNameID" + (rowNum - 1)).text());
        localStorage.setItem("key2", $("#totalID" + rowNum).text()); //localStorage.setItem("key2", $("#totalID" + (rowNum - 1)).text()); 
        localStorage.setItem("key3", $("#order1ID" + rowNum).text());
        localStorage.setItem("key4", $("#order1priceID" + rowNum).text());
        localStorage.setItem("key5", $("#order1NumID" + rowNum).text());
        localStorage.setItem("key6", $("#order2ID" + rowNum).text());
        localStorage.setItem("key7", $("#order2priceID" + rowNum).text());
        localStorage.setItem("key8", $("#order2NumID" + rowNum).text());
        localStorage.setItem("key9", $("#order3ID" + rowNum).text());
        localStorage.setItem("key10", $("#order3priceID" + rowNum).text());
        localStorage.setItem("key11", $("#order3NumID" + rowNum).text());
        localStorage.setItem("key12", $("#order4ID" + rowNum).text());
        localStorage.setItem("key13", $("#order4priceID" + rowNum).text());
        localStorage.setItem("key14", $("#order4NumID" + rowNum).text());
        localStorage.setItem("key15", $("#order5ID" + rowNum).text());
        localStorage.setItem("key16", $("#order5priceID" + rowNum).text());
        localStorage.setItem("key17", $("#order5NumID" + rowNum).text());
        localStorage.setItem("key18", $("#clientIdInputNameID" + rowNum).text());
        localStorage.setItem("key19", $("#orderID" + rowNum).text());
        localStorage.setItem("key20", $("#departmentID" + rowNum).text());
        localStorage.setItem("key21", $("#postID" + rowNum).text());
        localStorage.setItem("key22", $("#postAddressID" + rowNum).text());
        localStorage.setItem("key23", $("#telID" + rowNum).text());
        localStorage.setItem("key24", $("#faxID" + rowNum).text());
        localStorage.setItem("key25", $("#emailID" + rowNum).text());
        localStorage.setItem("key26", $("#orderDateID" + rowNum).text());
        localStorage.setItem("key27", $("#deliveryDateID" + rowNum).text());

        //更新のページに遷移する。
        location.href = "Detail.html";

    })

    //削除ボタンクリックでクリック行目を取得する。
    //URL:http://itemy.net/?p=1495
    $(document).on('click', '.deleteBtnClass' ,function () {
        var td = $(this)[0];

        //直近の親要素<tr>をclosest()メソッドを使って取得する。
        var tr = $(this).closest('tr')[0];
        //console.log('td:' + td.cellIndex);
        console.log('tr:' + tr.rowIndex + '行目');
        console.log($(this).text());
    });

})