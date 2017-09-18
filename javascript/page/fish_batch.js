var notyf = new Notyf();//notyf.js宣告使用
//魚池id
var Fish_Pool_id;
//魚池資料變數
var Fish_detail_id = -1; //魚群細節編號
var Fish_detail_Fish_id = -1; //魚群編號1 2 3 4 5
var Fish_detail_Move_date = -1; //移動日7
var Fish_detail_Fish_size = -1; //魚隻體型6
var Fish_detail_Origin_Fish_detail_id = -1; //魚群來源
var Fish_Spawning_date = -1; //產卵日 1
var Fish_Insert_fry_date = -1; //入苗日 2
var Fish_Insert_year = -1; //民國年 3
var Fish_Fry_weight = -1;//魚苗魚重 4
var Fish_Fish_company_id = -1; //魚群供應商編號
var Fish_company_company_abbreviation = -1;//魚群供應商簡稱 5
var Fish_Fish_kind_id = -1;//魚種編號
var Fush_Pool_Pool_number = -1;//魚池數量
var Fish_Pool_Pool_number = -1;//魚池數量
var Fish_detail_Fish_AVGweight = 0;//測量用
//抓網址變數
$.UrlParam = function (name) {
    //宣告正規表達式
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    /*
     * window.location.search 獲取URL ?之後的參數(包含問號)
     * substr(1) 獲取第一個字以後的字串(就是去除掉?號)
     * match(reg) 用正規表達式檢查是否符合要查詢的參數
    */
    var r = window.location.search.substr(1).match(reg);
    //如果取出的參數存在則取出參數的值否則回穿null
    if (r != null) return unescape(r[2]); return null;
}
var Page_Pool_id = $.UrlParam('Pool_id') ? $.UrlParam('Pool_id') : -1; //現在魚池
if (Page_Pool_id == -1) {
    notyf.alert('伺服器錯誤,無法載入池號');
} else {
    //顯示魚池編號
    $("#pool").html(Page_Pool_id);
    /*批號開始*/
    //魚池ajax
    $.ajax({
        url: 'database/pool/pool_view.ashx',
        type: 'post',
        dataType: 'json',
        async: false,//啟用同步請求
        success: function (Pool_Json) {
            Fish_Pool_id = Pool_Json;
            var Json_Pool_id = -1; //JSON 索引值
            //搜尋池號
            for (var Pool_id in Pool_Json) {
                if (Pool_Json[Pool_id]["Pool_id"] == Page_Pool_id) {
                    Json_Pool_id = Pool_id;
                }
            }
            if (Json_Pool_id == -1) { //找不到池號為-1
                notyf.alert('伺服器錯誤,無法載入池號');
            } else { //有魚池號的話，魚群細節
                //魚群細節編號
                Fish_detail_id = Pool_Json[Json_Pool_id]["Fish_detail_id"];
                //數量
                $('#pool_number').html(Pool_Json[Json_Pool_id]["Pool_number"] + "隻");
                Fush_Pool_Pool_number = Pool_Json[Json_Pool_id]["Pool_number"];
            }
        },
        error: function (e) {
            notyf.alert('伺服器錯誤,無法載入池號' + e);
        }
    });
    //魚群細節ajax
    $.ajax({
        url: 'database/Fish_detail/Fish_detail_view.ashx',
        type: 'post',
        dataType: 'json',
        data: { Fish_detail_id: Fish_detail_id },
        async: false,//啟用同步請求
        success: function (Fish_detail_Json) {
            Fish_detail_Fish_id = Fish_detail_Json[0]["Fish_id"];
            Fish_detail_Move_date = Fish_detail_Json[0]["Move_date"];
            Fish_detail_Fish_size = Fish_detail_Json[0]["Fish_size"];
            Fish_detail_Fish_AVGweight = Fish_detail_Json[0]["Fish_AVGweight"];
            Fish_detail_Origin_Fish_detail_id = Fish_detail_Json[0]["Origin_Fish_detail_id"];
        },
        error: function (e) {
            notyf.alert('伺服器錯誤,無法載入魚群細節' + e);
        }
    });
    //魚群ajax
    $.ajax({
        url: 'database/Fish/Fish_view.ashx',
        type: 'post',
        dataType: 'json',
        data: { Fish_id: Fish_detail_Fish_id },
        async: false,//啟用同步請求
        success: function (Fish_Json) {
            Fish_Spawning_date = Fish_Json[0]["Spawning_date"];
            Fish_Insert_fry_date = Fish_Json[0]["Insert_fry_date"];
            Fish_Insert_year = Fish_Json[0]["Insert_year"];
            Fish_Fry_weight = Fish_Json[0]["Fry_weight"];
            Fish_Fish_company_id = Fish_Json[0]["Fish_company_id"];
            Fish_Fish_kind_id = Fish_Json[0]["Fish_kind_id"];
        },
        error: function (e) {
            notyf.alert('伺服器錯誤,無法載入魚群資料' + e);
        }
    });
    //供應商ajax
    $.ajax({
        url: 'database/supplier/supplier_search.ashx',
        type: 'post',
        dataType: 'json',
        data: { Fish_company_id: Fish_Fish_company_id },
        async: false,//啟用同步請求
        success: function (Fish_Json) {
            Fish_company_company_abbreviation = Fish_Json[0]["company_abbreviation"];
        },
        error: function (e) {
            notyf.alert('伺服器錯誤,無法載入供應商簡稱' + e);
        }
    });
    //日期操作 產卵日
    var Spawning_date = new Date(Fish_Spawning_date);
    Spawning_date_MM = ((Spawning_date.getMonth() + 1) < 10 ? '0' : '') + (Spawning_date.getMonth() + 1);//補0
    Spawning_date_DD = (Spawning_date.getDate() < 10 ? '0' : '') + Spawning_date.getDate();
    Spawning_date = Spawning_date_MM + '' + Spawning_date_DD;
    //日期操作 入苗日
    var fry_date = new Date(Fish_Insert_fry_date);
    fry_date_MM = ((fry_date.getMonth() + 1 < 10 ? '0' : '')) + (fry_date.getMonth() + 1);
    fry_date_DD = (fry_date.getDate() < 10 ? '0' : '') + fry_date.getDate();
    fry_date = fry_date_MM + '' + fry_date_DD;
    //日期操作 移動日
    var Move_date = new Date(Fish_detail_Move_date);
    Move_date_MM = ((Move_date.getMonth() + 1 < 10 ? '0' : '')) + (Move_date.getMonth() + 1);
    Move_date_DD = (Move_date.getDate() < 10 ? '0' : '') + Move_date.getDate();
    //移入日期顯示
    $('#insert_date').html(Move_date.getFullYear() + '-' + Move_date_MM + '-' + Move_date_DD);
    Move_date = Move_date_MM + '' + Move_date_DD;
    //批號顯示
    $('#batch').html(Spawning_date + '-' + fry_date + '-' + Fish_Insert_year + '-' + Fish_Fry_weight + '-'
        + Fish_company_company_abbreviation + '-' + Fish_detail_Fish_size + '-' + Move_date);

    /*批號結束*/

    /*來源批號開始*/
    var source_empty = false;
    //魚群細節ajax
    $.ajax({
        url: 'database/Fish_detail/Fish_detail_view.ashx',
        type: 'post',
        dataType: 'json',
        data: { Fish_detail_id: Fish_detail_Origin_Fish_detail_id },
        async: false,//啟用同步請求
        success: function (Fish_detail_Json) {
            if (JSON.stringify((Fish_detail_Json)).indexOf("null") <= 0) {
                source_empty = true;
            } else {
                Fish_detail_Fish_id = Fish_detail_Json[0]["Fish_id"];
                Fish_detail_Move_date = Fish_detail_Json[0]["Move_date"];
                Fish_detail_Fish_size = Fish_detail_Json[0]["Fish_size"];
                Fish_detail_Origin_Fish_detail_id = Fish_detail_Json[0]["Origin_Fish_detail_id"];
            }
        },
        error: function (e) {
            source_empty = true;
            // notyf.alert('伺服器錯誤,無法載入來源批號' + e);
        }
    });
    //魚群ajax
    if (!source_empty) { //如果來源不為null
        $.ajax({
            url: 'database/Fish/Fish_view.ashx',
            type: 'post',
            dataType: 'json',
            data: { Fish_id: Fish_detail_Fish_id },
            async: false,//啟用同步請求
            success: function (Fish_Json) {
                Fish_Spawning_date = Fish_Json[0]["Spawning_date"];
                Fish_Insert_fry_date = Fish_Json[0]["Insert_fry_date"];
                Fish_Insert_year = Fish_Json[0]["Insert_year"];
                Fish_Fry_weight = Fish_Json[0]["Fry_weight"];
                Fish_Fish_company_id = Fish_Json[0]["Fish_company_id"];
                Fish_Fish_kind_id = Fish_Json[0]["Fish_kind_id"];
            },
            error: function (e) {
                notyf.alert('伺服器錯誤,無法載入魚群資料' + e);
            }
        });
        //供應商ajax
        $.ajax({
            url: 'database/supplier/supplier_search.ashx',
            type: 'post',
            dataType: 'json',
            data: { Fish_company_id: Fish_Fish_company_id },
            async: false,//啟用同步請求
            success: function (Fish_Json) {
                Fish_company_company_abbreviation = Fish_Json[0]["company_abbreviation"];
            },
            error: function (e) {
                notyf.alert('伺服器錯誤,無法載入供應商簡稱' + e);
            }
        });
        //日期操作 產卵日
        var Spawning_date = new Date(Fish_Spawning_date);
        Spawning_date_MM = ((Spawning_date.getMonth() + 1) < 10 ? '0' : '') + (Spawning_date.getMonth() + 1);//補0
        Spawning_date_DD = (Spawning_date.getDate() < 10 ? '0' : '') + Spawning_date.getDate();
        Spawning_date = Spawning_date_MM + '' + Spawning_date_DD;
        //日期操作 入苗日
        var fry_date = new Date(Fish_Insert_fry_date);
        fry_date_MM = ((fry_date.getMonth() + 1 < 10 ? '0' : '')) + (fry_date.getMonth() + 1);
        fry_date_DD = (fry_date.getDate() < 10 ? '0' : '') + fry_date.getDate();
        fry_date = fry_date_MM + '' + fry_date_DD;
        //日期操作 移動日
        var Move_date = new Date(Fish_detail_Move_date);
        Move_date_MM = ((Move_date.getMonth() + 1 < 10 ? '0' : '')) + (Move_date.getMonth() + 1);
        Move_date_DD = (Move_date.getDate() < 10 ? '0' : '') + Move_date.getDate();
        //移入日期顯示
        $('#insert_date').html(Move_date.getFullYear() + '-' + Move_date_MM + '-' + Move_date_DD);
        Move_date = Move_date_MM + '' + Move_date_DD;
        //批號顯示
        $('#source_batch').html(Spawning_date + '-' + fry_date + '-' + Fish_Insert_year + '-' + Fish_Fry_weight + '-'
            + Fish_company_company_abbreviation + '-' + Fish_detail_Fish_size + '-' + Move_date);
    } else {
        //批號顯示
        $('#source_batch').html("無");
    }
    //魚群種類ajax
    $.ajax({
        url: 'database/fish_kind/fish_kind_search.ashx',
        type: 'post',
        dataType: 'json',
        data: { Fish_kind_id: Fish_Fish_kind_id },
        async: false,//啟用同步請求
        success: function (Fish_kind_Json) {
            $('#fish_kind').html(Fish_kind_Json[0]["kind_name"]);
        },
        error: function (e) {
            notyf.alert('伺服器錯誤,無法載入魚種' + e);
        }
    });
    /*來源批號結束*/
}