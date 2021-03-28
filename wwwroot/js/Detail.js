$(document).ready(function () {
    //ローカルストレージから値を取得する。
    var m = localStorage.getItem("key1");
    console.log("この値は：" + m);

    //【★重要★】<label>の値を変更する場合はtext()メソッドを使って「$("label#ID名").text(変更する値);」と記述する。
    $("label#clientIdLabelInput").text(localStorage.getItem("key1")); //localStorage.getItem("key1")
    $("label#orderIdInput").text(localStorage.getItem("key19")); 
    $("label#clientNameInput").text(localStorage.getItem("key18"));
    $("label#departmentInput").text(localStorage.getItem("key20"));
    $("label#postInput").text(localStorage.getItem("key21"));
    $("label#postAddressInput").text(localStorage.getItem("key22"));
    $("label#telInput").text(localStorage.getItem("key23"));
    $("label#faxInput").text(localStorage.getItem("key24"));
    $("label#emailInput").text(localStorage.getItem("key25"));
    $("label#order1Input").text(localStorage.getItem("key3"));
    $("label#order1priceInput").text(localStorage.getItem("key4"));
    $("label#order1NumInput").text(localStorage.getItem("key5"));
    $("label#order2Input").text(localStorage.getItem("key6"));
    $("label#order2priceInput").text(localStorage.getItem("key7"));
    $("label#order2NumInput").text(localStorage.getItem("key8"));
    $("label#order3Input").text(localStorage.getItem("key9"));
    $("label#order3priceInput").text(localStorage.getItem("key10"));
    $("label#order3NumInput").text(localStorage.getItem("key11"));
    $("label#order4Input").text(localStorage.getItem("key12"));
    $("label#order4priceInput").text(localStorage.getItem("key13"));
    $("label#order4NumInput").text(localStorage.getItem("key14"));
    $("label#order5Input").text(localStorage.getItem("key15"));
    $("label#order5priceInput").text(localStorage.getItem("key16"));
    $("label#order5NumInput").text(localStorage.getItem("key17"));
    $("label#orderDateInput").text(localStorage.getItem("key26"));
    $("label#deliveryDateInput").text(localStorage.getItem("key27"));
    $("label#totalInput").text(localStorage.getItem("key2"));

    //ローカルストレージのデータをClear()メソッドで全削除する。
    localStorage.clear();

})
