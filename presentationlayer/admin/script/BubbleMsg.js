function getOffsetRect(elem) {
    var box = elem.getBoundingClientRect()

    var body = document.body
    var docElem = document.documentElement

    var scrollTop = window.pageYOffset || docElem.scrollTop || body.scrollTop
    var scrollLeft = window.pageXOffset || docElem.scrollLeft || body.scrollLeft

    var clientTop = docElem.clientTop || body.clientTop || 0
    var clientLeft = docElem.clientLeft || body.clientLeft || 0

    var top = box.top + scrollTop - clientTop
    var left = box.left + scrollLeft - clientLeft

    return { top: Math.round(top), left: Math.round(left) }
}

function AutoHide(SpanID, Duration) {
    var Pattern = /^\d+$/;
    var isNumber = Pattern.test(Duration);

    if (isNumber) {
        setTimeout(function () {
            SpanID.className = "HideBubble";
        }, Duration);
    }
}

function RequiredFieldValidation(TextBoxID, SpanID, Duration, ErrorMsg) {
    var j = getOffsetRect(TextBoxID);

    AutoHide(SpanID, Duration);

    var Width = TextBoxID.clientWidth + 150 + j.left;
    if (TextBoxID.value == "") {
        SpanID.className = "ShowBubble";
        SpanID.style.top = 30 + "px";
        SpanID.style.left = 200 + "px";
        SpanID.innerHTML = ErrorMsg;

        return false;
    }
    return true;
}

function EmailValidation(ControlID, SpanID, ErrorMsg) {
    var j = getOffsetRect(ControlID);
    var Width = ControlID.clientWidth + 10 + j.left;
    var regEmail = /^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z]+(\.[a-zA-Z]+)*(\.[a-zA-Z]{1,3})$/;

    AutoHide(SpanID, Duration);

    if (!regEmail.test(ControlID)) {
        SpanID.className = "ShowBubble";
        SpanID.style.top = j.top + "px";
        SpanID.style.left = Width + "px";
        SpanID.innerHTML = ErrorMsg;
        return false;
    }
    return true;
}

function CustomValidation(ControlID, SpanID, Pattern, ErrorMsg) {
    var j = getOffsetRect(ControlID);
    var Width = ControlID.clientWidth + 10 + j.left;
    var reg = Pattern;

    AutoHide(SpanID, Duration);

    if (!reg.test(ControlID)) {
        SpanID.className = "ShowBubble";
        SpanID.style.top = j.top + "px";
        SpanID.style.left = Width + "px";
        SpanID.innerHTML = ErrorMsg;
        return false;
    }
    return true;
}