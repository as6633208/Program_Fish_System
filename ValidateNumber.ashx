<%@ WebHandler Language="C#" Class="ValidateNumber" %>

using System;
using System.Web;
using System.Drawing;
using System.Web.SessionState;
public class ValidateNumber : IHttpHandler , IRequiresSessionState{
    
     public void ProcessRequest (HttpContext context) {
        
        int NumCount = 4;//預設產生4位亂數
        if(!string.IsNullOrEmpty(context.Request.QueryString["NumCount"]))
        {//有指定產生幾位數
           
            //字串轉數字，轉型成功的話儲存到 NumCount，不成功的話，NumCount會是0
          Int32.TryParse(context.Request.QueryString["NumCount"].Replace("'","''"),out NumCount);
        }
        if(NumCount==0)NumCount=4;
        //取得亂數
        string str_ValidateCode = this.GetRandomNumberString(NumCount);
        /*用於驗證的Session*/
        context.Session["ValidateNumber"] = str_ValidateCode;
        
        //取得圖片物件
        System.Drawing.Image image = this.CreateCheckCodeImage(context,str_ValidateCode);
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms , System.Drawing.Imaging.ImageFormat.Jpeg);
        /*輸出圖片*/
        context.Response.Clear();
        context.Response.ContentType = "image/jpeg";
        context.Response.BinaryWrite(ms.ToArray());
        ms.Close();
            
    }
 
    #region 產生數字亂數
    private string GetRandomNumberString(int int_NumberLength)  
    {
        System.Text.StringBuilder str_Number = new System.Text.StringBuilder();//字串儲存器
        Random rand = new Random(Guid.NewGuid().GetHashCode());//亂數物件
        
        for(int i = 1 ;i<= int_NumberLength ;i++)
        {
            str_Number.Append( rand.Next(0,10).ToString() );//產生0~9的亂數
        }
        
        return str_Number.ToString();
    }
    #endregion

    #region 產生圖片
    private System.Drawing.Image CreateCheckCodeImage(HttpContext context,string checkCode)
    {

        System.Drawing.Bitmap image = new System.Drawing.Bitmap((checkCode.Length * 20), 27);//產生圖片，寬20*位數，高40像素
        System.Drawing.Graphics g =   Graphics.FromImage(image);

        
            //生成隨機生成器
            Random random =new Random(Guid.NewGuid().GetHashCode());
            int int_Red =255;
            int int_Green =255;
            int int_Blue =255;
  

            //清空圖片背景色
            g.Clear(Color.FromArgb(int_Red, int_Green, int_Blue));
        


            Font font = new System.Drawing.Font("Arial", 19, (System.Drawing.FontStyle.Bold));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2F, true);
          
            g.DrawString(checkCode, font, brush, 2, 2);
            for(int i = 0 ;i<= 99;i++)
            {

                //畫圖片的前景噪音點
                int x   = random.Next(image.Width);
                int y   = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //畫圖片的邊框線
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            return image;
     
    }
    #endregion

    /*實作 IHttpHandler介面的方法，不可刪除*/
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}