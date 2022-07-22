const img = document.getElementById('ShowImg');
const panelEdit = document.getElementById('EditPanel');

function MouseOver(argument) {

    var left = argument.offsetLeft + 5;
    var top = argument.offsetTop + 30;
    img.style.cssText = 'display: block; left:' + left + 'px; top:' + top + 'px;';
    img.src = argument.value;

}
function MouseOut(argument) {
    img.style.cssText = 'display: none;'
    img.src = '';
}
function Remove(Id) {
        $.ajax({
        url: '/AdminPanel/Remove',
        data: JSON.stringify(Id), //your data
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',

        success: function (result) {
            console.log('Ok_Remove: ' + Id); //Remove from Elements
            var element = document.getElementById(Id);
            element.remove();
        },
        complete: function () {
            //do something on complete
        },
            failure: function (err) {
                console.log(err); // Display error message
        }
    });
}
var temp;
var isEdit = false;
function Edit(e) {

    if (isEdit) {
        console.log('isEdit:' + isEdit);
        return;
    }
    isEdit = true;
    temp = e;
    console.log(e);

    panelEdit.style.cssText = 'display: block;';

    var div = document.createElement('div');
    div.id = 'TempPanel';

    for (var [key, value] of Object.entries(e))
    {
        if (key != 'id') {
            var inp = document.createElement('input');

            switch (typeof value) {
                case 'string':
                    inp.type = 'text';
                    break;
                case 'number':
                    inp.type = 'number';
                    break;
                case 'boolean':
                    inp.type = 'checkbox';
                    break;
                default:
                    console.log('object');
                    break;

            }
            inp.id = key;
            inp.value = value;

            div.appendChild(inp);
            panelEdit.appendChild(div);
        }
    }
    

}
function Save() {
    console.log(temp);

    for (var key of Object.keys(temp))
    {
        if (key != 'id')
            temp[key] = document.getElementById(key).value;
    }
    console.log('=================');
    console.log(temp);

    $.ajax({
        url: '/AdminPanel/Edit',
        data: JSON.stringify(temp), //your data
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',

        success: function (result) {

            for (var [key, value] of Object.entries(result)) {
                if (key != 'id') {
                    var iKey = key.toLowerCase() + '_' + result.id;
                    var t = document.getElementById(iKey);

                    switch (t.tagName) {
                        case 'P':
                            t.textContent = value;
                            break;
                        case 'INPUT':
                            break;
                            t.value = value;
                        default:
                            console.log('Error: tagName=NaN');
                            break;
                    }
                }
            }
        },
        complete: function () {
            document.getElementById('TempPanel').remove();
            panelEdit.style.cssText = 'display: none;';
            isEdit = false;

            //TODO: Create message "upDate..."
            
        },
        failure: function (err) {
            alert(err); // Display error message
        }
    });

}
function Back() {
    document.getElementById('TempPanel').remove();
    panelEdit.style.cssText = 'display: none;';
    isEdit = false;
}