// window.onload = function (){
//     document.getElementById("modal_close_button").addEventListener("click",  function () { CloseModal("modal"); });
// };

var scrollbar_style = `
<style>
/* Let's get this party started */
::-webkit-scrollbar {
    width: 5px;
}
/* Track */
::-webkit-scrollbar-track {
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3); 
    -webkit-border-radius: 5px;
    border-radius: 5px;
}
/* Handle */
::-webkit-scrollbar-thumb {
    -webkit-border-radius: 5px;
    border-radius: 5px;
    background: rgba(0,0,0,0.5); 
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.5); 
}
::-webkit-scrollbar-thumb:window-inactive {
    background: rgba(0,0,0,0.4); 
}
</style>`;

function ScrollbarInject(){
    var popup_iframe = document.getElementsByClassName('modal_iframe')[0];
    var win = popup_iframe.contentWindow;
    var doc = popup_iframe.contentDocument? popup_iframe.contentDocument: popup_iframe.contentWindow.document;
    doc.body.innerHTML = doc.body.innerHTML + scrollbar_style;
}

function ShowModal(source, href_src, modal_id) {
    var info = document.getElementById(modal_id.concat("_info"));
    if(source == 1){
        info.innerHTML = `<iframe class="modal_iframe" src="./res/` + href_src + `.html" onload="ScrollbarInject()"></iframe>`;
        Show([modal_id], 1, "0.5s");
    } else if (source == 0) {
        info.innerHTML = `<iframe class="modal_iframe" src="` + href_src + `" onload=""></iframe>`;
        Show([modal_id], 1, "0.5s");
    } else {
        console.log("Error: he_modal.js - ShowModal() - Source not defined or not acceptable value.");
    }
}

function CloseModal(modal_id){
    Hide([modal_id], 1, "0.5s");
    setTimeout(function(){
    var info = document.getElementById(modal_id.concat("_info"));
    while(info.hasChildNodes()){ info.removeChild(info.firstChild); }
    }, 500);
}

function Show(elements, isFade, time){
    document.documentElement.style.setProperty('--fade-speed', time + " !important");
    for(each of elements){
        if(isFade == 1){
            var fade_element = document.getElementById(each),
                computed_style = window.getComputedStyle(fade_element);
            if(computed_style.getPropertyValue('opacity') > 0){
                fade_element.style.opacity = 0;
            }

            fade_element.style.visibility = 'visible';

            setTimeout(function(){
                fade_element.classList.add('fade_in');
                fade_element.classList.remove('fade_out');
            }, 500);

        }else{
            var current_element = document.getElementById(each);
            current_element.style.visibility = "visible";
        }
    }
}

/* Hides elements with or without a fade effect */
function Hide(elements, isFade, time){
    document.documentElement.style.setProperty('--fade-speed', time + " !important");
    for(each of elements){
    if(isFade == 1){
        var fade_element = document.getElementById(each),
        computed_style = window.getComputedStyle(fade_element);
        if(computed_style.getPropertyValue('opacity') < 1){
        fade_element.style.opacity = 1;
        }
        
        fade_element.classList.add('fade_out');
        fade_element.classList.remove('fade_in');

        setTimeout(function(){
        fade_element.style.visibility = "hidden";
        }, 500);
        }else{
        var current_element = document.getElementById(each);
        current_element.style.visibility = "hidden";
    }
    }
}