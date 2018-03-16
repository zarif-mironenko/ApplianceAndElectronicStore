// Добавить характеристику к категории
function addProdSpecToTable(tableBody, prodSpecId, addedSpecificationName, prodSpecQty) {
    var tr = document.createElement('tr');

    var td = tr.insertCell();
    td.className = 'text-right';

    // Вставляем в ячейку с Id значение Id
    td.innerText = prodSpecId;

    if (prodSpecId != 0) {
        // Создаем скрытый input со значением Id новой характеристики
        var input = document.createElement('input');
        input.type = 'hidden';
        input.value = prodSpecId;
        input.id = `CategoriesProductSpecifications_${prodSpecQty}__ProductSpecificationId`;
        input.name = `CategoriesProductSpecifications[${prodSpecQty}].ProductSpecificationId`;

        // Добавляем скрытый input со значением Id
        td.appendChild(input);

        input = document.createElement('input');
        input.type = 'hidden';
        input.value = addedSpecificationName;
        input.id = `CategoriesProductSpecifications_${prodSpecQty}__ProductSpecification_Name`;
        input.name = `CategoriesProductSpecifications[${prodSpecQty}].ProductSpecification.Name`;

        td = tr.insertCell();
        td.innerText = addedSpecificationName;
        td.appendChild(input);
    } else {
        // Создаем input со значением Name новой характеристики
        input = document.createElement('input');
        input.className = 'form-control';
        input.type = 'text';
        input.id = `CategoriesProductSpecifications_${prodSpecQty}__ProductSpecification_Name`;
        input.name = `CategoriesProductSpecifications[${prodSpecQty}].ProductSpecification.Name`;

        // Добавляем атрибут value со значением
        // добавляемой характеристики 
        var attr = document.createAttribute('value');
        attr.value = addedSpecificationName;
        input.setAttributeNode(attr);

        // Устанавливаем атрибуты клиентской валидации
        input.dataset.val = true;
        input.dataset.valRequired = 'Не указано название характеристики';


        // Создаем span, в котором будет выводится сообщение
        // о неправильном вводе значения в input со значением Name
        var spanValid = document.createElement('span');
        spanValid.className = 'text-danger field-validation-valid';

        spanValid.dataset.valmsgFor = `CategoriesProductSpecifications[${prodSpecQty}].ProductSpecification.Name`;
        spanValid.dataset.valmsgReplace = true;

        td = tr.insertCell();
        td.appendChild(input);          // Добавляем input со значением Name
        td.appendChild(spanValid);      // Добавляем span сообщений валидации
    } // if

    tableBody.append(tr);

    return prodSpecQty + 1;
} // addProdSpecToTable

// Добавить значение характеристики к таблице характеристик товара
function addProdSpecWithValueToTable(tableBody, prodSpecValue, iProdSpec) {
    var tr = document.createElement('tr');

    var td = tr.insertCell();

    var label = document.createElement('label');
    label.classList.add('control-label', 'form-control-static');
    label.innerText = prodSpecValue.productSpecification.name;

    td.appendChild(label);

    // Создаем скрытый input со значением Id характеристики
    var input = document.createElement('input');
    input.type = 'hidden';
    input.value = prodSpecValue.productSpecificationId;
    input.id = `SpecificationsValues_${iProdSpec}__ProductSpecificationId`;
    input.name = `SpecificationsValues[${iProdSpec}].ProductSpecificationId`;

    // Добавляем скрытый input со значением Id
    td.appendChild(input);
    
    // Создаем input со значением характеристики
    input = document.createElement('input');
    input.className = 'form-control';
    input.type = 'text';
    input.id = `SpecificationsValues_${iProdSpec}__Value`;
    input.name = `SpecificationsValues[${iProdSpec}].Value`;

    // Добавляем атрибут value со значением
    // добавляемой характеристики 
    if (prodSpecValue.value != null) {
        var attr = document.createAttribute('value');
        attr.value = prodSpecValue.value;
        input.setAttributeNode(attr);
    } // if

    // Устанавливаем атрибуты клиентской валидации
    input.dataset.val = true;
    input.dataset.valRequired = 'Не указано название характеристики';


    // Создаем span, в котором будет выводится сообщение
    // о неправильном вводе значения в input со значением Name
    var spanValid = document.createElement('span');
    spanValid.className = 'text-danger field-validation-valid';

    spanValid.dataset.valmsgFor = `SpecificationsValues[${iProdSpec}].Value`;
    spanValid.dataset.valmsgReplace = true;

    td = tr.insertCell();
    td.appendChild(input);          // Добавляем input со значением Name
    td.appendChild(spanValid);      // Добавляем span сообщений валидации

    tableBody.append(tr);
}

function detectChangesForSaving() {
    $(function () {
        var isValuesWasEdit = false;

        $('input:not([type="submit"])').change(function () {
            if (!isValuesWasEdit) isValuesWasEdit = true;

            $('#btnSaveChanges').removeAttr('disabled');
        });

        $('a[href]').click(function (event) {
            if (!isValuesWasEdit) return;

            event.preventDefault();
            alert('У Вас есть несохраненные изменения!');
        });
    });
}

function changeSearchFormOffset() {
    //$(window).resize(function () {
    //    let div = $('#formSearchProduct').parent();

    //    let colOffsetClass = 'col-md-offset-1';

    //    let colMd3Class = 'col-md-3';
    //    let colMd4Class = 'col-md-4';
        
    //    if (innerWidth > 1190 && innerWidth <= 1210)
    //        div.removeClass(`${colMd4Class} ${colOffsetClass}`)
    //           .addClass(colMd3Class);
    //    else if (innerWidth <= 1190) div.removeClass(colMd3Class).addClass(colMd4Class);
    //    else div.addClass(colOffsetClass);
    //});
}

function checkAvailableProductQtyForOrder(btnForFormSubmitting, inputsQty = 1, isList = false) {
    $(function () {
        var signIdOrClass = (inputsQty > 1 || isList) ? '.' : '#';

        var inputsRequestedProdQty = $(`${signIdOrClass}quantity`);
        var inputsProdQtyInStock = $(`${signIdOrClass}prodQtyInStock`);
        var isFoundError = false;

        inputsRequestedProdQty.change(function () {
            if (inputsQty > 1 || isList) {
                var currInputRequestedProdQty = $(`#${this.id}`)[0];
                var currInputProdQtyInStock = $(`#pQtyInStock${currInputRequestedProdQty.dataset.index}`)[0];
            } else {
                currInputRequestedProdQty = inputsRequestedProdQty[0];
                currInputProdQtyInStock = inputsProdQtyInStock[0];
            } // if

            var spanShowErrMsg = $(`span[data-valmsg-for="${currInputRequestedProdQty.id}"]`);

            if (+this.value > +currInputProdQtyInStock.value) {
                currInputRequestedProdQty.classList.add('input-validation-error');
                spanShowErrMsg.html(`Максимально можно заказать <b>${currInputProdQtyInStock.value} шт.</b>`);
                isFoundError = true;
            } else {
                currInputRequestedProdQty.classList.remove('input-validation-error');
                spanShowErrMsg.text('');
                isFoundError = false;
            } // if

            //$.get({
            //    url: '/Products/AjaxGetAvailableProductQtyForOrder',
            //    data: { productId: +$('#productId').val(), requstedQty: +$(this).val() },
            //    success: function () {
            //        inputRequestedProdQty.removeClass('input-validation-error');
            //        spanShowErrMsg.text('');
            //        isFoundError = false;
            //    },
            //    error: function (_error) {
            //        if (_error.status >= 500) return;

            //        inputRequestedProdQty.addClass('input-validation-error');
            //        spanShowErrMsg.text(_error.responseText);
            //        isFoundError = true;
            //    }
            //});
        });

        $(btnForFormSubmitting).click(function (event) {
            if (!isFoundError) return;

            event.preventDefault();
            $(`#qty${this.dataset.index}`).focus();
        });
    });
}