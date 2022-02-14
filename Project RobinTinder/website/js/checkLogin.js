app.checklogin = async ()=>{
    var token = getCookie("Token");
    if(token == ''){
        return app.login = false;
    }else{
        var time = new Date().getTime();
        var data = ""
        try{
            data = await app.fetch.get(basic_url + getUser +"?user="+token);
            if(data.status == "Error") {
                app.login = false;
                setCookie("UserID"," ",time);
                setCookie("Token"," ",time);
                alert("Tài Khoản k tồn tại")
                return app.login = false;        
            }else{
                setCookie("UserID",data.message,time);
                return app.login = true;
            }
        }catch(e){
            alert("Trang web đang bảo trì !! Mong bạn vào lại vào hôm sau :3");
            console.log(e)
            setCookie("UserID"," ",time);
            setCookie("Token"," ",time)
            return app.login = false;
        }
    }
}