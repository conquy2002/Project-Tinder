app.loadnoti = ()=>{
    if(app.login){
        setTimeout(async() => {
            try{
                var data = await app.fetch.get(basic_url + Notiurl + "/" + app.dataUser.id);
                if(app.noti != data.length){
                    if(data.length != 0){
                        document.title = "Robin Hẹn Hò " + data[0].body;
                        if(data.length != app.noti){
                            data.forEach(a => app.senderidnoti[a.senderId] ? app.senderidnoti[a.senderId]++ : app.senderidnoti[a.senderId] = 1);
                            app.noti = data.length;
                            // app.update();
                        }
                    }else{
                        document.title = "Robin Hẹn Hò";
                    }
                }
                if(app.noti == 0) document.title = "Robin Hẹn Hò";
            }catch(err){
                console.log(err)
            }
            app.loadnoti();
        },1000)
    } 
}
app.start();




