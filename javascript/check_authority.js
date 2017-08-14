/**Cookies check**/
$(document).ready(function () {
    //alert(Cookies.get('authority'))
    if (Cookies.get('authority') === undefined) {
        location.href = 'login.html';
    }
});
/****/
$(document).ready(function () {
    $("#Sign_out").click(function (e) {
        e.preventDefault();
        swal({
            title: '確認登出帳戶？',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            allowOutsideClick: false,//不可點背景關閉
            confirmButtonText: '確認！',
            cancelButtonText: '取消'
        }).then(function (isConfirm) {
            if (isConfirm) {
                Cookies.remove('user');
                Cookies.remove('authority');
                swal({
                    type: 'success',
                    title: '登出成功!',
                    allowOutsideClick: false //不可點背景關閉
                }).then(function () {
                    location.href = 'login.html';
                })
            }
        })
    });
});