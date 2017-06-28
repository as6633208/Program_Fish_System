<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
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
    <form id="form1" runat="server">
     
     <!--src連結到ValidateNumber.ashx即可-->
     <img src="ValidateNumber.ashx" alt="驗證碼" name="imgCode"  /> 
     <input type="button" onclick="form1.imgCode.src='ValidateNumber.ashx?' + Math.random();" value="重新整理" />
     <hr />


     <!--前端驗證結果訊息要放到ispan_result的innerHtml-->
     <asp:TextBox ID="txt_input" runat="server" /><span id="span_result"></span>
     <br />
     <asp:Button Text="送出" ID="btn_submit" runat="server" 
         OnClientClick="return isPassValidateCode();" onclick="btn_submit_Click" />
    </form>
</body>
</html>