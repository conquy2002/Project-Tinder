app.render = async ()=>{
    if(!app.login){
        var interestHTML = "";
        qS(".nav__header--contact").innerHTML = " ";
        qS(".nav_content").innerHTML = login_register_html;
        try{
            var hobbys = await app.fetch.get(basic_url + Interests);
            for(var i=0; i<hobbys.length; i++){
                interestHTML += `<div><input type="checkbox" id="hobby_${i}" name="vehicle1" value="${hobbys[i].id}">
                <label for="hobby_${i}"> ${hobbys[i].name} </label></div> `
            }   
        }catch(err){
            console.log(err);
        }
        qS("#js_interest").innerHTML = interestHTML;
        qS("#form-login").style.opacity = "1";   
        qS(".nav__hearder--profile").innerHTML = " ";
        return showContent();
    }else{
        app.dataUser = await app.fetch.get(basic_url + Userurl + "/"+ getCookie("UserID"))
  
        //sở thích trong profile;
        hobbies = await app.fetch.get(basic_url + Interests);
        for(var i=0; i< hobbies.length; i++){
            if(app.dataUser.hobbies.some(j => j.name == hobbies[i].name)){
                interestProfileHTML += `<div><input type="checkbox" id="hobby_${i}" name="vehicle1" value="${hobbies[i].id}" checked>
                <label for="hobby_${i}"> ${hobbies[i].name} </label></div> `
            }else{
                interestProfileHTML += `<div><input type="checkbox" id="hobby_${i}" name="vehicle1" value="${hobbies[i].id}">
                <label for="hobby_${i}"> ${hobbies[i].name} </label></div> `
            }
        } 
        qS(".nav__header--contact").innerHTML = `<div id = "logout"><i class='bx bx-log-out'></i></div>`;
        qS(".nav__hearder--profile").innerHTML = `<a id = "js_profile">
            <img src = "${basic_url + ImagerUrl + "?name=" + app.dataUser.avatar}">
            <h3 style = "margin-right: 5px;">${(app.dataUser.name.split(" ")[app.dataUser.name.split(" ").length-1]).trim(" ")}</h3>
        </a>`;
        //các hình ảnh trong profile
        app.dataUser.imagers.forEach(a => nav_profile_main_listimg += `<div class = "imgconent">
            <img src = "${basic_url + ImagerUrl + "?name="+a.imager}">
            <p> ${a.body}</p>
        </div>`)
        //sở  thích
        
        app.dataUser.hobbies.forEach(a => interestProfileHTML2 += `<div>${a.name}</div>`);

        for(const user of app.dataUser.userContects){
            const data = await app.fetch.get(basic_url + Userurl + "/" + user.userConnectId)
            var img = basic_url + ImagerUrl + "?name="+  data.avatar;
            ketnoihtml += `
            <div class = "connection" data-value = "${user.userConnectId}">
                <img src ="${img}"/>
                <div class = "connection_span"><span>${data.name}</span></div>
            </div>
            `
        }
        for(const user of app.dataUser.messgerUsers){
            const data = await app.fetch.get(basic_url + Userurl + "/" + user.threadUserId);
            var img = basic_url + ImagerUrl + "?name="+  data.avatar;
            if(app.senderidnoti[user.threadUserId]){
                bodymess += `
                <div class = "messger notimessger" data-value = "${user.threadUserId}">
                    <img src ="${img}"/>
                    <div class = "messger-content">
                        <span class = "connection_span"><h3>${data.name}</h3></span>
                        <p>${user.self ? "<i class='bx bx-arrow-back'></i>" : ""}${user.body ? user.body :"ảnh"}</p>
                    </div>
                </div>
                `
            }else{
                bodymess += `
                <div class = "messger " data-value = "${user.threadUserId}">
                    <img src ="${img}"/>
                    <div class = "messger-content">
                        <span class = "connection_span"><h3>${data.name}</h3></span>
                        <p>${user.self ? "<i class='bx bx-arrow-back'></i>" : ""}${user.body ? user.body :"ảnh"}</p>
                    </div>
                </div>
                `
            }
        }
        qS(".nav_content").innerHTML = logined_html;
        app.showMessage ? qS("#js_nav_content_main").innerHTML = bodymess : qS("#js_nav_content_main").innerHTML =  ketnoihtml;
        app.dataUser.hobbies.forEach(a => app.hobbyUser.push(a.id));
        await showContent(app.hobbyUser[0]);
    }
}

async function showContent(hoppyID) {
    var data = "";
    if(hoppyID == null){
        try{
            data = await app.fetch.get(basic_url + Userurl);
            data = data[0];
            if(data == undefined) return;
        }catch(err){
            console.log(err)
        }
    }else{
        app.profile_account_ho.length == 0 ? app.profile_account_ho = await app.fetch.get(basic_url + Interests + "/"+ hoppyID) : "";
        data = app.profile_account_ho[0];
    }
    qS("#js_imgs_profile").innerHTML =   qS('#js_menu_imgs').innerHTML =  qS('#js_main_profile').innerHTML =" ";
    var imgshow = [{"imager": data.avatar }];
    data.imagers.forEach(a => imgshow.push(a))
    for(var index = 0; index < imgshow.length; index++){
        var img = basic_url + ImagerUrl + "?name=" + imgshow[index].imager;
        qS("#js_imgs_profile").innerHTML +=`<div class = "slide slide-${index}"><img  src="${ img}"><span>${imgshow[index].body?imgshow[index].body:""}</span></div>`;
        qS('#js_menu_imgs').innerHTML += `<label class="slide-dot"></label>`
    }
    qS('#js_main_profile').innerHTML = ` <h1>${data.name} "<span>" ${  data.age }"</span>"</h1> <div class = "content__profile-main-hobbys" id = "js_profile-main-hobbys"></div>`; 
        
    if(imgshow.length != 0) app.showDivs(app.slideIndex);
    qS(".content_profile").innerHTML = `
        <div class = "nav_profile-imgavt">
            <img src = "${basic_url + ImagerUrl + "?name="+ data.avatar}">
        </div>
        <div id ="js_hiddenprofile"><i class='bx bxs-downvote'></i></div>
        <div class="nav_profile-main">
            <h3 class = "hilight"> ${data.name} <span>${data.age}</span> </h3>
            <h3>Nơi ở: <span>${data.city}</span></h3>
            <h3>Giới Tính: <span>${data.gender}</span></h3>
            <hr/>
            <span style = "margin: 5px 10px;">${data.note}</span>
            <hr/>
            <h3> Sở thích </h3>
            <div class = "content__profile-main-hobbys">
            </div>
        </div>`
    return data.hobbies.forEach(a => {
        qS('#js_profile-main-hobbys').innerHTML += `<div>${a.name}</div>` ;
        qS(".content_profile .content__profile-main-hobbys").innerHTML += `<div>${a.name}</div>`
    })  

}
