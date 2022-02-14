var basic_url = 'https://localhost:7180'

var getUser = "/api/Logins";
var Interests = "/api/Hobbies";
var hobbyUserUrl = "/api/UserHobbies"
var Userurl = "/api/Users";
var ImagerUrl = "/api/Imager";
var UserImagerUrl = "/api/ImagerUsers";
var UserConnectUrl = "/api/UserContects";
var PostMessger = "/api/MessgerUsers";
var Notiurl = "/api/NotiUsers";

const qS = document.querySelector.bind(document);
const qSA = document.querySelectorAll.bind(document);

var app = {
    slideIndex: 1,
    imgPofilePush: [],
    dataUser: {},
    senderidnoti: {},
    deleteimg: [],
    imgshow: [],
    noti: 0,
    hobbyUser: [],
    showMessage: "",
    profile_account_ho: [],
    avt: "",
    login: false
}
//innerHTML
var nav_profile_main_listimg = "";
var interestHTML = "";
var interestProfileHTML = "";
var interestProfileHTML2 = "";
var ketnoihtml = "";
var bodymess = "";
var hobbies = "";
var logined_html = `
<div class="nav_content-button">
    <button type="button" class="nav-button nav-active" data-value = "ket_noi"><spam>Kết Nối</spam></button>
    <button type="button" class="nav-button" data-value = "tin_nhan"><spam>Tin Nhắn</spam></button>
    </div>
<div class="nav__content-main" id = "js_nav_content_main"></div>
`;
var login_register_html = `
<div class="login-register">
    <div id = "form-login" class = "show">
        <h2>Đăng nhập</h2>
        <form class="form">
            <input type="email" placeholder="Email" id = "email_login" />
            <input type="password" placeholder="Mật khẩu"  id = "pwd_login"/>
            <div class = "button" id = "login"> Đăng nhập </div>
            <div class = "form_login-button" id = "covent_dk"> <p> Đăng ký </p></div>
        </form>
    </div>
    <div id = "form-register" >
        <h2>Đăng Ký</h2>
        <form class="form"   >
            <input type="text" placeholder="Tên đăng nhập" id = "email"/>
            <input type="password" placeholder="Mật khẩu" id = "pwd"/>
            <input type="password" placeholder="Nhập lại mật khẩu" id = "confirmation_pwd" />
            <input type="text" placeholder="Tên hiển thị"  id = "name"/>
            <input type="text" placeholder="Địa Chỉ"  id= "city"/>      
            <input type="number" placeholder="Tuổi" onkeypress="return isNumberKey(event)" 
            maxlength="5" id = "age" />
            <h5 style = "margin-left: 10%; ">Giới tính</h5>
            <select id="genderoption" > 
                <option id = "Nam" value="Nam">Nam</option> 
                <option id = "Nữ" value="Nữ">Nữ</option> 
                <option id = "Giới tính khác" value="Giới tính khác">Giới tính khác</option> 
                <option id = "Ẩn" value="Ẩn">Ẩn</option> 
            </select>
            <h5 style = "margin-left: 10%; ">Sở thích</h5>   
            <div id = "js_interest" class = "form_register-interest"></div>
            <div class = "button" id = "register"> Đăng ký </div>
            <div class = "form_login-button" id = "covent_dn"> <p> Đăng nhập </p></div>
        </form>
    </div>
</div>`;