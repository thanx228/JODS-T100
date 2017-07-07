var win = null;
function NewWindow(mypage, myname, w, h, scroll) {
    LeftPosition = (screen.width) ? (screen.width - w) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - h) / 2 : 0;
    settings = 'height=' + h + ',width=' + w + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',resizable=yes';
    win = window.open(mypage, myname, settings);
}
function NewWindowMax(mypage, myname, w, h, scroll) {
    LeftPosition = (screen.width) ? (screen.width - w) : 0;
    TopPosition = (screen.height) ? (screen.height - h) : 0;
//    settings = 'height=' + h + ',width=' + w + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',resizable=yes';
    win = window.open(mypage, myname);
}

function showPOP(url, type_show) {
    var xstyle = 'dialogHeight:400px;dialogWidth:600px;resizable:yes;scrollbars:yes;';
    if (type_show == '1') {
        xstyle = 'dialogHeight:500px;dialogWidth:700px;resizable:yes;scrollbars:yes;';
    }
    window.showModalDialog(url, 'newWin', xstyle);
}
function ChangeRowColor(row,color_click,color_unclick) {
    var scolor = "#ffdaa9";
    var scolor2 = "#ffffff";
    if (color_click != "")scolor = color_click;
    if (color_unclick != "") scolor2 = color_unclick;
    if (colorToHex(row.style.backgroundColor) == scolor) scolor = scolor2;
    row.style.backgroundColor = scolor;
}
function colorToHex(color) {
    if (color.substr(0, 1) === '#') {
        return color;
    }
    if (color.substr(0, 3) !== "rgb") {
        return color;
    }
    var digits = /(.*?)rgb\((\d+), (\d+), (\d+)\)/.exec(color);
    var red = parseInt(digits[2]);
    var green = parseInt(digits[3]);
    var blue = parseInt(digits[4]);
    var rgb = blue | (green << 8) | (red << 16);
    return digits[1] + '#' + rgb.toString(16);
}
function returnConfirm(msg){
    return confirm(msg);
}
