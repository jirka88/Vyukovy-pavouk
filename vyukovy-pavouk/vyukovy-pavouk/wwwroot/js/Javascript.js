function SetBodyStyle() {
    document.querySelector("body").style.background = "#1f1f1f";
}
function SetBodyStyleContrast() {
    document.querySelector("body").style.background = "#000000";
}
function SetInputDark() {
    document.querySelector("input").style.background = "#292929";
}
function ScrollTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementsByClassName("navigationTop")[0].style.display = "block";
    } else {
        document.getElementsByClassName("navigationTop")[0].style.display = "none";
    }
}
