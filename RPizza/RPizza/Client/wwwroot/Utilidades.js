function focusById(elementId) {
    var element = document.getElementById(elementId);
    if (element) {
        element.focus();
    }
}


function focusonId(elementId) {
    var element = document.getElementById(elementId);
    if (element) {
        element.focus();
    }
}


window.jsfunction = {
    focusElement: function (id) {
        const element = document.getElementById(id);
        element.focus();
    }
}


function Foco(x) {
    var element = document.getElementById('txt_name');
    if (element) {
        element.focus();
    }
}

window.MySetFocus = (ctrl) => {
    document.getElementById(ctrl).focus();
    return true;
}