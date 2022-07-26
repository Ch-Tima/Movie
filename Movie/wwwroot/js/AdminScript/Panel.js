const img = document.getElementById('ShowImg');
const panelEdit = document.getElementById('EditPanel');
const comboBox = document.getElementById('ComboBox');
const elementList = document.getElementById('elements');

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
        data: JSON.stringify({ "id": Id, "type": comboBox.value }), //your data
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

    $.ajax({
        url: '/AdminPanel/Edit',
        data: JSON.stringify({ "type": comboBox.value, "newModel": temp }), //your data
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',

        success: function (result) {

            for (var [key, value] of Object.entries(temp)) {
                if (key != 'id') {
                    var iKey = key.toLowerCase() + '_' + temp.id;
                    var t = document.getElementById(iKey);
                    if (t != null) {
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


function SelectModel() {

    if (comboBox.value == null || comboBox.value == "") {
        console.error("Error type combobox");
        return 0;
    }
    $.ajax({
        url: "/AdminPanel/getPaintings",
        data: JSON.stringify(comboBox.value),
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log(result);
            var arrayObjResult = JSON.parse(result);

            var elementText = "";

            

            for (var i = 0; i < arrayObjResult.length; i++)
            {
                var e = JSON.stringify(arrayObjResult[i]).replace(/"([^"]+)":/g, '$1:').replace(/\\"/g, '"').replace(/([\{|:|,])(?:[\s]*)(")/g, "$1'").replace(/(?:[\s]*)(?:")([\}|,|:])/g, "'$1").replace(/([^\{|:|,])(?:')([^\}|,|:])/g, "$1\\'$2")
                elementText += '<div class="BoxElement" id="' + arrayObjResult[i]['Id'] + '"><p id="id_"' + arrayObjResult[i]['Id'] + '">' + arrayObjResult[i]['Id'] + '</p><p id="name_' + arrayObjResult[i]['Id'] + '">' + arrayObjResult[i]['Name'] + '</p><p id="evaluation_' + arrayObjResult[i]['Id'] + '">' + arrayObjResult[i]['Evaluation'] + '</p><p style="display: none;" id="description_' + arrayObjResult[i]['Id'] + '">' + arrayObjResult[i]['Description'] + '</p><input id="posterpath_' + arrayObjResult[i]['Id'] + '" type="image" onmouseover="MouseOver(this)" onmouseout="MouseOut(this)" src="../img/image.png" value="../' + arrayObjResult[i]['PosterPath'] + '"><div class="ElementInput"><input style="width: auto;" onclick="Edit(' + e + ')" type="button" name="Edit" value="Edit"><input onclick="Remove(' + arrayObjResult[i]['Id'] + ')" style="width: auto;" type="button" name="Remove" value="Remove"></div></div>';
            }
            elementList.innerHTML = elementText;

        },
        complete: function () {
            console.log('working...');
        },
        failure: function (err) {
            console.error(err);
        }
    });


}