<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>水產養殖管理系統</title>
    <meta name="description" content="智慧水產養殖管理資訊系統" />
    <meta name="keywords" content="智慧水產養殖管理資訊系統" />
    <meta name="author" content="智慧水產養殖管理資訊系統" />
    <!-- Icon -->
    <link rel="icon" href="image/favcon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="image/favcon.ico">
    <!-- CSS Design -->
    <link href="css/jasny-bootstrap.min.css" rel="stylesheet" />
    <!-- CSS Main -->
    <link href="css/style.css" rel="stylesheet" />

        <!--驗證結果訊息為了美觀，多追加以下的css-->
    <style type="text/css">
    #span_result
    {
     color:Red;
     font-size:12px;      
     }
    </style>
</head>
<body>
    <!--Loding-->
    <div class="preloader-it">
        <div class="la-anim-1"></div>
    </div>
    <!--Loding-->
    <div class="wrapper pa-0">
        <!--Main-->
        <div class="page-wrapper pa-0 ma-0">
            <div class="container-fluid full">
                <!-- Row 
                  <div class="pull-right">         
                    <h4 class="animated" style="color:white"><span>清新的空氣、廣闊的海洋</span></h4>
                    <h4 class="animated" style="color:white"><span>師法自然 用心耕耘</span></h4>
                    <h4 class="animated" style="color:white"><span>共同珍惜地球賦予珍貴寶藏</span></h4>
                  </div>
                    -->
                <div class="table-struct full-width full-height">
                    <div class="table-cell vertical-align-middle">
                        <div class="auth-form  ml-auto mr-auto no-float">
                            <div class="panel panel-default card-view mb-0">
                                <div class="panel-heading">
                                    <div class="pull-right" >                                                                 
                                        <h4> 登入系統 </h4>
                                        <p style="color:red;">請直接登入您的帳號、密碼與驗證碼</p>
                                    </div>
                                    <img src="image/icon/login.png"  height="50" width="50"/>

                                    <div class="clearfix"></div>
                                </div>
                                <div class="panel-wrapper collapse in">
                                    <div class="panel-body" style="width:370px;height:270px">
                                        <div class="row">
                                            <div class="col-sm-12 col-xs-12">
                                                <div class="form-wrap">
                                                    <form action="#" runat="server" id="form1">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-3">帳號</label>
                                                            <div class="input-group">
                                                                <input type="email" class="form-control" required="" id="" placeholder="輸入帳號">
                                                                <div class="input-group-addon"><i class="icon-envelope-open"></i></div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="control-label col-md-3">密碼</label>
                                                            <div class="input-group">
                                                                <input type="password" class="form-control" required="" id="" placeholder="輸入密碼">
                                                                <div class="input-group-addon"><i class="icon-lock"></i></div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">驗證碼</label>                                                          
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txt_input" class="form-control" runat="server" maxlength="4" /><span id="span_result"></span>
                                                                <div class="input-group-addon"><img src="ValidateNumber.ashx" alt="驗證碼" onclick="form1.imgCode.src='ValidateNumber.ashx?' + Math.random();" name="imgCode"  /> </div>
                                                            </div>
                                                        </div>                                
                                                            <asp:Button Text="登入"  class="btn btn-primary btn-block" ID="btn_submit" runat="server" OnClientClick="return isPassValidateCode();" onclick="btn_submit_Click" />
                                                            <img src="image/background/logo-1.png"/>                                        
                                                    </form>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--Row -->
            </div>
            <!-- Footer -->
                <footer class="footer">
                    <div class="row">
                       <div class="col-sm-12 text-center" style="color:black">      
                            <p>&nbsp;&nbsp;國立屏東科技大學 電子商務研發中心、資訊管理系多媒體資訊網路前瞻實驗室 版權所有&nbsp;&nbsp;&nbsp;&nbsp; Copyright &copy; 2017 &nbsp; All Rights Reserved &nbsp;&nbsp;本網站支援IE10以上、Firefox及Chrome，最佳瀏覽解析度為1024x768以上</p>
                            </div>
                    </div>
                </footer>
                <!-- /Footer -->
        </div>
        <!--Main-->
    </div>

    <!-- jQuery -->
    <script src="javascript/jquery.min.js"></script>
    <!-- Bootstrap Core JavaScript -->
    <script src="javascript/bootstrap.min.js"></script>
    <script src="javascript/jasny-bootstrap.min.js"></script>
    <!-- Slimscroll JavaScript -->
    <script src="javascript/jquery.slimscroll.js"></script>
    <!-- Fancy Dropdown JS -->
    <script src="javascript/dropdown-bootstrap-extended.js"></script>
    <!-- Init -->
    <script src="javascript/init.js"></script>
    <script src="javascript/typed.js"></script>
        <script type="text/javascript">
        jQuery(document).ready(init);
        function init() {
           /*每次Dom載入完，確保圖片都不一樣*/
           jQuery("img[name='imgCode']").attr("src", "ValidateNumber.ashx?" + Math.random());        
        }
        function isPassValidateCode() {
          var  nowValidateNumber =  jQuery.ajax({
                url: "verification.ashx",
                type: "post",
                async: false,
                data:{},
                success: function (htmlVal) {  }
            }).responseText;

            if (nowValidateNumber == "" || nowValidateNumber == null) {
                alert("驗證碼逾時，請重新整理");
                return false;
            }
            var userInput = jQuery("#<%= txt_input.ClientID%>").val();
            var validateResult = ((nowValidateNumber == userInput) ? true : false);
            if (validateResult == false) {
                jQuery("#span_result").html("驗證碼輸入不正確");
            }
            //回傳true Or false
            return validateResult;
        }
    </script>
</body>
</html>
