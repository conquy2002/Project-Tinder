app.start = async function(){  
    //check login
    
    await app.checklogin();

    await app.reload();
    //Hiển thị
    
    await app.render();

    var a = Object.values(qS(".nav_profile").classList)
    var c = []
    for(var b of a){
        c.push(b)
    }
    c.includes("top100") ? "" : qS("#js_profile").classList.toggle("emphasize") ;  
    //nghe các sự kiện click
    app.handEvent();


    app.loadnoti();
}
app.plusDivs = (n)=>{
    app.showDivs(app.slideIndex += n);
};
// app.await = (data,dieukien)=>{
//     if(data == dieukien) return; 
//     console.log("chờ")
//     return app.await(data,dieukien);
// }
app.showDivs = (n)=>{
    var i;
    var x = qSA('.slide');
    var x_dot = qSA(".slide-dot");
    if (n > x.length) {app.slideIndex = 1}
    if (n < 1) {app.slideIndex = x.length}
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";  
        x_dot[i].style.background = "rgb(112 112 112)";
    }
    x[app.slideIndex-1].style.display = "flex"; 
    x_dot[app.slideIndex-1].style.background = "#fff";  
};

app.nextprofile = async()=>{
    if(!app.login){
        alert("Bạn cần đăng nhập")
    }else{
        app.profile_account_ho.splice(0,1);
        showContent(app.hobbyUser[0]);
        await app.reload();
        app.start();
    }
}

app.fetch =  {
    get:  (url, callback) => {
        var resolveFunc = function(){};
        var rejectFunc = function(){};
        var returnPromise = new Promise(function (resolve, reject) {
            resolveFunc = resolve;
            rejectFunc = reject;
        });
        if (!callback) {
            callback = function (err, friendList) {
                if (err) {
                return rejectFunc(err);
                }
                resolveFunc(friendList);
            };
        }
        fetch(url,{
            method : "GET",
        })
        .then(response => response.json())
        .then(data => callback(null,data))
        .catch(function(error) {
            callback(error)
        })
        return returnPromise
    },

    post: (url, data= { }, file ,callback) => fetch(url,{
        method: 'POST',
        headers: file ? {} : {'Content-type': 'application/json; charset=UTF-8'},
        body: file ? data : JSON.stringify(data)
    })
    .then(response => response.json())
    .then(data => callback(null,data))
    .catch(function(error) {
        callback(error)
    }),

    put: (url,data ={},callback) => fetch(url, {
        method: 'PUT',
        headers: {
            'Content-type': 'application/json; charset=UTF-8' // Indicates the content 
        },
        body: JSON.stringify(data)
    }).then(response => response.json())
    .then(data => callback(null,data))
    .catch(function(error) {
        callback(error)
    }),
    delete:  (url, callback) => fetch(url,{
        method : "DELETE",
    })
    .then(response => response.json())
    .then(data => callback(null,data))
    .catch(function(error) {
        callback(error)
    })
}

function setCookie(cname,cvalue,exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
  
function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
}

function noneitems(){
    qSA(".content_hd-item").forEach(a => a.classList.toggle("show"));
}
app.reload =  async()=>{
    nav_profile_main_listimg = 
    interestHTML =  
    interestProfileHTML =  
    interestProfileHTML2 =  
    ketnoihtml =  bodymess = app.dataUser = "";
    app.imgPofilePush = [];
    app.deleteimg = [];
    app.imgshow = [];
    var a = Object.values(qS(".nav_profile").classList)
    var c = []
    for(var b of a){
        c.push(b)
    }
    c.includes("top100") ? "" : qS(".nav_profile").classList.add("top100");
    qS(".nav_content").classList.remove("top100");
    
}

app.update = async() =>{
    showMessage(app.showMessage,0);
    
    await app.reload();
    await app.render();
    app.handEvent();
    app.loadnoti();
}
