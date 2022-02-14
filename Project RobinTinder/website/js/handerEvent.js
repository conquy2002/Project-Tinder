app.handEvent = async () =>{
    if(!app.login){
        qS("#js_reload").onclick = ()=> alert("Bạn cần đăng nhập") ;
        qS("#js_tym").onclick = ()=> alert("Bạn cần đăng nhập");
        qS("#js_notym").onclick = ()=> alert("Bạn cần đăng nhập");
        //chuyển sang đăng ký
        qS("#covent_dk").onclick = ()=> {
            qS("#form-login").style.opacity = "0";
            qS("#form-login").style.left = "100%";
            qS("#form-register").style.opacity = "1";
            qS("#form-register").style.left = "0";
        }
        //chuyển sang đăng nhập
        qS("#covent_dn").onclick = ()=> {
            qS("#form-login").style.opacity = "1";
            qS("#form-login").style.left = "0";
            qS("#form-register").style.opacity = "0";
            qS("#form-register").style.left = "100%";
        }
        //click đăng nhập
        qS("#login").onclick = async ()=>{
            var time = Date.now();
            var pwdmd5 = md5(md5(qS("#pwd_login").value));
            var token = md5(qS("#email_login").value + pwdmd5);
            setCookie("Token",token,time)
            return app.start();
        }
        //click đăng ký
        qS('#register').onclick = async ()=>{
            var pwd = qS('#pwd').value
            var confirmPwd = qS('#confirmation_pwd').value
            if(pwd != confirmPwd) {
                alert('Mat khau khong khop, vui long kiem tra lai!!!')
                return false;
            }
            var time = Date.now();
            var pwdmd5 = md5(md5(pwd));
            var token = md5(qS("#email").value + pwdmd5);     
            var hobbys = [];
            document.getElementsByName("vehicle1")
                .forEach(a => a.checked ? hobbys.push({
                    hobbyId: a.value
                }) : "")
            return app.fetch.post(basic_url + Userurl, {
                "account": qS("#email").value,
                "password":  pwdmd5,
                "token": token,
                "name": qS("#name").value,
                "age": qS("#age").value,
                "city": qS("#city").value,
                "note": "",
                "avatar": "nofound.jpg",
                "gender": document.getElementsByTagName("option")[qS("#genderoption").selectedIndex].value,
                "hobbies": [],
                "userHobbys": hobbys
            },false,(loi,data) => {
                if(loi){
                    alert("Trang web đang bảo trì !! Mong bạn vào lại vào hôm sau :3");
                    return app.start();
                } 
                if(data.status == "Error" || data.errors) {  
                    alert(data.message ? data.message : data.errors); 
                    console.log(data)
                    return  app.start();
                }
                alert(data.message)
                setCookie("Token",token,time)
                setCookie("UserID",token,time)
                return app.start();
            })
        }
    }else{
        //click log out
        qS("#logout").onclick = ()=> {
            var time = new Date().getTime();
            setCookie("Token"," ",time);
            setCookie("UserID"," ",time);
            window.location.reload();
            return app.start();
        }
        //click vào profile_account
        qS("#js_profile").onclick = ()=> {
            qS(".nav_content").classList.toggle("top100");
            qS(".nav_profile").classList.toggle("top100");
            return qS("#js_profile").classList.toggle("emphasize")
        }

        //click vào profile button
        var nav_clicks = qSA(".nav-button");
        for (const item of nav_clicks) {
            item.onclick = (e) => {
                nav_clicks.forEach(a => a.classList.remove("nav-active"))
                item.classList.add("nav-active");
                switch (item.dataset.value) {
                    case "tin_nhan": {
                        qS("#js_nav_content_main").innerHTML = bodymess ? bodymess :`<h2>Bạn chưa kết nối với ai</h2>`;
                        break;
                    }
                    case "ket_noi":{
                        qS("#js_nav_content_main").innerHTML = ketnoihtml ? ketnoihtml :`<h2>Bạn chưa kết nối với ai</h2>`;
                        break;
                    }
                }
                app.handEvent();

            }
        }

        var Userconnect_clicks = qSA(".connection");
        for (const item of Userconnect_clicks) {
            item.onclick = ()=> showmess(parseInt(item.dataset.value));
        }
        var Userconnect_clicks2 = qSA(".messger");
        for (const item of Userconnect_clicks2) {
            item.onclick = ()=> {
                Userconnect_clicks2.forEach(a => a.classList.remove("active-messger"));
                item.classList.add("active-messger")
                item.classList.remove("notimessger")
                showmess(parseInt(item.dataset.value));
            };
        }
        //Khi nhấn keybroad
        
        shorProFile();
        qS("#js_reload").onclick = ()=> {return app.nextprofile()} ;
        qS("#js_tym").onclick = ()=> {return tym()};
        qS("#js_notym").onclick = ()=> {return app.nextprofile()};
        qS("#js_renderprofile").onclick = ()=> showprofile();
        qS("#js_hiddenprofile").onclick = ()=> hildeprofile();
        qS("body").addEventListener("keydown", (event) => {
            switch(event.code){
                case "ArrowUp":
                    showprofile();
                    break;
                case "ArrowDown":
                    hildeprofile();
                    break;
                case "ArrowLeft":
                    break;
                case "ArrowRight":
                    app.nextprofile()
                    break;
                case "Space":
                    app.plusDivs(1);
                    break;
            }
            return;
        });
    }
}
async function showmess(id){
    var hobby = "";
    var data = await app.fetch.get(basic_url + Userurl + "/"+ id);
    data.hobbies.forEach(a => hobby += `<div>${a.name}</div>`)
    var img = basic_url + ImagerUrl + "?name=" + data.avatar;
    qS(".content_messger-main").innerHTML = `<div class="content_messger-header">
        <img src = "${img}">
        <h3>${data.name}</h3>
        <div class ="hiddenmessger"><i class='bx bx-x'></i></div>
    </div>
    <div class="content_messger-body"></div>
    <div class="content_messger-input">
        <div class ="tacvu">
            <label style = " color: #ffffff ; " for = "inputImg"><i class='bx bx-file-blank' ></i></label>
            <label  style = " color: #0f63ff ; "><i class='bx bxs-file-gif'></i></label>
            <input type="file" id = "inputImg" accept=".jpg, .jpeg, .png" multiple>
        </div>
        <input type="text" placeholder="Nhập tin nhắn" id = "inputbodymessger">
        <div class ="sender">
            <i class='bx bx-send'></i>
        </div>
        <div class="imgshowinput"></div>
    </div>`
    qS(".content_messger-profile").innerHTML = `
    <div class = "nav_profile-imgavt">
        <img src = "${basic_url + ImagerUrl + "?name="+ data.avatar}">
    </div>
    <div class="nav_profile-main">
        <h3 class = "hilight"> ${data.name} <span>${data.age}</span> </h3>
        <h3>Nơi ở: <span>${data.city}</span></h3>
        <h3>Giới Tính: <span>${data.gender}</span></h3>
        <hr/>
        <span style = "margin: 5px 10px;">${data.note}</span>
        <hr/>
        <h3> Sở thích </h3>
        <div class = "content__profile-main-hobbys">
        ${hobby}
        </div>
    </div>`
    
    qS(".hiddenmessger").onclick = ()=> {
        qS('.content_messger').style.display = "none";
        var Userconnect_clicks2 = qSA(".messger");
            Userconnect_clicks2.forEach(a => a.classList.remove("active-messger"));
        app.showMessage = "";
    }
    qS('.content_messger').style.display = "flex";
    app.imgput = [];
    qS('#inputImg').onchange = ()=>{
        app.imgput = [];
        var imgPofilePush = qS("#inputImg").files;
        for (const file of imgPofilePush) {
            app.imgput.push(file);
        }
        showimg()
    }
    
    qS(".content_messger-body").onscroll = ()=> {
        if(qS(".content_messger-body").scrollTop == 0){
            showMessage(app.showMessage,id,true)
        }
    }
    var threadIDs = await app.fetch.get(basic_url + "/api/MessgerBodies/" +`?UserId=${id}&userconnectid=${app.dataUser.id}`);
    app.showMessage = threadIDs.length > 0 ?  threadIDs[0] : "";
    var datames = await app.fetch.get(basic_url + "/api/MessgerBodies/" +app.showMessage +"?number=2");
    if(datames.length > 0) showMessage(app.showMessage,id)
    
    qS('#inputbodymessger').addEventListener("keydown", async(e)=>{
        if(e.code == "NumpadEnter" || e.code == "Enter"){
            sendMessger(id);
        }
    })
    qS(".sender").onclick = () => sendMessger(id);

    return data.hobbies.forEach(a => {
        qS(".content_profile .content__profile-main-hobbys").innerHTML += `<div>${a.name}</div>`
    })
}
function showprofile(){
    qS('.content_profile').style.display = "block";
}
function hildeprofile(){
    qS('.content_profile').style.display = "none";
}
async function tym(){
    var data = app.profile_account_ho[0];
    await app.fetch.post(basic_url + UserConnectUrl,{
        "userId": app.dataUser.id,
        "userConnectId": data.id
      },false,(err,data)=> {console.log(err,data)})
    return app.nextprofile()
}
function showimg(){
    var img = app.imgput;
    qS(".imgshowinput").innerHTML = "";
    qS(".imgshowinput").style.display = "flex";
    if(img.length == 0) return qS(".imgshowinput").style.display = "none";
    for(var i=0;i<img.length;i++){
        qS(".imgshowinput").innerHTML += `<div>
            <img src = '${URL.createObjectURL(img[i])}'>
            <div class = "icon" onclick = "deleteimginput(${i})"><i class='bx bx-x'></i></div>
        </div>`
    }
}
function deleteimginput(id){
    app.imgput.splice(id,1);
    showimg();
}
function renderimgProlife(){
    var imgPofile = app.imgPofilePush;
    qS("#show-img").innerHTML = "";
    for (let index = 0; index <  app.imgshow.length; index++) {
        qS(".nav_profile-main-listimg").style.height = "200px";
        const element =  app.imgshow[index];
        qS("#show-img").innerHTML += `<div class = "imgconent">
            <img src = "${basic_url + ImagerUrl + "?name="+element.imager}">
            <input type = "text" id = "img_profile_context_${index}" placeholder = "Nhập nội dung" value = "${element.body}">
            <div class = "icon" onclick = "deleteimgProlife(${index},${true},${element.id})"><i class='bx bx-x'></i></div>
        </div>`
        
    }
    for (let index = 0; index < imgPofile.length; index++) {
        const element = imgPofile[index];
        qS(".nav_profile-main-listimg").style.height = "200px";
        qS("#show-img").innerHTML += `
        <div class = "imgconent">
            <img src = '${URL.createObjectURL(element)}'>
            <input type = "text" id = "img_profile_context_${app.imgshow.length + index}" placeholder = "Nhập nội dung">
            <div class = "icon" onclick = "deleteimgProlife(${index})"><i class='bx bx-x'></i></div>
        </div>`
    }
}
async function deleteimgProlife(index,old,id){
    if(old){
        app.deleteimg.push(id)
        app.imgshow.splice(index,1)
    }else{
        if(app.imgPofilePush.length > 0) app.imgPofilePush.splice(index,1)
    }   
    renderimgProlife();
}
function changImg(img,idfile,index){
    qS(img).src = URL.createObjectURL(qS(idfile).files[index])
}

function shorProFile(){
    app.dataUser.imagers.forEach(a => app.imgshow.push(a))
    qS(".nav_profile").innerHTML = `
        <div class = "nav_profile-imgavt">
            <img src = "${basic_url + ImagerUrl + "?name=" + app.dataUser.avatar}">
        </div>
        <div class="nav_profile-main">
            <h3 class = "hilight"> ${app.dataUser.name} <span>${app.dataUser.age}</span> </h3>
            <h3>Nơi ở: <span>${app.dataUser.city}</span></h3>
            <h3>Note: <span>${app.dataUser.note}</span></h3>
            <h3>Giới Tính: <span>${app.dataUser.gender}</span></h3>

            <h3> Ảnh nổi bật </h3>
            <div class="nav_profile-main-listimg">
            ${nav_profile_main_listimg}
            </div>
            <h3> Sở thích </h3>
            <div class = "content__profile-main-hobbys">
                ${interestProfileHTML2}
            </div>
            <div class="button" id="Putprofile">Sửa</div>
        </div>`
    qS("#Putprofile").onclick = async ()=>  {
        qS(".nav_profile").innerHTML = `
            <div class = "nav_profile-imgavt">
                <img src = "${basic_url + ImagerUrl + "?name=" + app.dataUser.avatar}" id ="show_avt_profile">    
            </div>
            <label for="avtProfile" class = "label" >Chọn Ảnh</label>
            <input type="file" id = "avtProfile" accept=".jpg, .jpeg, .png"  style = "opacity: 0;" onchange = "changImg('#show_avt_profile','#avtProfile',0)" >
            <div class="nav_profile-main">
                <input type="text" value = "${app.dataUser.name}" id = "namePofile" >
                <input type="number" value = "${app.dataUser.age}" id = "agePofile" >
                <h3>Nơi ở: <input type="text" value = "${app.dataUser.city}" id = "cityPofile" ></h3>
                <h3>Note: <input type="text" value = "${app.dataUser.note}" id = "notePofile"></h3>
                <h3>Giới tính: <select id="genderoption" > 
                                    <option id = "Nam" value="Nam">Nam</option> 
                                    <option id = "Nữ" value="Nữ">Nữ</option> 
                                    <option id = "Khác" value="Khác">Giới tính khác</option> 
                                    <option id = "Ẩn" value="Ẩn">Ẩn</option> 
                                </select>  </h3>
                <h3> Ảnh nổi bật </h3>
                <div class="nav_profile-main-listimg">
                    <label for="imgPofile"  class = "label">Chọn ảnh</label>
                    <input type="file" id = "imgPofile" accept=".jpg, .jpeg, .png"  style = "opacity: 0;position: absolute;" multiple >
                    <div id = "show-img"></div>
                </div>
                <h3> Sở thích </h3>
                <div class = "content__profile-main-hobbys">
                    ${interestProfileHTML}
                </div>
                <div class="button" id="upload">Upload</div>
                <div class ="button" id="Unupload">Hủy</div>
            </div>
            `
        renderimgProlife();
        //click hủy
        qS("#Unupload").onclick = ()=> {
            return shorProFile(); 
        }   
        //thay đổi ảnh
        qS('#imgPofile').onchange = ()=>{
            var imgPofilePush = qS("#imgPofile").files;
            for (const file of imgPofilePush) {
                app.imgPofilePush.push(file);
            }
            return renderimgProlife();
        }
        qS(`#${app.dataUser.gender}`).selected = true;
            

        qS("#upload").onclick = async ()=>{
            let fileAvater = qS("#avtProfile").files[0];
            var imgPofile = app.imgPofilePush;
            if(fileAvater != undefined){
                const formdata = new FormData();
                formdata.append('file',fileAvater, `${app.dataUser.id}_avt.jpg`);
                await app.fetch.post(basic_url + ImagerUrl,formdata,{},(err,data)=> {
                    if(err != null) {
                        console.log(JSON.stringify(err));
                        return alert("Vui lòng thửu lại sau")
                    }
                })
            } 
            var body_img = [];
            for (let index = 0; index < app.imgshow.length; index++) {
                const element = app.imgshow[index];
                body_img.push({
                    "body": qS(`#img_profile_context_${index}`).value, 
                    "imager": element.imager, 
                    "created": element.created,
                    "userId": element.userId
                })
            }
            
            if(imgPofile.length != 0) {
                for(var i=0; i<imgPofile.length; i++) {
                    const formdata2 = new FormData();
                    var time = new Date();
                    var imgName = i+ md5(time);
                    formdata2.append('file',imgPofile[i], `${app.dataUser.id}_Imgae_${imgName}.jpg`);
                    await app.fetch.post(basic_url + ImagerUrl,formdata2,true,(err,data)=> {
                        if(err != null) {
                            console.log(JSON.stringify(err));
                            return alert("Vui lòng thửu lại sau")
                        }
                    })
                    body_img.push({
                        "body": qS(`#img_profile_context_${app.imgshow.length + i}`).value,
                        "userId": app.dataUser.id,
                        "imager": `${app.dataUser.id}_Imgae_${imgName}.jpg`
                    })
                }
                app.imgPofilePush = [];
            }

            app.deleteimg.forEach(async(id) => await app.fetch.delete(basic_url + UserImagerUrl + "/"+id,(err) => console.log(JSON.stringify(err))))

            
            await app.fetch.post(basic_url + UserImagerUrl + `/${app.dataUser.id}`,body_img,false,(err)=> {console.log(JSON.stringify(err))})


            await app.fetch.put(basic_url + Userurl + `/${app.dataUser.id}` , {
                "id": app.dataUser.id,
                "account": app.dataUser.account,
                "password":  app.dataUser.password,
                "token": app.dataUser.token,
                "name": qS("#namePofile").value,
                "age": qS("#agePofile").value,
                "city": qS("#cityPofile").value,
                "note": qS("#notePofile").value,
                "avatar": fileAvater ?  `${app.dataUser.id}_avt.jpg` : app.dataUser.avatar ,
                "gender": document.getElementsByTagName("option")[qS("#genderoption").selectedIndex].value
            },(err, data) => {
                if(err) { 
                    JSON.stringify(err)
                    return alert("Vui lòng thửu lại sau")
                }
                
            })
            var hobbysArray = [];
            document.getElementsByName("vehicle1").forEach(a => a.checked ?hobbysArray.push({userId: app.dataUser.id, hobbyId: parseInt(a.value)}) : "")
            
            await app.fetch.post(basic_url + hobbyUserUrl + `?put=true&id=` + app.dataUser.id,hobbysArray, false,(err,data) => {
                if(err){
                    console.log(JSON.stringify(err));
                    return alert("Vui lòng thửu lại sau")
                }
            })
            return app.start();
        }
    } 
}
async function showMessage(id,userid,cong){
    app.noti = 0;
    if(id == "") return qS(".content_messger-body").innerHTML = "";
    !app.page ? app.page = 2 : "";
    if(cong){
        app.page = app.page + 1
    }
    var data = await app.fetch.get(basic_url + "/api/MessgerBodies/" +id +"?number="+app.page);
    var body = "";
    
    for (let index = data.length-1; index >= 0; index--) {
        const element = data[index];
        var imgbody = "";
        if(element.imagers.length > 0) {
            element.imagers.forEach(a =>  imgbody += `<img src="${basic_url + ImagerUrl + "?name=" + a.name}" >`)
        }
        if(element.senderId == app.dataUser.id){
            body += `
                ${element.body ? `<div class = "messger-body-self"><span>${element.body}</span></div>` : ""}
                
                ${element.imagers.length > 0 ? `<div class = "messger-body-self"><div class="messger-body-img">${imgbody}</div></div>`  : ""}
            `
        }else{
            body += `
            ${element.body ? `<div class = "messger-body-you"><span>${element.body}</span></div>` : ""}
            ${element.imagers.length > 0 ? `<div class = "messger-body-you"><div class="messger-body-img">${imgbody}</div></div>`  : ""}
            `
        }
    }
    qS(".content_messger-body").innerHTML = body;
    await app.fetch.delete(basic_url + Notiurl + `/${app.dataUser.id}?senderid=` + userid,(err,data)=>{})
    if(!cong)qS(".content_messger-body").scrollTop = qS(".content_messger-body").scrollHeight;
}
async function sendMessger(id){
    var hasimg = [];
    var time = new Date();
    
    if(app.imgput.length != 0) {
        for(var i=0; i<app.imgput.length; i++) {
            const formdata2 = new FormData();
            var imgName = i+ md5(time);
            formdata2.append('file',app.imgput[i], `${app.dataUser.id}_Messger_Imgae_${imgName}.jpg`);
            await app.fetch.post(basic_url + ImagerUrl,formdata2,true,(err,data)=> {
                if(err != null) {
                    console.log(JSON.stringify(err));
                    return alert("Vui lòng thửu lại sau")
                }
            })
            hasimg.push({
                "name": `${app.dataUser.id}_Messger_Imgae_${imgName}.jpg`,
                "time": time
            })
        }
    }
    app.showMessage != "" ? "" :app.showMessage = `${id}_${app.dataUser.id}`;
    await app.fetch.post(basic_url + PostMessger,[{
        "userId": app.dataUser.id,
        "threadID": app.showMessage,
        "body": qS("#inputbodymessger").value,
        "self": true,
        "threadUserId": id,
        "messgerBodies": {
            "threadID": app.showMessage ,
            "senderId": app.dataUser.id,
            "body": qS("#inputbodymessger").value,
            "time": time,
            "imagers": hasimg
        }
    },{
        "userId": id,
        "threadID": app.showMessage,
        "body": qS("#inputbodymessger").value,
        "threadUserId": app.dataUser.id,
        "self": false
    }],false,(err,data)=> console.log(JSON.stringify(err),data));
    await app.fetch.post(basic_url + Notiurl,{
        "id": 0,
        "userId": id,
        "senderID": app.dataUser.id,
        "body": `${app.dataUser.name} đã nhắn tin cho bạn: ${qS("#inputbodymessger").value}`
    },false,(err)=> {if(err) console.log(JSON.stringify(err))})
    qS("#inputbodymessger").value = "";
    
    app.imgput = [];
    qS(".imgshowinput").style.display = "none";
    return app.update();
}